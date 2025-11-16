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