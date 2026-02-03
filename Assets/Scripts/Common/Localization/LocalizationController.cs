using ResourceManager.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Common.Localization
{
    internal class LocalizationController : ILocalization
    {

        public event Action<string> LanguageChanged;

        private const string LOCALIZATION_KEY = "Localization";
        private const string DefaultLanguageCode = "ru";

        private char _separator = '|';

        /// <summary>
        /// Текущий код языка (например, "ru", "en").
        /// </summary>
        public string CurrentLanguageCode { get; private set; }

        /// <summary>
        /// Список доступных языков (из заголовка CSV).
        /// </summary>
        public IReadOnlyList<string> AvailableLanguages => _languageCodes;

        private readonly Dictionary<string, string> _currentLanguageTable = new();
        private readonly Dictionary<string, Dictionary<string, string>> _allLanguages
            = new(StringComparer.OrdinalIgnoreCase);

        private List<string> _languageCodes = new();


        public async Task Initialize()
        {
            var localizationText = await AddressableExtention.Load<TextAsset>(LOCALIZATION_KEY, GetTag());
            Initialize(localizationText?.text);
        }

        private string GetTag()
        {
            return GetHashCode().ToString();
        }

        private void Initialize(string csvText)
        {
            if (string.IsNullOrWhiteSpace(csvText))
            {
                Debug.LogError("[LocalizationController] Пустой текст CSV. Нечего загружать.");
                return;
            }

            using var reader = new StringReader(csvText);

            _allLanguages.Clear();
            _languageCodes = new List<string>();

            string headerLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(headerLine))
            {
                Debug.LogError("[LocalizationController] Первая строка CSV пуста. Ожидается заголовок: key;ru;en;...");
                return;
            }

            string[] headers = SplitCsvLine(headerLine);
            if (headers.Length < 2 || !string.Equals(headers[0], "key", StringComparison.OrdinalIgnoreCase))
            {
                Debug.LogError("[LocalizationController] Первая колонка должна называться 'key'. Текущая строка: " + headerLine);
                return;
            }

            // Список языков из заголовка, начиная со второго столбца.
            for (int i = 1; i < headers.Length; i++)
            {
                string langCode = headers[i].Trim();
                if (string.IsNullOrEmpty(langCode))
                    continue;

                if (!_allLanguages.ContainsKey(langCode))
                {
                    _allLanguages[langCode] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    _languageCodes.Add(langCode);
                }
            }

            if (_languageCodes.Count == 0)
            {
                Debug.LogError("[LocalizationController] Не найдено ни одного языка в заголовке.");
                return;
            }

            // Читаем строки с данными.
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] columns = SplitCsvLine(line);
                if (columns.Length == 0)
                    continue;

                string key = columns[0].Trim();
                if (string.IsNullOrEmpty(key))
                    continue;

                for (int i = 1; i < headers.Length && i < columns.Length; i++)
                {
                    string langCode = headers[i].Trim();
                    if (string.IsNullOrEmpty(langCode))
                        continue;

                    string value = columns[i].Trim();
                    if (!_allLanguages.TryGetValue(langCode, out var table))
                        continue;

                    // Последнее значение для ключа перезапишет предыдущее — это нормально.
                    table[key] = value;
                }
            }

            Debug.Log($"[LocalizationController] Успешно загружено языков: {_languageCodes.Count}");

            // Устанавливаем язык по умолчанию после загрузки.
            SetLanguage(DefaultLanguageCode);
        }

        /// <summary>
        /// Установить текущий язык по коду (должен совпадать с заголовком столбца, например 'ru', 'en').
        /// </summary>
        public void SetLanguage(string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                Debug.LogWarning("[LocalizationController] Пустой код языка. Игнорируем.");
                return;
            }

            languageCode = languageCode.Trim();

            if (!_allLanguages.TryGetValue(languageCode, out var table))
            {
                Debug.LogWarning($"[LocalizationController] Язык '{languageCode}' не найден. Остаёмся на '{CurrentLanguageCode}'.");
                return;
            }

            // If language didn't change - nothing to do
            if (string.Equals(CurrentLanguageCode, languageCode, StringComparison.OrdinalIgnoreCase))
                return;

            _currentLanguageTable.Clear();
            foreach (var kvp in table)
            {
                _currentLanguageTable[kvp.Key] = kvp.Value;
            }

            CurrentLanguageCode = languageCode;

            // Notify listeners about language change
            LanguageChanged?.Invoke(languageCode);
        }

        /// <summary>
        /// Получить локализованную строку по ключу для текущего языка.
        /// При отсутствии ключа вернётся сам ключ.
        /// </summary>
        public string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            if (_currentLanguageTable.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value))
                return value;

            // Фолбэк — вернуть ключ, чтобы сразу видеть "битые" строки.
            return key;
        }

        /// <summary>
        /// Попробовать получить строку; вернуть true, если ключ найден.
        /// </summary>
        public bool TryGet(string key, out string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                value = string.Empty;
                return false;
            }

            return _currentLanguageTable.TryGetValue(key, out value);
        }

        /// <summary>
        /// Простая разбивка строки CSV по разделителю с учетом возможных кавычек.
        /// Достаточно для типичных экспортов из Excel.
        /// </summary>
        private string[] SplitCsvLine(string line)
        {
            if (line == null)
                return Array.Empty<string>();

            var result = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    // Двойные кавычки внутри поля ("") интерпретируем как один символ ".
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == _separator && !inQuotes)
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }

            result.Add(sb.ToString());
            return result.ToArray();
        }

    }
}

