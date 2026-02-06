using Common.GameInitializer.States;
using Common.Localization;
using Common.ResourceManager;
using Common.Scenes;
using Common.Session;
using Common.Session.States;
using Common.Windows;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
   internal class ProjectInstaller : MonoInstaller
   {

      [SerializeField, HideInInspector]
      private WindowsController _windowsController;
      
      
      private void OnValidate()
      {
         _windowsController = GetComponent<WindowsController>();
      }


      public override void InstallBindings()
      {
         InitializeAddressables();

         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInterfacesAndSelfTo<WindowsController>().FromInstance(_windowsController);
         Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle();
         Container.BindInterfacesAndSelfTo<GameInitializer.GameInitializer>().AsSingle();
         
         //Initialization
         Container.Bind<InitializeStepBase>().To<InitLocalizationStep>().AsTransient();
         Container.Bind<InitializeStepBase>().To<LoginStep>().AsTransient();
         Container.Bind<InitializeStepBase>().To<LoadAssetsStep>().AsTransient();
         
         // Session
         Container.Bind<InitState>().AsTransient();
         Container.Bind<MainMenuState>().AsTransient();
         Container.Bind<GameState>().AsTransient();
         Container.BindInterfacesAndSelfTo<Session.Session>().AsSingle().NonLazy();
      }

      private static void InitializeAddressables()
      {
         var handleStorage = new HandleStorage();
         AsyncOpHandleExtension.Initialize(handleStorage);
         AddressableExtention.Initialize(handleStorage);
      }
      


   }
}