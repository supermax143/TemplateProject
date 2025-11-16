using UnityEngine;

namespace Session.States {
	public class GameState : GlobalSessionStateBase
	{
		protected override void OnStateEnter() {
			Debug.Log("OnStateEnter GameState");
		}
	}
}