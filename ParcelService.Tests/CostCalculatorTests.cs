using ParcelService.Factories;
using ParcelService.Interfaces;
using Xunit;

namespace ParcelService.Tests
{
    public class CostCalculatorTests
    {
        private readonly ICostCalculator _costCalculator;
        private readonly ICalculatorFactory _calculatorFactory;

        public CostCalculatorTests()
        {
            _calculatorFactory = new CalculatorFactory();
            _costCalculator = _calculatorFactory.CreateCostCalculator();
        }

        [Theory]
        [InlineData(5, 5, 5, 3)]
        [InlineData(30, 30, 30, 8)]
        [InlineData(60, 60, 60, 15)]
        public void CalculateParcelCost_ReturnsCorrectCost(double length, double width, double height, decimal expectedCost)
        {
            var parcel = _calculatorFactory.CreateParcel(length, width, height);
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(expectedCost, cost);
        }

        [Fact]
        public void CalculateOrderCost_WithSpeedyShipping_ReturnsCorrectCosts()
        {
            var order = _calculatorFactory.CreateOrder();
            order.AddParcel(_calculatorFactory.CreateParcel(5, 5, 5));  // Small: $3
            order.AddParcel(_calculatorFactory.CreateParcel(30, 30, 30));  // Medium: $8
            order.AddParcel(_calculatorFactory.CreateParcel(60, 60, 60));  // Large: $15
            order.SpeedyShipping = true;

            var result = _costCalculator.CalculateOrderCost(order);

            Assert.Equal(52m, result.TotalCost);
            Assert.Equal(26m, result.SpeedyShippingCost);
            Assert.Equal(26m, result.ParcelsCost);
        }

        [Fact]
        public void CalculateOrderCost_WithoutSpeedyShipping_ReturnsCorrectCosts()
        {
            var order = _calculatorFactory.CreateOrder();
            order.AddParcel(_calculatorFactory.CreateParcel(5, 5, 5));  // Small: $3
            order.AddParcel(_calculatorFactory.CreateParcel(30, 30, 30));  // Medium: $8
            order.AddParcel(_calculatorFactory.CreateParcel(60, 60, 60));  // Large: $15
            order.SpeedyShipping = false;

            var result = _costCalculator.CalculateOrderCost(order);

            Assert.Equal(26m, result.TotalCost);
            Assert.Equal(0m, result.SpeedyShippingCost);
            Assert.Equal(26m, result.ParcelsCost);
        }
    }
}