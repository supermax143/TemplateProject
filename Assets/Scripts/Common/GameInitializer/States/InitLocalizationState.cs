using System.Threading.Tasks;
using Assets.Scripts.Common.Localization;
using Zenject;

namespace Common.Game.States
{
   internal class InitLocalizationState : InitializeStateBase
   {
      
      [Inject] private LocalizationController _localizationController;
      
      public override string Ident => "init_localization";
      
      public override async Task Execute()
      {
         await _localizationController.Initialize();
      }
   }
}