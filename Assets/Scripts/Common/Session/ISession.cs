using Common.Session.States;

namespace Common.Session
{
   public interface ISession
   {
      ISessionState CurrentState { get; }
   }
}