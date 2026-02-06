using Common.Session.States;

namespace Common.Session
{
   public interface IApplicationSession
   {
      ISessionState CurrentState { get; }
   }
}