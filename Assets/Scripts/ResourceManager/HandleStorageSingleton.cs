using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ResourceManager.Runtime
{
    internal static class HandleStorageSingleton
    {
        private static HandleStorage s_handleStorage;
        private static Task s_initTask;

        public static HandleStorage Instance => s_handleStorage;
        public static Task InitTask => s_initTask;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            s_handleStorage?.Dispose();
            s_handleStorage = new HandleStorage();
            s_initTask = Addressables.InitializeAsync().Task;
        }
    }
}