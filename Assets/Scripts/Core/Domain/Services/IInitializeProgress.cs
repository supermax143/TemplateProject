using System;

namespace Common.GameInitializer
{
   public interface IInitializeProgress
   {
      event Action OnInitializationComplete;
      string CurStepIdent { get; }
      float Progress { get; }
      event Action OnInitStepStarted;
   }
}