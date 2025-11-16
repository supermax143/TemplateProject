using DefaultNamespace;
using Session;
using UnityEngine;
using Zenject;

namespace Installers
{
   public class ProjectInstaller : MonoInstaller
   {

      [SerializeField, HideInInspector]
      private GlobalSession globalSession;
     
      private void OnValidate()
      {
         globalSession = GetComponent<GlobalSession>();
      }


      public override void InstallBindings()
      {
         Container.BindInstance(globalSession).AsSingle();
         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
      }
   }
}