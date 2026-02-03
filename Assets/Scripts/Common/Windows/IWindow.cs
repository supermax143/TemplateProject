using System;
using UnityEngine;

namespace Windows
{
	/// <summary>
	///     Интерфейс для window'ов, которые могут управляться через WindowManager
	/// </summary>
	public interface IWindow
	{
		GameObject GameObject { get; }
		void Show();

		void Hide();
		event Action<IWindow> OnShow;
		event Action<IWindow> OnHide;
	}
}