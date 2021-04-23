using Sim.Models;

namespace Sim.Interfaces
{
    public interface IAgentService
    {
        void EnsureInitialized(Agent agent);
        void Act(Agent agent);
    }
}
