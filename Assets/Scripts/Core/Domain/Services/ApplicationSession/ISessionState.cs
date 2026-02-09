namespace Core.Domain.Services.ApplicationSession
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

