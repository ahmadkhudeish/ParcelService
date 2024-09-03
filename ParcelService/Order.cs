using ParcelService.Interfaces;

namespace ParcelService;

public class Order : IOrder
{
    private readonly List<IParcel> _parcels = new();
    public IReadOnlyList<IParcel> Parcels => _parcels.AsReadOnly();
    public bool SpeedyShipping { get; set; }

    public void AddParcel(IParcel parcel)
    {
        _parcels.Add(parcel);
    }
}
