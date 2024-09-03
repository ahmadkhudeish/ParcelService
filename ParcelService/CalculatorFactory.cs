using ParcelService.Interfaces;
using ParcelService.Services;

namespace ParcelService.Factories;

public class CalculatorFactory : ICalculatorFactory
{
    public ICostCalculator CreateCostCalculator()
    {
        return new CostCalculator();
    }

    public IParcel CreateParcel(double length, double width, double height)
    {
        return new Parcel(length, width, height);
    }

    public IOrder CreateOrder()
    {
        return new Order();
    }
}
