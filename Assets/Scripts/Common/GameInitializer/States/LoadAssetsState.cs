using System.Threading.Tasks;

namespace Common.GameInitializer.States
{
   internal class LoadAssetsState : InitializeStateBase
   {
      public override string Ident => "load_assets";
      
      public override async Task Execute()
      {
         await Task.Delay(3000);//TODO: заглушка
      }
   }
}