using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ResourceManager.Runtime
{
    public static class AsyncOpHandleExtension
    {
        private static HandleStorage _handleStorage;

        public static void Initialize(HandleStorage handleStorage)
        {
            _handleStorage = handleStorage;
        }
        
        public static Task<T> LoadAndRegisterHandle<T>(this AsyncOperationHandle<T> handle, HandleDependencies handleDependencies)
            => _handleStorage.RegisterHandle(ref handle, handleDependencies);
        
        public static Task<T> LoadAndRegisterHandle<T>(this AsyncOperationHandle<T> handle, string tag)
            => _handleStorage.RegisterHandle(ref handle, tag);

        public static Task<T> LoadAndRegisterHandle<T>(this AsyncOperationHandle<T> handle, GameObject link)
            => _handleStorage.RegisterHandle(ref handle, link);
    }
}