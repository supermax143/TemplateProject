using System.Threading.Tasks;
using Common.Localization;
using Zenject;

namespace Common.GameInitializer.States
{
   internal class InitLocalizationStep : InitializeStepBase
   {
      
      [Inject] private LocalizationController _localizationController;
      
      public override string Ident => "init_localization";
      
      public override async Task Execute()
      {
         await _localizationController.Initialize();
      }
   }
}