using System.Collections;
using DefaultNamespace;
using Zenject;

namespace Session.States
{
   public class StartState : GlobalSessionStateBase
   {
      [Inject] private ScenesLoader _scenesLoader;
      
      protected override void OnStateEnter()
      {
         if (_scenesLoader.CurScene != SceneNames.MainMenuScene)
         {
            _scenesLoader.LoadMainMenuScene();
         }

         StartCoroutine(CheckCurScene());
      }

      public IEnumerator CheckCurScene()
      {
         if (_scenesLoader.CurScene != SceneNames.MainMenuScene)
         {
            yield return null;
         }
         _globalSession.SetState<MainMenuState>();
      }
      
   }
}