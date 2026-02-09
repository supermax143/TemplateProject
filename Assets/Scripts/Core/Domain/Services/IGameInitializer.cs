using System.Threading.Tasks;

namespace Core.Domain.Services
{
   internal interface IGameInitializer
   {
      Task Start();
   }
}