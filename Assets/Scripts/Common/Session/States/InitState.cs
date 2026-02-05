using System;
using Common.Scenes;
using Zenject;

namespace Common.Session.States
{
   internal class InitState : SessionStateBase
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
         Session.ChangeState<MainMenuState>();
      }
      
   }
}