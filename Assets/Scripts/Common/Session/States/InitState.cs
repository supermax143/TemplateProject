using Common.Scenes;
using Zenject;

namespace Common.Session.States
{
   public class InitState : GlobalSessionStateBase
   {
      [Inject] private ScenesLoader _scenesLoader;
      [Inject] private GameInitializer.GameInitializer _gameInitializer;
      
      protected override async void OnStateEnter()
      {
         if (_scenesLoader.CurScene != SceneNames.InitGameScene)
         {
            await _scenesLoader.LoadInitGameScene();
         }
         await _gameInitializer.InitializeGame();
         _globalSession.SetState<MainMenuState>();
      }
      
   }
}