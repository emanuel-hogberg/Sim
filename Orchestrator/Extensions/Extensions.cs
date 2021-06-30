using CLI.Commands;
using Microsoft.Extensions.DependencyInjection;
using Orchestrator.Commands;

namespace Orchestrator.Extensions
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<ICommand, TestCommand>();

            return serviceCollection;
        }
    }
}
