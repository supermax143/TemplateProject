using DefaultNamespace;
using Windows;
using ResourceManager.Runtime;
using Session;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
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
      }

      private static void InitializeAddressables()
      {
         var handleStorage = new HandleStorage();
         AsyncOpHandleExtension.Initialize(handleStorage);
         AddressableExtention.Initialize(handleStorage);
      }


   }
}