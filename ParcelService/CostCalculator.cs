using ParcelService.Interfaces;

namespace ParcelService.Services;

public class CostCalculator : ICostCalculator
{
    private static readonly Dictionary<ParcelSize, decimal> CostBySize = new()
    {
        { ParcelSize.Small, 3m },
        { ParcelSize.Medium, 8m },
        { ParcelSize.Large, 15m },
        { ParcelSize.XL, 25m },
        { ParcelSize.Heavy, 50m }
    };

    private static readonly Dictionary<ParcelSize, double> WeightLimitBySize = new()
    {
        { ParcelSize.Small, 1 },
        { ParcelSize.Medium, 3 },
        { ParcelSize.Large, 6 },
        { ParcelSize.XL, 10 },
        { ParcelSize.Heavy, 50 }
    };

    private const decimal OverweightChargePerKg = 2m;
    private const decimal HeavyOverweightChargePerKg = 1m;

    public decimal CalculateParcelCost(IParcel parcel)
    {
        decimal baseCost = CostBySize[parcel.Size];
        double weightLimit = WeightLimitBySize[parcel.Size];
        

        if (parcel.Size == ParcelSize.Heavy)
        {
            decimal cost = baseCost;
            if (parcel.Weight > weightLimit)
            {
                cost += (decimal)(parcel.Weight - weightLimit) * HeavyOverweightChargePerKg;
            }
            return cost;
        }
        else if (parcel.Weight > weightLimit)
        {
            decimal cost = baseCost + (decimal)(parcel.Weight - weightLimit) * OverweightChargePerKg;
            return cost;
        }

        return baseCost;
    }

    public OrderCostResult CalculateOrderCost(IOrder order)
    {
        decimal parcelsCost = order.Parcels.Sum(CalculateParcelCost);
        decimal speedyShippingCost = order.SpeedyShipping ? parcelsCost : 0;

        return new OrderCostResult(parcelsCost, speedyShippingCost);
    }
}
