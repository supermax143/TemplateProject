using System.Threading.Tasks;

namespace Common.Game.States
{
   internal class LoadAssetsState : InitializeStateBase
   {
      public override string Ident => "load_assets";
      
      public override async Task Execute()
      {
         await Task.Delay(1000);//TODO: заглушка
      }
   }
}