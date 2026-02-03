using System;
using Windows;

namespace Common.Windows
{
   public interface IWindowsController
   {
      void ShowWindowAsync<TWindow>(Action<IWindow> initCallback) where TWindow : IWindow;
   }
}