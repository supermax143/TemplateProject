using System;
using System.Collections.Generic;
using System.Linq;
using Common.Windows;
using GUI.Windows;
using ResourceManager.Runtime;
using UnityEngine;
using Zenject;

namespace Windows
{
	/// <summary>
	///     Менеджер для управления window'ами.
	///     Обеспечивает, что единовременно может быть открыт только один window.
	///     Window'ы могут находиться на разных сценах.
	/// </summary>
	internal class WindowsController : MonoBehaviour, IWindowsMemberHolder, IWindowsController
    {

        [SerializeField] private Transform _windowsParent;

        [Inject] private readonly DiContainer _diContainer;
        private readonly List<string> _loadingWindows = new();


        private readonly List<WindowsListMember> _windowsList = new();

        

        public event Action<string> OnWindowStartLoading;
        public event Action OnActiveWindowChanged;
        public event Action OnWindowShown;
        public event Action<string> OnAnyWindowClosed;
        public event Action OnLastWindowClosed;


        void IWindowsMemberHolder.OnWindowRemoved(WindowsListMember member)
        {
            if (!_windowsList.Any())
            {
                Debug.LogError("Calling OnWindowRemoved but windowsList is empty");
                return;
            }

            var isLastMember = _windowsList.Last() == member;

            _windowsList.Remove(member);
            AddressableExtention.ReleaseTag(GetWindowUnloadTag(member.WindowName));

            var isWindowsListEmpty = !_windowsList.Any();
            if (isLastMember) OnActiveWindowChanged?.Invoke();

            if (isWindowsListEmpty && !_loadingWindows.Any()) OnLastWindowClosed?.Invoke();
            OnAnyWindowClosed?.Invoke(member.WindowName);
        }
        
        public void ShowWindowAsync<TWindow>(Action<IWindow> initCallback) where TWindow : IWindow
        {
            ShowWindowAsync(typeof(TWindow), initCallback);
        }
        

        public void ShowWindowAsync(Type windowType, Action<IWindow> initCallback)
        {
            if (!WindowAttribute.TryGetName(windowType, out var windowName))
            {
                Debug.LogError($"Can't find Window attribute on Type {windowType.Name}");
                return;
            }

            LoadWindow(windowName, initCallback);
        }

        private async void LoadWindow(string windowName, Action<IWindow> callback)
        {
            _loadingWindows.Add(windowName);
            OnWindowStartLoading?.Invoke(windowName);

            var windowPrefab = await AddressableExtention.Load<GameObject>(windowName, GetWindowUnloadTag(windowName));

            callback?.Invoke(InitializeInstance(windowPrefab, windowName));
        }

        private IWindow InitializeInstance(GameObject windowPrefab, string windowName)
        {
            var window = _diContainer.InstantiatePrefabForComponent<IWindow>(windowPrefab, _windowsParent);
            var windowsListMember = window.GameObject.AddComponent<WindowsListMember>();
            windowsListMember.Initialize(this, windowName);

            _loadingWindows.Remove(windowsListMember.WindowName);

            ShowWindowInternal(windowsListMember);

            _windowsList.Add(windowsListMember);


            OnWindowShown?.Invoke();

            return window;
        }

        private void ShowWindowInternal(WindowsListMember member)
        {
            OnActiveWindowChanged?.Invoke();

            var animator = member.GetComponent<Animator>();
            if (animator != null)
                animator.Update(0);

            var rectTransform = member.GetComponent<RectTransform>();
            rectTransform.SetParent(_windowsParent, false);
        }

        private string GetWindowUnloadTag(string windowName) => $"{windowName}-{GetHashCode()}";
    }
}