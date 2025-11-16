using System;
using Session.States;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Session
{
   public class GlobalSession : MonoBehaviour
   {

      [Inject] private DiContainer _container;

      private GlobalSessionStateBase _curState;

      public GlobalSessionStateBase CurState => _curState;

      private void Start()
      {
         SetState<InitState>();
      }

      public T SetState<T>() where T : GlobalSessionStateBase
      {
         if (CurState != null && CurState.GetType() == typeof(T))
         {
            return CurState as T;
         }

         if (CurState != null)
         {
            CurState.ExitState();
         }
         _curState = _container.InstantiateComponent<T>(gameObject);
         return CurState as T;
      }
      
   }
}