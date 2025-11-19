using UnityEngine;
using Zenject;

namespace Popups
{
    /// <summary>
    /// Пример реализации popup'а.
    /// Показывает, как создать собственный popup и использовать PopupManager.
    /// </summary>
    public class ExamplePopup : PopupBase
    {
        [Inject] private PopupManager _popupManager;

        protected override void OnShow()
        {
            base.OnShow();
            Debug.Log("[ExamplePopup] Popup показан");
        }

        protected override void OnHide()
        {
            base.OnHide();
            Debug.Log("[ExamplePopup] Popup скрыт");
        }

        public void ShowViaManager()
        {
            if (_popupManager != null)
            {
                _popupManager.ShowPopup(this);
            }
            else
            {
                Debug.LogWarning("[ExamplePopup] PopupManager не найден");
            }
        }

        public void CloseViaManager()
        {
            if (_popupManager != null)
            {
                _popupManager.ClosePopup(this);
            }
        }
    }
}

