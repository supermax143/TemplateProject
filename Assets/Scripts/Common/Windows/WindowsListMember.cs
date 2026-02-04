using UnityEngine;

namespace Common.Windows
{
	internal interface IWindowsMemberHolder
	{
		void OnWindowRemoved(WindowsListMember member);
	}

	internal sealed class WindowsListMember : MonoBehaviour
	{
		private IWindowsMemberHolder _windowsController;

		public bool UseDefaultBackground { get; set; }
		public string WindowName { get; private set; }
		public IWindow Window { get; private set; }


		private void OnDestroy()
		{
			if (_windowsController != null) _windowsController.OnWindowRemoved(this);
		}

		internal void Initialize(IWindowsMemberHolder windowsController, string windowName)
		{
			_windowsController = windowsController;
			WindowName = windowName;

			Window = gameObject.GetComponent<IWindow>();
		}
	}
}