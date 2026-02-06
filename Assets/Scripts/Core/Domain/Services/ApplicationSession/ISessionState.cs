using System;

namespace Common.Session.States
{
   public interface ISessionState
   {
      void StartGame();
   }
   
   internal interface ISessionStateInternal : ISessionState
   {
      void Enter();
      void Exit();
   }
   
}

