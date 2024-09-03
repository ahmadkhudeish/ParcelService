using ParcelService.Interfaces;

namespace ParcelService;

public class Parcel : IParcel
{
    public ParcelSize Size { get; }
    public double Length { get; }
    public double Width { get; }
    public double Height { get; }
    public double Weight { get; }

    public Parcel(double length, double width, double height, double weight)
    {
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        Size = DetermineSize();
    }

    private ParcelSize DetermineSize()
    {
        double maxDimension = Math.Max(Length, Math.Max(Width, Height));
        
        if (maxDimension < 10) return ParcelSize.Small;
        if (maxDimension < 50) return ParcelSize.Medium;
        if (maxDimension < 100) return ParcelSize.Large;
        return ParcelSize.XL;
    }
}
