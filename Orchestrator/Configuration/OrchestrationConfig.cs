namespace Orchestrator
{
    internal class OrchestrationConfig
    {
        public const string OrchestrationConfigElement = "OrchestrationConfig";

        public int Agents { get; set; }
        public int ExecutionsPerAgent { get; set; }
    }
}
