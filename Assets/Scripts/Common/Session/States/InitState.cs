using System.Collections;
using Assets.Scripts.Common.Localization;
using Common.Game;
using DefaultNamespace;
using Zenject;

namespace Assets.Scripts.Common.Session.States
{
   public class InitState : GlobalSessionStateBase
   {
      [Inject] private ScenesLoader _scenesLoader;
      [Inject] private GameInitializer _gameInitializer;
      
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