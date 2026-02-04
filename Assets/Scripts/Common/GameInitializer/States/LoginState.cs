using System.Threading.Tasks;

namespace Common.Game.States
{
   internal class LoginState : InitializeStateBase
   {
      public override string Ident => "login_state";
      
      public override async Task Execute()
      {
         await Task.Delay(1000);//TODO: заглушка
      }
   }
}