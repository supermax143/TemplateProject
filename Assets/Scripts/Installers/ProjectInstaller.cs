using DefaultNamespace;
using Windows;
using Session;
using UnityEngine;
using Zenject;

namespace Installers
{
   public class ProjectInstaller : MonoInstaller
   {

      [SerializeField, HideInInspector]
      private GlobalSession globalSession;
      
      [SerializeField, HideInInspector]
      private WindowManager windowManager;
     
      private void OnValidate()
      {
         globalSession = GetComponent<GlobalSession>();
         windowManager = GetComponent<WindowManager>();
      }


      public override void InstallBindings()
      {
         Container.BindInstance(globalSession).AsSingle();
         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInstance(windowManager).AsSingle();
      }
   }
}