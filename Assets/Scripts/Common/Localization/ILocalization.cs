using System;

namespace Assets.Scripts.Common.Localization
{
   public interface ILocalization
   {
      /// <summary>
      /// Получить локализованную строку по ключу для текущего языка.
      /// При отсутствии ключа вернётся сам ключ.
      /// </summary>
      string Get(string key);

      event Action<string> LanguageChanged;
   }
}