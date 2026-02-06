using Common.Scenes;
using Zenject;

namespace Common.Session.States
{
   internal class MainMenuState : SessionStateBase
   {

      [Inject] IScenesLoader _scenesLoader;
      
      protected override void OnStateEnter()
      {
         _scenesLoader.LoadMainMenuScene();
      }

      public override void StartGame() 
      {
         ApplicationSession.ChangeState<GameState>();
      }

   }
}