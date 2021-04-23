namespace Sim.Configuration
{
    public class AgentConfig
    {
        public double MaxRandomLowExtent { get; set; }
        public int MinExtraInterestedIn { get; set; }
        public int MaxExtraInterestedIn { get; set; }
        public double MinRandomHighExtent { get; set; }
        public bool UseInterestExtentForPurchaseQuantity { get; internal set; }
        public int MaxQuantityToPurchase { get; internal set; }
    }
}