using DefaultNamespace;
using Popups;
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
      private PopupManager popupManager;
     
      private void OnValidate()
      {
         globalSession = GetComponent<GlobalSession>();
         popupManager = GetComponent<PopupManager>();
      }


      public override void InstallBindings()
      {
         Container.BindInstance(globalSession).AsSingle();
         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInstance(popupManager).AsSingle();
      }
   }
}