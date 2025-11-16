using System.Collections;
using DefaultNamespace;
using Zenject;

namespace Session.States
{
   public class InitState : GlobalSessionStateBase
   {
      [Inject] private ScenesLoader _scenesLoader;
      
      protected override void OnStateEnter()
      {
         _globalSession.SetState<MainMenuState>();//пока заглушка
      }
      
   }
}