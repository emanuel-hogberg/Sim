using Sim.Models;

namespace Sim.Interfaces
{
    public interface IAgentService
    {
        void EnsureInitialized(Agent agent);
        void MakePurchase(Agent agent);
    }
}
