using Sim.Interfaces;
using Sim.Models;
using System.IO;
using System.Text.Json;

namespace Sim.Services
{
    public class JsonRepositoryService : IRepositoryService
    {
        private const string RepositoryFolder = "Repository";

        public Agent LoadOrCreateAgent(string agentName)
        {
            var path = GetFilePath(agentName);
            if (!File.Exists(path))
            {
                return new Agent
                {
                    Name = agentName
                };
            }

            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<Agent>(json);
        }

        public void SaveAgent(Agent agent)
        {
            var path = GetFilePath(agent.Name);
            var serializedAgent = JsonSerializer.Serialize(agent);

            File.WriteAllText(path, serializedAgent);
        }
        private static string GetFilePath(string agentName)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), RepositoryFolder);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return Path.Combine(directory, agentName) + ".json";
        }
    }
}
