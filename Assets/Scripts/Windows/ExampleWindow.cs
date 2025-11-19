using UnityEngine;
using Zenject;

namespace Windows
{
    /// <summary>
    /// Пример реализации window'а.
    /// Показывает, как создать собственный window и использовать WindowManager.
    /// </summary>
    public class ExampleWindow : WindowBase
    {
        [Inject] private WindowManager _windowManager;

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

        public void ShowViaManager()
        {
            if (_windowManager != null)
            {
                _windowManager.ShowWindow(this);
            }
            else
            {
                Debug.LogWarning("[ExampleWindow] WindowManager не найден");
            }
        }

        public void CloseViaManager()
        {
            if (_windowManager != null)
            {
                _windowManager.CloseWindow(this);
            }
        }
    }
}

