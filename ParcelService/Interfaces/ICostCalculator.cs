namespace ParcelService.Interfaces;

public interface ICostCalculator
{
    decimal CalculateParcelCost(IParcel parcel);
    decimal CalculateOrderCost(IOrder order);
}
