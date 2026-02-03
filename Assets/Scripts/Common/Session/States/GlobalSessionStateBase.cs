using System;
using System.Collections;
using Assets.Scripts.Common.Session;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Android;
using Zenject;

namespace Assets.Scripts.Common.Session.States
{
   public abstract class GlobalSessionStateBase : MonoBehaviour
   {
      
      [Inject] protected GlobalSession _globalSession;

      private void Start()
      {
         OnStateEnter();
      }

      public void ExitState()
      {
         Destroy(this);
      }

      public virtual void StartGame() { }


      protected abstract void OnStateEnter();
      
      protected virtual void OnStateExit() 
      {
      }

      private void OnDestroy()
      {
         OnStateExit();
      }
   }
}