using DefaultNamespace;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Common.Session.States {
	public class GameState : GlobalSessionStateBase
	{
		
		[Inject] ScenesLoader _scenesLoader;
		
		protected override void OnStateEnter()
		{
			_scenesLoader.LoadGameScene();
		}
	}
}