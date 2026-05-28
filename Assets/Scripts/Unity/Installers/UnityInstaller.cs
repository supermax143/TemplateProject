using Core.Application.ApplicationSession;
using Core.Application.ApplicationSession.States;
using Core.Application.DataStorage;
using Core.Application.Localization;
using Unity.Bootstrap.GameInitializer;
using Unity.Infrastructure.ResourceManager;
using Unity.Infrastructure.Scenes;
using Unity.Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace Unity.Bootstrap.Installers
{
   internal class UnityInstaller : MonoInstaller
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

         Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle();
         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInterfacesAndSelfTo<WindowsController>().FromInstance(_windowsController);
         Container.BindInterfacesAndSelfTo<GameBootrstarp>().AsSingle();
         Container.BindInterfacesAndSelfTo<ResourceManager>().AsSingle();
         //Data Storage
         Container.Bind<ILocalStorageProvider>().To<PlayerPrefsStorageProvider>().AsTransient();
         Container.Bind<IGlobalStorageProvider>().To<PlayerPrefsStorageProvider>().AsTransient();
      }

      private static void InitializeAddressables()
      {
         var handleStorage = new HandleStorage();
         AsyncOpHandleExtension.Initialize(handleStorage);
         AddressableExtention.Initialize(handleStorage);
      }
      


   }
}