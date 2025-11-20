using System;
using UnityEngine;
using Zenject;

namespace Windows
{
    /// <summary>
    /// Менеджер для управления window'ами.
    /// Обеспечивает, что единовременно может быть открыт только один window.
    /// Window'ы могут находиться на разных сценах.
    /// </summary>
    public class WindowsController : MonoBehaviour
    {
        private IWindow _currentWindow;
        
        public IWindow CurrentWindow => _currentWindow;
        
        public bool HasActiveWindow => _currentWindow != null && _currentWindow.IsActive;

        public event Action<IWindow> OnWindowOpened;
        
        public event Action<IWindow> OnWindowClosed;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void ShowWindow(IWindow window)
        {
            if (window == null)
            {
                Debug.LogWarning("[WindowManager] Попытка показать null window");
                return;
            }

            if (_currentWindow != null && _currentWindow.IsActive)
            {
                CloseCurrentWindow();
            }

            _currentWindow = window;
            window.Show();
            OnWindowOpened?.Invoke(window);
        }

        public void CloseCurrentWindow()
        {
            if (_currentWindow != null && _currentWindow.IsActive)
            {
                var windowToClose = _currentWindow;
                _currentWindow = null;
                windowToClose.Hide();
                OnWindowClosed?.Invoke(windowToClose);
            }
        }

        public void CloseWindow(IWindow window)
        {
            if (window == null)
            {
                return;
            }

            if (_currentWindow == window && window.IsActive)
            {
                CloseCurrentWindow();
            }
        }

        public bool IsCurrentWindow(IWindow window)
        {
            return _currentWindow == window && window != null && window.IsActive;
        }

        private void OnDestroy()
        {
            if (_currentWindow != null && _currentWindow.IsActive)
            {
                _currentWindow.Hide();
            }
        }
    }
}

