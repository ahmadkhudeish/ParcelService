namespace ParcelService.Interfaces;

public interface IOrder
{
    IReadOnlyList<IParcel> Parcels { get; }
    bool SpeedyShipping { get; set; }
    void AddParcel(IParcel parcel);
}