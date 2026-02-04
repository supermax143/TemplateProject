using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Common.Localization;
using Common.Game.States;
using UnityEngine;
using Zenject;

namespace Common.Game
{
   internal class GameInitializer : IInitializable, IInitializeProgress
   {
      [Inject] private LocalizationController _localizationController;
      [Inject] private DiContainer _container;
    
      public event Action OnInitStepComplete;
      
      private readonly List<InitializeStateBase> _states = new List<InitializeStateBase>();

      private InitializeStateBase _curState;
      
      public string CurStateIdent => _curState?.Ident ?? "";
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
            _curState = _states[i];
            await _curState.Execute();
            OnInitStepComplete?.Invoke();
            UpdateProgress(i+1);
         }
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