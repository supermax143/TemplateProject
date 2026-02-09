using Core.Application.ApplicationSession.States;
using Core.Application.Localization;
using Zenject;

namespace Core.Application.Installers
{
   public class ApplicationInstaller  : MonoInstaller
   {
      public override void InstallBindings()
      {
          
         Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle();
         
         // Session
         Container.Bind<InitState>().AsTransient();
         Container.Bind<MainMenuState>().AsTransient();
         Container.Bind<GameState>().AsTransient();
         Container.BindInterfacesAndSelfTo<ApplicationSession.ApplicationSession>().AsSingle().NonLazy();
      }
   }
}