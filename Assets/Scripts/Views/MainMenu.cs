using System;
using Session;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
	
	public Action OnStartGame;
	
	public void StartGame() => OnStartGame?.Invoke(); 
	
}
