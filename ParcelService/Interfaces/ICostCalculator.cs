namespace ParcelService.Interfaces;

public interface ICostCalculator
{
    decimal CalculateParcelCost(IParcel parcel);
    OrderCostResult CalculateOrderCost(IOrder order);
}
