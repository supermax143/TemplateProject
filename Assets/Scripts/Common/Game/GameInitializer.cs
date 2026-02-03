using System.Threading.Tasks;
using Assets.Scripts.Common.Localization;
using Zenject;

namespace Common.Game
{
   internal class GameInitializer
   {
      [Inject] private LocalizationController _localizationController;
      
      public async Task Initialize()
      {
         await _localizationController.Initialize();
         await Task.Delay(1000);
      }
   }
}