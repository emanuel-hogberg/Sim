using System;
using Microsoft.Extensions.DependencyInjection;
using McMaster.Extensions.CommandLineUtils;
using CLI.Commands;

namespace CLI
{
    // Implementation taken from https://brainwipe.github.io/asp.net/core/cli/commandline/di/2017/10/10/di-on-dotnet-core-cli/,
    // many thanks!!
    public class CLIwithDI : CommandLineApplication
    {
        public CLIwithDI(IServiceProvider serviceProvider)
        {
            RegisterCommands(serviceProvider);
        }

        private void RegisterCommands(IServiceProvider serviceProvider)
        {
            foreach (var command in serviceProvider.GetServices<ICommand>())
            {
                if (command is not CommandLineApplication commandLineApp)
                {
                    throw new InvalidCastException("Commands must inherit from ICommand and CommandLineApplication");
                }

                AddSubcommand(commandLineApp);
            }
        }
    }
}
