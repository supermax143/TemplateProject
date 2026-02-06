using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.GameInitializer.States;
using Common.Localization;
using Zenject;

namespace Common.GameInitializer
{
   internal class GameInitializer : IInitializeProgress
   {
      [Inject] private LocalizationController _localizationController;
      [Inject] private DiContainer _container;
    
      public event Action OnInitStepStarted;
      public event Action OnInitializationComplete;
      
      private List<InitializeStepBase> _states;
      private InitializeStepBase _curStep;
      
      public string CurStepIdent => _curStep?.Ident ?? "";
      public float Progress { get; private set; }
      
      
      public async Task Start()
      {
         
         _states = _container.ResolveAll<InitializeStepBase>();
         
         for (int i = 0; i < _states.Count; i++)
         {
            UpdateProgress(i);
            _curStep = _states[i];
            OnInitStepStarted?.Invoke();
            await _curStep.Execute();
         }
         UpdateProgress(_states.Count);
         _curStep = null;
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