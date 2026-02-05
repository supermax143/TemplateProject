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

      private ISessionState _currentState;

      public ISessionState CurrentState => _currentState;

      public void Initialize()
      {
         ChangeState<InitState>();
      }
      

      public void ChangeState<TState>() where TState : ISessionState
      {
         var newState = _container.Resolve<TState>();
         ChangeState(newState);
      }
      
      public void ChangeState(ISessionState newState)
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