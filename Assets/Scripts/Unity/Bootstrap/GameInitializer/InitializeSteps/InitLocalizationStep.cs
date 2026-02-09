using System.Threading.Tasks;
using Core.Application.Localization;
using Zenject;

namespace Unity.Bootstrap.GameInitializer.InitializeSteps
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