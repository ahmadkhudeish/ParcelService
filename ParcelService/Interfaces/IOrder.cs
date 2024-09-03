namespace ParcelService.Interfaces;

public interface IOrder
{
    IReadOnlyList<IParcel> Parcels { get; }
    void AddParcel(IParcel parcel);
}