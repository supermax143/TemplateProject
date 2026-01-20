using DefaultNamespace;
using Localization;
using Windows;
using ResourceManager.Runtime;
using Session;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

namespace Installers
{
   public class ProjectInstaller : MonoInstaller
   {

      [SerializeField, HideInInspector]
      private GlobalSession globalSession;
      [SerializeField, HideInInspector]
      private WindowsController _windowsController;
      
      [Header("Localization")]
      [SerializeField]
      private string _localizationAddressKey = "Localization";
      
      private void OnValidate()
      {
         globalSession = GetComponent<GlobalSession>();
         _windowsController = GetComponent<WindowsController>();
      }


      public override void InstallBindings()
      {
         InitializeAddressables();

         var localizationController = InitializeLocalization();
         
         Container.BindInstance(globalSession).AsSingle();
         Container.BindInterfacesAndSelfTo<ScenesLoader>().AsSingle();
         Container.BindInstance(_windowsController).AsSingle();
         Container.BindInstance(localizationController).AsSingle();
      }

      private static void InitializeAddressables()
      {
         var handleStorage = new HandleStorage();
         AsyncOpHandleExtension.Initialize(handleStorage);
         AddressableExtention.Initialize(handleStorage);
      }
      
      private LocalizationController InitializeLocalization()
      {
         var controller = new LocalizationController();
         
         // Загружаем CSV через Addressables асинхронно, инициализируем контроллер при завершении.
         // Важно: пока файл не загружен, Get(key) будет возвращать ключи.
         if (!string.IsNullOrEmpty(_localizationAddressKey))
         {
            Addressables.LoadAssetAsync<TextAsset>(_localizationAddressKey)
               .Completed += handle =>
               {
                  if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
                  {
                     controller.Initialize(handle.Result.text);
                  }
                  else
                  {
                     Debug.LogError($"[ProjectInstaller] Не удалось загрузить файл локализации по адресу '{_localizationAddressKey}'.");
                  }
               };
         }
         else
         {
            Debug.LogWarning("[ProjectInstaller] Пустой ключ адреса для локализации. Файл не будет загружен.");
         }

         return controller;
      }


   }
}