using System.Collections.Generic;

namespace Sim.Models
{
    public class AgentActionResults
    {
        public Agent Agent { get; set; }
        public List<Purchase> PurchasesBeforeAct { get; set; }
    }
}
