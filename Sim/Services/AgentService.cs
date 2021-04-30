using Microsoft.Extensions.Options;
using Sim.Configuration;
using Sim.Interfaces;
using Sim.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sim.Services
{
    public class AgentService : IAgentService
    {
        private readonly AgentConfig _config;
        private readonly Random _random;

        public AgentService(IOptions<AgentConfig> config)
        {
            _config = config.Value;

            var seed = new Guid()
                .ToString()
                .Select(c => c.GetHashCode())
                .Sum();
            _random = new Random(seed);
        }

        public void MakePurchase(Agent agent)
        {
            var pivot = _random.NextDouble() * agent.InterestExtentSum;

            var interestToPurchase = agent.Interests.FirstOrDefault(interest =>
                pivot >= interest.ExtentRangeMin &&
                pivot <= interest.ExtentRangeMax);

            if (interestToPurchase == null)
            {
                throw new InvalidOperationException("Did not find an interest to purchase.");
            }

            var quantityToAdd = GetQuantity(interestToPurchase);

            if (agent.Purchases.FirstOrDefault(purchase => purchase.Category == interestToPurchase.Category) is Purchase existingPurchase)
            {
                existingPurchase.Quantity += quantityToAdd;
            }
            else
            {
                agent.Purchases.Add(new Purchase
                {
                    Category = interestToPurchase.Category,
                    Quantity = quantityToAdd
                });
            }
        }

        private int GetQuantity(Interest interestToPurchase) =>
            _config.UseInterestExtentForPurchaseQuantity ?
                (int)((_random.Next(0, _config.MaxQuantityToPurchase) + 1) * interestToPurchase.Extent) + 1
                : _random.Next(1, _config.MaxQuantityToPurchase);

        public void EnsureInitialized(Agent agent)
        {
            if (agent.Interests?.Any() == true)
            {
                return;
            }

            agent.Interests = AllAvailableCategories()
                .Select(category => new Interest
                {
                    Category = category,
                    Extent = 0.0
                })
                .ToList();

            SetInterestsExtents(agent.Interests);

            agent.Purchases = new List<Purchase>();
        }

        private void SetInterestsExtents(List<Interest> interests)
        {
            SetBasicInterestLevels(interests);
            SetExtraInterestedLevels(interests);
            SetPartialSums(interests);
        }

        private static void SetPartialSums(List<Interest> interests)
        {
            double extentSum = 0.0;
            for (int i = 0; i < interests.Count; i++)
            {
                interests[i].ExtentRangeMin = extentSum;
                extentSum += interests[i].Extent;
                interests[i].ExtentRangeMax = extentSum;
            }
        }

        private void SetExtraInterestedLevels(List<Interest> interests)
        {
            var extraInterestedIn = _random.Next(
                            _config.MinExtraInterestedIn,
                            _config.MaxExtraInterestedIn);

            for (int i = 0; i < extraInterestedIn; i++)
            {
                interests[_random.Next(0, interests.Count - 1)]
                    .Extent = _config.MinRandomHighExtent + (1.0 - _config.MinRandomHighExtent) * _random.NextDouble();
            }
        }

        private void SetBasicInterestLevels(List<Interest> interests)
        {
            interests.ForEach(interest =>
                            interest.Extent = _random.NextDouble() * _config.MaxRandomLowExtent);
        }

        private static IEnumerable<Category> AllAvailableCategories()
        {
            return Enum.GetValues<Category>();
        }
    }
}
