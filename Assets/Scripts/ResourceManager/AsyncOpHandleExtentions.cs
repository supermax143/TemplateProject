using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ResourceManager.Runtime
{
    public static class AsyncOpHandleExtension
    {
        private static HandleStorage HandleStorage => HandleStorageSingleton.Instance;

        public static Task<T> LoadAndRegisterHandle<T>(this AsyncOperationHandle<T> handle, string tag)
            => HandleStorage.RegisterHandle(ref handle, tag);

        public static Task<T> LoadAndRegisterHandle<T>(this AsyncOperationHandle<T> handle, GameObject link)
            => HandleStorage.RegisterHandle(ref handle, link);
    }
}