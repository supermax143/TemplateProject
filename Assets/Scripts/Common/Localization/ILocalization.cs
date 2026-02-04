using System;

namespace Assets.Scripts.Common.Localization
{
   public interface ILocalization
   {
     
      string Get(string key);

      event Action<string> LanguageChanged;
      bool Initialized { get; }
   }
}