using System.ComponentModel;
using DefaultNamespace;
using Windows;
using ResourceManager.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using Assets.Scripts.Common.Localization;
using Assets.Scripts.Common.Session;

namespace Assets.Scripts.Common.Installers
{
   public class ProjectInstaller : MonoInstaller
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

         Container.BindInstance(globalSession).AsSingle();
         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInstance(_windowsController).AsSingle();
         Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle().NonLazy();
      }

      private static void InitializeAddressables()
      {
         var handleStorage = new HandleStorage();
         AsyncOpHandleExtension.Initialize(handleStorage);
         AddressableExtention.Initialize(handleStorage);
      }
      


   }
}