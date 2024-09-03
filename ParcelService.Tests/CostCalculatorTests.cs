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
        [InlineData(5, 5, 5, 1, 3)]
        [InlineData(30, 30, 30, 3, 8)]
        [InlineData(60, 60, 60, 6, 15)]
        [InlineData(100, 100, 100, 10, 25)]
        public void CalculateParcelCost_ReturnsCorrectCost(double length, double width, double height, double weight, decimal expectedCost)
        {
            var parcel = _calculatorFactory.CreateParcel(length, width, height, weight);
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(expectedCost, cost);
        }

        [Theory]
        [InlineData(5, 5, 5, 2, 5)] // Small parcel 1kg overweight
        [InlineData(30, 30, 30, 5, 12)] // Medium parcel 2kg overweight
        [InlineData(60, 60, 60, 8, 19)] // Large parcel 2kg overweight
        [InlineData(100, 100, 100, 15, 35)] // XL parcel 5kg overweight
        public void CalculateParcelCost_WithWeight_ReturnsCorrectCost(double length, double width, double height, double weight, decimal expectedCost)
        {
            var parcel = _calculatorFactory.CreateParcel(length, width, height, weight);
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(expectedCost, cost);
        }

        [Theory]
        [InlineData(10, 10, 10, 50, 50)] // Heavy parcel at weight limit
        [InlineData(10, 10, 10, 55, 55)] // Heavy parcel 5kg overweight
        [InlineData(100, 100, 100, 60, 60)] // Heavy parcel, large dimensions
        public void CalculateParcelCost_HeavyParcel_ReturnsCorrectCost(double length, double width, double height, double weight, decimal expectedCost)
        {
            var parcel = _calculatorFactory.CreateParcel(length, width, height, weight);
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(expectedCost, cost);
        }

        [Fact]
        public void CalculateParcelCost_SmallParcel_ReturnsCorrectCost()
        {
            var parcel = _calculatorFactory.CreateParcel(9, 9, 9, 1);
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(3m, cost);
        }

        [Fact]
        public void CalculateParcelCost_MediumParcel_ReturnsCorrectCost()
        {
            var parcel = _calculatorFactory.CreateParcel(49, 49, 49, 3);
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(8m, cost);
        }

        [Fact]
        public void CalculateParcelCost_LargeParcel_ReturnsCorrectCost()
        {
            var parcel = _calculatorFactory.CreateParcel(99, 99, 99, 6);
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(15m, cost);
        }

        [Fact]
        public void CalculateOrderCost_WithSpeedyShipping_DoublesCost()
        {
            var order = _calculatorFactory.CreateOrder();
            order.AddParcel(_calculatorFactory.CreateParcel(5, 5, 5, 1));  // Small: $3
            order.AddParcel(_calculatorFactory.CreateParcel(30, 30, 30, 3));  // Medium: $8
            order.SpeedyShipping = true;

            var result = _costCalculator.CalculateOrderCost(order);

            Assert.Equal(22m, result.TotalCost);
            Assert.Equal(11m, result.SpeedyShippingCost);
            Assert.Equal(11m, result.ParcelsCost);
        }

        [Fact]
        public void CalculateOrderCost_WithoutSpeedyShipping_NormalCost()
        {
            var order = _calculatorFactory.CreateOrder();
            order.AddParcel(_calculatorFactory.CreateParcel(5, 5, 5, 1));  // Small: $3
            order.AddParcel(_calculatorFactory.CreateParcel(30, 30, 30, 3));  // Medium: $8
            order.SpeedyShipping = false;

            var result = _costCalculator.CalculateOrderCost(order);

            Assert.Equal(11m, result.TotalCost);
            Assert.Equal(0m, result.SpeedyShippingCost);
            Assert.Equal(11m, result.ParcelsCost);
        }

        [Fact]
        public void CalculateOrderCost_SpeedyShipping_DoesNotAffectIndividualParcelCosts()
        {
            var order = _calculatorFactory.CreateOrder();
            var smallParcel = _calculatorFactory.CreateParcel(5, 5, 5, 1);  // Small: $3
            var mediumParcel = _calculatorFactory.CreateParcel(30, 30, 30, 3);  // Medium: $8
            order.AddParcel(smallParcel);
            order.AddParcel(mediumParcel);
            order.SpeedyShipping = true;

            var result = _costCalculator.CalculateOrderCost(order);

            Assert.Equal(3m, _costCalculator.CalculateParcelCost(smallParcel));
            Assert.Equal(8m, _costCalculator.CalculateParcelCost(mediumParcel));
        }

        [Fact]
        public void CalculateParcelCost_HeavyParcelOverridesSize()
        {
            var parcel = _calculatorFactory.CreateParcel(5, 5, 5, 51); // Small dimensions but heavy weight
            var cost = _costCalculator.CalculateParcelCost(parcel);
            Assert.Equal(51m, cost); // Base cost of $50 + $1 for 1kg over
        }
    }
}