using GUI.Windows;
using UnityEngine;
using Zenject;

namespace Windows
{
    /// <summary>
    /// Пример реализации window'а.
    /// Показывает, как создать собственный window и использовать WindowManager.
    /// </summary>
    [Window(nameof(ExampleWindow))]
    public class ExampleWindow : WindowBase
    {
        [Inject] private WindowsController _windowsController;

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

