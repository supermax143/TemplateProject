using Common.Windows;
using UnityEngine;
using View.Windows;
using Zenject;

namespace View
{
    public class GameView : MonoBehaviour
    {
        [Inject] private IWindowsController _windowsController;
        
        public void ShowExampleWindow()
        {
            _windowsController.ShowWindowAsync<ExampleWindow>( window => window.Show());
        }
    }
}