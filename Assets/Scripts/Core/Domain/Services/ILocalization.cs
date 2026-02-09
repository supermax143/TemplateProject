using System;
using System.Collections.Generic;

namespace Core.Domain.Services
{
   public interface ILocalization
   {
     
      string Get(string key);

      event Action<string> LanguageChanged;
      bool Initialized { get; }
      string CurrentLanguageCode { get; }
      List<string> LanguageCodes { get; }
      void SetLanguage(string languageCode);
      bool TryGetLanguageCodes(out IEnumerable<string> codes);
   }
}