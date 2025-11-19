using System;
using UnityEngine;
using Zenject;

namespace Popups
{
    /// <summary>
    /// Менеджер для управления popup'ами.
    /// Обеспечивает, что единовременно может быть открыт только один popup.
    /// Popup'ы могут находиться на разных сценах.
    /// </summary>
    public class PopupManager : MonoBehaviour
    {
        private IPopup _currentPopup;
        
        public IPopup CurrentPopup => _currentPopup;
        
        public bool HasActivePopup => _currentPopup != null && _currentPopup.IsActive;

        public event Action<IPopup> OnPopupOpened;
        
        public event Action<IPopup> OnPopupClosed;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void ShowPopup(IPopup popup)
        {
            if (popup == null)
            {
                Debug.LogWarning("[PopupManager] Попытка показать null popup");
                return;
            }

            if (_currentPopup != null && _currentPopup.IsActive)
            {
                CloseCurrentPopup();
            }

            _currentPopup = popup;
            popup.Show();
            OnPopupOpened?.Invoke(popup);
        }

        public void CloseCurrentPopup()
        {
            if (_currentPopup != null && _currentPopup.IsActive)
            {
                var popupToClose = _currentPopup;
                _currentPopup = null;
                popupToClose.Hide();
                OnPopupClosed?.Invoke(popupToClose);
            }
        }

        public void ClosePopup(IPopup popup)
        {
            if (popup == null)
            {
                return;
            }

            if (_currentPopup == popup && popup.IsActive)
            {
                CloseCurrentPopup();
            }
        }

        public bool IsCurrentPopup(IPopup popup)
        {
            return _currentPopup == popup && popup != null && popup.IsActive;
        }

        private void OnDestroy()
        {
            if (_currentPopup != null && _currentPopup.IsActive)
            {
                _currentPopup.Hide();
            }
        }
    }
}

