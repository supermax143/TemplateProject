using Common.Scenes;
using Zenject;

namespace Common.Session.States {
	internal class GameState : GlobalSessionStateBase
	{
		
		[Inject] ScenesLoader _scenesLoader;
		
		protected override void OnStateEnter()
		{
			_scenesLoader.LoadGameScene();
		}
	}
}