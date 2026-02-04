using System.Threading.Tasks;

namespace Common.GameInitializer.States
{
   internal abstract class InitializeStateBase
   {
      public abstract string Ident { get; }
      
      public abstract Task Execute();
      
   }
}