using System;
using Cysharp.Threading.Tasks;

namespace Common.Windows
{
   public interface IWindowsController
   {
      UniTask<IWindow> ShowWindow<TWindow>() where TWindow : IWindow;
      void ShowWindow<TWindow>(Action<IWindow> handler) where TWindow : IWindow;
   }
}