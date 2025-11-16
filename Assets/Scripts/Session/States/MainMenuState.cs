using System.Collections;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Session.States
{
   public class MainMenuState : GlobalSessionStateBase
   {

      [Inject] ScenesLoader _scenesLoader;
      
      private MainMenu _mainMenu = null;
      
      protected override void OnStateEnter()
      {
         _mainMenu = FindObjectOfType<MainMenu>();   
         _mainMenu.OnStartGame += CreateGame;
      }

      private void CreateGame()
      {
         Unsign();
         _scenesLoader.LoadGameScene();
         StartCoroutine(WaitSceneLoaded());
      }

      private IEnumerator WaitSceneLoaded() 
      {
         if (_scenesLoader.CurScene != SceneNames.GameScene)
         {
            yield return null;
         }
         _globalSession.SetState<GameState>();
      }

      private void Unsign()
      {
         _mainMenu.OnStartGame -= CreateGame;
      }
   }
}