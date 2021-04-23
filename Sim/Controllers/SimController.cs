using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sim.Interfaces;
using Sim.Models;
using System;

namespace Sim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimController : ControllerBase
    {
        private readonly ILogger<SimController> _logger;
        private readonly IRepositoryService _repositoryService;
        private readonly IAgentService _agentService;

        public SimController(
            ILogger<SimController> logger,
            IRepositoryService repositoryService,
            IAgentService agentService)
        {
            _logger = logger;
            _repositoryService = repositoryService;
            _agentService = agentService;
        }

        [HttpGet]
        public Agent Act(string agentName)
        {
            if (string.IsNullOrWhiteSpace(agentName))
            {
                return default;
            }

            return UsingAgent(agentName, agent =>
            {
                _agentService.Act(agent);

                return agent;
            });
        }

        private T UsingAgent<T>(string agentName, Func<Agent, T> function)
        {
            var agent = _repositoryService.LoadOrCreateAgent(agentName);

            _agentService.EnsureInitialized(agent);

            var result = function(agent);

            _repositoryService.SaveAgent(agent);

            return result;
        }
    }
}
