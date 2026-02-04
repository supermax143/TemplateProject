using System.Threading.Tasks;
using XDiffGui;

namespace Common.Game.States
{
   internal abstract class InitializeStateBase
   {
      public abstract string Ident { get; }
      
      public abstract Task Execute();
      
   }
}