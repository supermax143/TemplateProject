using Core.Application.ApplicationSession.States;
using Core.Application.Localization;
using Core.Application.Models;
using Zenject;

namespace Core.Application.Installers
{
   public class ApplicationInstaller  : MonoInstaller
   {
      public override void InstallBindings()
      {
          
         Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle();
         
         // Session
         Container.Bind<BootstrapState>().AsTransient();
         Container.Bind<MainMenuState>().AsTransient();
         Container.Bind<GameState>().AsTransient();
         Container.BindInterfacesAndSelfTo<ApplicationSession.ApplicationStateMachine>().AsSingle().NonLazy();
         
         
         //Models
         Container.BindInterfacesAndSelfTo<MainModel>().AsSingle();
         
      }
   }
}