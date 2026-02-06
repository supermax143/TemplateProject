using Common.Scenes;
using Zenject;

namespace Common.Session.States {
	internal class GameState : SessionStateBase
	{
		
		[Inject] IScenesLoader _scenesLoader;
		
		protected override void OnStateEnter()
		{
			_scenesLoader.LoadGameScene();
		}
	}
}