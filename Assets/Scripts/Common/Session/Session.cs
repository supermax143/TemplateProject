using System;
using Common.Session.States;
using UnityEngine;
using Zenject;

namespace Common.Session
{
   internal class Session : ISession, IInitializable
   {

      public event Action<ISessionState> OnStateChanged;
      
      [Inject] private DiContainer _container;

      private ISessionStateInternal _currentState;

      public ISessionState CurrentState => _currentState;

      public void Initialize()
      {
         ChangeState<InitState>();
      }
      

      internal void ChangeState<TState>() where TState : ISessionStateInternal
      {
         var newState = _container.Resolve<TState>();
         ChangeState(newState);
      }
      
      private void ChangeState(ISessionStateInternal newState)
      {
         if (_currentState != null)
         {
            _currentState.Exit();
         }
            
         _currentState = newState;
         _currentState.Enter();
            
         OnStateChanged?.Invoke(_currentState);
      }

     
   }
}