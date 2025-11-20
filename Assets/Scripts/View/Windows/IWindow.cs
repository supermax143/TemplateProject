using UnityEngine;

namespace Windows
{
    /// <summary>
    /// Интерфейс для window'ов, которые могут управляться через WindowManager
    /// </summary>
    public interface IWindow
    {
        void Show();
        
        void Hide();
        
        bool IsActive { get; }
        
        GameObject GameObject { get; }
    }
}

