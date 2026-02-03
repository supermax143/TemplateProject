using Windows;
using UnityEngine;
using Zenject;

namespace View
{
    public class GameView : MonoBehaviour
    {
        [Inject] private WindowsController _windowsController;
        
        public void ShowExampleWindow()
        {
            _windowsController.ShowWindowAsync<ExampleWindow>( window => window.Show());
        }
    }
}