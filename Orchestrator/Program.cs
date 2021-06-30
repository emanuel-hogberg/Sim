using System.Linq;
using CLI;
using Microsoft.Extensions.DependencyInjection;
using Orchestrator.Extensions;
using Orchestrator.Interfaces;
using Orchestrator.Services;

namespace Orchestrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ISimService, SimService>()
                .AddCommands()
                .BuildServiceProvider();

            if (args?.Any() != true)
            {
                args = new[] { "test" };
            }

            new CLIwithDI(serviceProvider)
                .Execute(args);
        }
    }
}
