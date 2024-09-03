namespace ParcelService.Interfaces;

public enum ParcelSize
{
    Small,
    Medium,
    Large,
    XL,
    Heavy
}

public interface IParcel
{
    ParcelSize Size { get; }
    double Length { get; }
    double Width { get; }
    double Height { get; }
    double Weight { get; }
}