using Session;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
	[Inject] private GlobalSession _globalSession;

	public void StartGame()
	{
		_globalSession.CurState.StartGame();
	}
}