using UnityEngine;

namespace Windows {
    /// <summary>
    ///     Интерфейс для window'ов, которые могут управляться через WindowManager
    /// </summary>
    public interface IWindow {
		bool IsActive { get; }

		GameObject GameObject { get; }
		void Show();

		void Hide();
	}
}