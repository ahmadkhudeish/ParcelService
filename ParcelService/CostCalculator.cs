using ParcelService.Interfaces;

namespace ParcelService.Services;

public class CostCalculator : ICostCalculator
{
    private static readonly Dictionary<ParcelSize, decimal> CostBySize = new()
    {
        { ParcelSize.Small, 3m },
        { ParcelSize.Medium, 8m },
        { ParcelSize.Large, 15m },
        { ParcelSize.XL, 25m }
    };

    public decimal CalculateParcelCost(IParcel parcel)
    {
        return CostBySize[parcel.Size];
    }

    public decimal CalculateOrderCost(IOrder order)
    {
        return order.Parcels.Sum(CalculateParcelCost);
    }
}
