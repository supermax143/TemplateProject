using Common.Localization;
using Common.ResourceManager;
using Common.Scenes;
using Common.Session;
using Common.Windows;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
   internal class ProjectInstaller : MonoInstaller
   {

      [SerializeField, HideInInspector]
      private GlobalSession globalSession;
      [SerializeField, HideInInspector]
      private WindowsController _windowsController;
      
      
      private void OnValidate()
      {
         globalSession = GetComponent<GlobalSession>();
         _windowsController = GetComponent<WindowsController>();
      }


      public override void InstallBindings()
      {
         InitializeAddressables();

         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInterfacesAndSelfTo<WindowsController>().FromInstance(_windowsController).AsSingle();
         Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle().NonLazy();
         Container.BindInterfacesAndSelfTo<GameInitializer.GameInitializer>().AsSingle().NonLazy();
         Container.BindInstance(globalSession).AsSingle();
      }

      private static void InitializeAddressables()
      {
         var handleStorage = new HandleStorage();
         AsyncOpHandleExtension.Initialize(handleStorage);
         AddressableExtention.Initialize(handleStorage);
      }
      


   }
}