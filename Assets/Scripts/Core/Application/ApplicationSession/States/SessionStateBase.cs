using UnityEngine;
using Zenject;

namespace Common.Session.States
{
   internal abstract class SessionStateBase : ISessionStateInternal
   {
      
      [Inject] protected ApplicationSession ApplicationSession;

      public void Enter()
      {
         OnStateEnter();
      }

      public void Exit()
      {
         OnStateExit();
      }

      public virtual void StartGame() { }


      protected abstract void OnStateEnter();
      
      protected virtual void OnStateExit() 
      {
      }

   }
}