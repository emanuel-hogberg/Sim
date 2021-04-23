using Sim.Models;

namespace Sim.Interfaces
{
    public interface IRepositoryService
    {
        Agent LoadOrCreateAgent(string agentName);
        void SaveAgent(Agent agent);
    }
}
