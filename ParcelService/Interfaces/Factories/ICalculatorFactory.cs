namespace ParcelService.Interfaces
{
    public interface ICalculatorFactory
    {
        ICostCalculator CreateCostCalculator();
        IParcel CreateParcel(double length, double width, double height, double weight);
        IOrder CreateOrder();
    }
}