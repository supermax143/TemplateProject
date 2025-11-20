using DefaultNamespace;
using Windows;
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
         Container.BindInstance(globalSession).AsSingle();
         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInstance(_windowsController).AsSingle();
      }
   }
}