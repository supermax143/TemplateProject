using UnityEngine;

namespace Popups
{
    /// <summary>
    /// Базовый класс для popup'ов, реализующий интерфейс IPopup.
    /// Наследуйте от этого класса для создания собственных popup'ов.
    /// </summary>
    public abstract class PopupBase : MonoBehaviour, IPopup
    {
        [SerializeField] private GameObject popupRoot;
        
        private bool _isActive;

        protected virtual void Awake()
        {
            if (popupRoot == null)
            {
                popupRoot = gameObject;
            }
            
            Hide();
        }

        public virtual void Show()
        {
            if (popupRoot != null)
            {
                popupRoot.SetActive(true);
            }
            else
            {
                gameObject.SetActive(true);
            }
            
            _isActive = true;
            OnShow();
        }

        public virtual void Hide()
        {
            if (popupRoot != null)
            {
                popupRoot.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
            
            _isActive = false;
            OnHide();
        }

        public bool IsActive => _isActive && (popupRoot != null ? popupRoot.activeSelf : gameObject.activeSelf);

        public GameObject GameObject => gameObject;

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        public void Close()
        {
            Hide();
        }
    }
}

