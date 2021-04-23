using System.Collections.Generic;
using System.Linq;

namespace Sim.Models
{
    public class Agent
    {
        private double _interestExtentSum = -1.0;

        public string Name { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Purchase> Purchases { get; set; }

        internal double InterestExtentSum { get
            {
                if (_interestExtentSum < 0)
                {
                    _interestExtentSum = Interests.Sum(Interest => Interest.Extent);
                }

                return _interestExtentSum;
            }
        }
    }
}