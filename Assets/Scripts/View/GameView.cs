using Common.Windows;
using Cysharp.Threading.Tasks;
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
           _windowsController.ShowWindow<ExampleWindow>( window => window.Show());
           
        }
    }
}