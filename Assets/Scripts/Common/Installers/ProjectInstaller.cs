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
      private Session.Session _session;
      [SerializeField, HideInInspector]
      private WindowsController _windowsController;
      
      
      private void OnValidate()
      {
         _session = GetComponent<Session.Session>();
         _windowsController = GetComponent<WindowsController>();
      }


      public override void InstallBindings()
      {
         InitializeAddressables();

         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInterfacesAndSelfTo<WindowsController>().FromInstance(_windowsController).AsSingle();
         Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle().NonLazy();
         Container.BindInterfacesAndSelfTo<GameInitializer.GameInitializer>().AsSingle().NonLazy();
         Container.BindInstance(_session).AsSingle();
         // States
         Container.Bind<InitState>().AsTransient();
         Container.Bind<MainMenuState>().AsTransient();
         Container.Bind<GameState>().AsTransient();
      }

      private static void InitializeAddressables()
      {
         var handleStorage = new HandleStorage();
         AsyncOpHandleExtension.Initialize(handleStorage);
         AddressableExtention.Initialize(handleStorage);
      }
      


   }
}