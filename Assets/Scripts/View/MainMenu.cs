using Common.Session;
using UnityEngine;
using Zenject;

namespace View
{
	public class MainMenu : MonoBehaviour
	{
		[Inject] private GlobalSession _globalSession;

		public void StartGame()
		{
			_globalSession.CurState.StartGame();
		}
	}
}