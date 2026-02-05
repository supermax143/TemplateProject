using System;

namespace Common.Session.States
{
   public interface ISessionState
   {
      void Enter();
      void Exit();
      void StartGame();
   }
   
}

