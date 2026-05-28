using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.DataStorage;
using Core.Application.Interfaces;
using Unity.Infrastructure.Localization;
using Unity.Infrastructure.VisualTutorial;
using Zenject;

namespace Unity.Bootstrap
{
   internal class GameBootrstarp : IBootstrapProgress, IGameBootstrap
   {
      [Inject] private LocalizationController _localization;
      [Inject] private IDataStorage _dataStorage;
      [Inject] private TutorialController _tutorialController;
      
      public event Action OnStepStarted;
      public event Action OnInitializationComplete;


      public string CurStepIdent { get; private set; } = "";
      public float Progress { get; private set; } = 0;
      
      
      private readonly Queue<BootstrapStepWrapper> _steps = new();
      
      
      public async Task Initialize()
      {
         AddSteps();

         int index = 1;
         while (_steps.Count > 0)
         {
            var step = _steps.Dequeue();
            CurStepIdent = step.StepIdent;
            OnStepStarted?.Invoke();
            await step.Execute();
            Progress = (float)index / _steps.Count;
            index++;
            await Task.Delay(500);
         }
         
         OnInitializationComplete?.Invoke();
      }

     
      private void AddSteps()
      {
           _steps.Enqueue(new BootstrapStepWrapper(_localization, "init_localization"));
           _steps.Enqueue(new BootstrapStepWrapper(_dataStorage, "init_data_storage"));
           _steps.Enqueue(new BootstrapStepWrapper(_tutorialController, "init_tutorial"));
      }
      
      
   }
}