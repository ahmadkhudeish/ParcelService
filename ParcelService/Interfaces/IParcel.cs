namespace ParcelService.Interfaces;

public enum ParcelSize
{
    Small,
    Medium,
    Large,
    XL
}

public interface IParcel
{
    ParcelSize Size { get; }
    double Length { get; }
    double Width { get; }
    double Height { get; }
}