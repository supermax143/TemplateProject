using System;
using Common.Session.States;
using UnityEngine;
using Zenject;

namespace Common.Session
{
   public class Session : MonoBehaviour
   {

      public event Action<ISessionState> OnStateChanged;
      
      [Inject] private DiContainer _container;

      private ISessionState _currentState;

      public ISessionState CurrentState => _currentState;

      private void Start()
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