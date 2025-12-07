using GUI.Windows;
using UnityEngine;
using Zenject;

namespace Windows
{
	[Window(nameof(ExampleWindow))]
	public class ExampleWindow : WindowBase
	{

		protected override void OnShow()
		{
			base.OnShow();
			Debug.Log("[ExampleWindow] Window показан");
		}

		protected override void OnHide()
		{
			base.OnHide();
			Debug.Log("[ExampleWindow] Window скрыт");
		}
	}
}