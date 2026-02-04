using System;

namespace Common.Game
{
   public interface IInitializeProgress
   {
      event Action OnInitStepComplete;
      string CurStateIdent { get; }
      float Progress { get; }
   }
}