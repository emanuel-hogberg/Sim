namespace Sim.Models
{
    public class Interest
    {
        public Category Category { get; set; }
        public double Extent { get; set; } // 0-1
        public double ExtentRangeMin { get; internal set; }
        public double ExtentRangeMax { get; internal set; }
    }
}