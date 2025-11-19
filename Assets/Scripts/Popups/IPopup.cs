using UnityEngine;

namespace Popups
{
    /// <summary>
    /// Интерфейс для popup'ов, которые могут управляться через PopupManager
    /// </summary>
    public interface IPopup
    {
        void Show();
        
        void Hide();
        
        bool IsActive { get; }
        
        GameObject GameObject { get; }
    }
}

