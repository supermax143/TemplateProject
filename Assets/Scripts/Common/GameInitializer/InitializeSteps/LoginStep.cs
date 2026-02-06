using System.Threading.Tasks;

namespace Common.GameInitializer.States
{
   internal class LoginStep : InitializeStepBase
   {
      public override string Ident => "login_state";
      
      public override async Task Execute()
      {
         await Task.Delay(1000);//TODO: заглушка
      }
   }
}