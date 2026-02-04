using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.GameInitializer.States;
using Common.Localization;
using Zenject;

namespace Common.GameInitializer
{
   internal class GameInitializer : IInitializable, IInitializeProgress
   {
      [Inject] private LocalizationController _localizationController;
      [Inject] private DiContainer _container;
    
      public event Action OnInitStepStarted;
      public event Action OnInitializationComplete;
      
      private readonly List<InitializeStateBase> _states = new List<InitializeStateBase>();

      private InitializeStateBase _curState;
      
      public string CurStepIdent => _curState?.Ident ?? "";
      public float Progress { get; private set; }
      
      
      public void Initialize()
      {
         _states.Add(_container.Instantiate<InitLocalizationState>());
         _states.Add(_container.Instantiate<LoginState>());
         _states.Add(_container.Instantiate<LoadAssetsState>());
      }
      
      public async Task InitializeGame()
      {
         for (int i = 0; i < _states.Count; i++)
         {
            UpdateProgress(i);
            _curState = _states[i];
            OnInitStepStarted?.Invoke();
            await _curState.Execute();
         }
         UpdateProgress(_states.Count);
         _curState = null;
         OnInitializationComplete?.Invoke();
         await Task.Delay(1000);
      }

      private void UpdateProgress(int index)
      {
         if (index == 0)
         {
            Progress = 0;
            return;
         }
         Progress = (float)index / _states.Count;
      }
   }
}