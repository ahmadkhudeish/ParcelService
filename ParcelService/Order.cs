using ParcelService.Interfaces;

namespace ParcelService.Models;

public class Order : IOrder
{
    private readonly List<IParcel> _parcels = new();
    public IReadOnlyList<IParcel> Parcels => _parcels.AsReadOnly();

    public void AddParcel(IParcel parcel)
    {
        _parcels.Add(parcel);
    }
}
