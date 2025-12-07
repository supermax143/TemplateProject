using System;
using System.Collections.Generic;
using System.Linq;
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
	public class WindowsController : MonoBehaviour, IWindowsMemberHolder
    {

        [SerializeField] private Transform _windowsParent;

        [Inject] private readonly DiContainer _diContainer;
        private readonly List<string> _loadingWindows = new();


        private readonly List<WindowsListMember> _windowsList = new();

        void IWindowsMemberHolder.OnWindowRemoved(WindowsListMember member)
        {
            if (!_windowsList.Any())
            {
                Debug.LogError("Calling OnWindowRemoved but windowsList is empty");
                return;
            }

            var isLastMember = _windowsList.Last() == member;

            _windowsList.Remove(member);
            AddressableExtention.ReleaseTag(member.WindowName);

            var isWindowsListEmpty = !_windowsList.Any();
            if (isLastMember) OnActiveWindowChanged?.Invoke();

            if (isWindowsListEmpty && !_loadingWindows.Any()) OnLastWindowClosed?.Invoke();
            OnAnyWindowClosed?.Invoke(member.WindowName);
        }

        public event Action<string> OnWindowStartLoading;
        public event Action OnActiveWindowChanged;
        public event Action OnWindowShown;
        public event Action<string> OnAnyWindowClosed;
        public event Action OnLastWindowClosed;



        private void ShowWindowAsync(Type windowType, Action<GameObject> initCallback)
        {
            if (!WindowAttribute.TryGetName(windowType, out var windowName))
            {
                Debug.LogError($"Can't find Window attribute on Type {windowType.Name}");
                return;
            }

            LoadWindow(windowName, initCallback);
        }

        private async void LoadWindow(string windowName, Action<GameObject> callback)
        {
            _loadingWindows.Add(windowName);
            OnWindowStartLoading?.Invoke(windowName);

            var windowPrefab = await AddressableExtention.Load<GameObject>(windowName, GetWindowUnloadTag(windowName));
            if (this == null) return;

            callback?.Invoke(InitializeInstance(windowPrefab, windowName));
        }

        private GameObject InitializeInstance(GameObject windowPrefab, string windowName)
        {
            var window = _diContainer.InstantiatePrefab(windowPrefab);

            var windowsListMember = window.gameObject.AddComponent<WindowsListMember>();
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