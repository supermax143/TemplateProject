using System.Threading.Tasks;

namespace Common.Scenes
{
   internal interface IScenesLoader
   {
      Task  LoadInitGameScene();
      Task  LoadMainMenuScene();
      Task  LoadGameScene();
      string CurScene { get; }
   }
}