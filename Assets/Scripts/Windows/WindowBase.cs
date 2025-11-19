using UnityEngine;

namespace Windows
{
    /// <summary>
    /// Базовый класс для window'ов, реализующий интерфейс IWindow.
    /// Наследуйте от этого класса для создания собственных window'ов.
    /// </summary>
    public abstract class WindowBase : MonoBehaviour, IWindow
    {
        [SerializeField] private GameObject windowRoot;
        
        private bool _isActive;

        protected virtual void Awake()
        {
            if (windowRoot == null)
            {
                windowRoot = gameObject;
            }
            
            Hide();
        }

        public virtual void Show()
        {
            if (windowRoot != null)
            {
                windowRoot.SetActive(true);
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
            if (windowRoot != null)
            {
                windowRoot.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
            
            _isActive = false;
            OnHide();
        }

        public bool IsActive => _isActive && (windowRoot != null ? windowRoot.activeSelf : gameObject.activeSelf);

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

