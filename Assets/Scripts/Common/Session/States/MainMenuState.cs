using Common.Scenes;
using Zenject;

namespace Common.Session.States
{
   public class MainMenuState : GlobalSessionStateBase
   {

      [Inject] ScenesLoader _scenesLoader;
      
      protected override void OnStateEnter()
      {
         _scenesLoader.LoadMainMenuScene();
      }

      public override void StartGame() 
      {
         _globalSession.SetState<GameState>();
      }

   }
}