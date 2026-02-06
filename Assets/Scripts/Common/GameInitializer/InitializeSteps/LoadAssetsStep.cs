using System.Threading.Tasks;

namespace Common.GameInitializer.States
{
   internal class LoadAssetsStep : InitializeStepBase
   {
      public override string Ident => "load_assets";
      
      public override async Task Execute()
      {
         await Task.Delay(1000);//TODO: заглушка
      }
   }
}