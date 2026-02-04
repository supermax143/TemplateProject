using System.Threading.Tasks;

namespace Common.GameInitializer.States
{
   internal class LoginState : InitializeStateBase
   {
      public override string Ident => "login_state";
      
      public override async Task Execute()
      {
         await Task.Delay(3000);//TODO: заглушка
      }
   }
}