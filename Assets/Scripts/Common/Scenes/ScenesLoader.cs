using UnityEngine.SceneManagement;
using Zenject;

namespace DefaultNamespace
{
   public class ScenesLoader  
   {
      [Inject] private ZenjectSceneLoader _sceneLoader;
      
      
      public string CurScene => SceneManager.GetActiveScene().name;


      public void LoadMainMenuScene() => LoadScene(SceneNames.MainMenuScene);
      public void LoadGameScene() => LoadScene(SceneNames.GameScene);
      
      private void LoadScene(string scene)
      {
         if (CurScene == scene)
         {
            return;
         }
         _sceneLoader.LoadScene(scene);
      }
      

   }
}