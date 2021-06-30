using System;
using CLI.Commands;
using McMaster.Extensions.CommandLineUtils;
using Orchestrator.Interfaces;

namespace Orchestrator.Commands
{
    internal class TestCommand : CommandLineApplication, ICommand
    {
        public ISimService SimService { get; }

        public TestCommand(ISimService simService)
        {
            SimService = simService;

            Name = "test";
            Description = "test the sim";
            HelpOption("-? | -h | --help");
            OnExecute(Execute);
        }

        public int Execute()
        {
            Console.WriteLine("hejhej");

            return 0;
        }
    }
}
