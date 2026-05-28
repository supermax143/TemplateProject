using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Interfaces;
using Core.Domain.Services;
using Zenject;

namespace Unity.Bootstrap.GameInitializer
{
   internal class GameBootrstarp : IBootstrapProgress, IGameBootstrap
   {
      [Inject] private ILocalization _localization;
    
      public event Action OnStepStarted;
      public event Action OnInitializationComplete;


      public string CurStepIdent { get; private set; } = "";
      public float Progress { get; private set; } = 0;
      
      
      private readonly Queue<BootstrapStepWrapper> _steps = new();
      
      
      public async Task Initialize()
      {
         AddSteps();

         
         for (int i = 0; i < _steps.Count; i++)
         {
            var step = _steps.Dequeue();
            CurStepIdent = step.StepIdent;
            OnStepStarted?.Invoke();
            await step.Execute();
            Progress = (float)(i+1) / _steps.Count;
            await Task.Delay(1000);
         }
         
         OnInitializationComplete?.Invoke();
      }

     
      private void AddSteps()
      {
           _steps.Enqueue(new BootstrapStepWrapper(_localization, "init_localization"));
      }
      
      
   }
}