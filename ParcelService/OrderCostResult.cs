using ParcelService.Services;

namespace ParcelService
{
    public class OrderCostResult
    {
        public decimal TotalCost { get; }
        public decimal SpeedyShippingCost { get; }
        public decimal ParcelsCost { get; }
        public List<Discount> Discounts { get; }
        public decimal DiscountAmount => Discounts.Sum(d => d.Amount);

        public OrderCostResult(decimal parcelsCost, decimal speedyShippingCost, List<Discount> discounts)
        {
            ParcelsCost = parcelsCost;
            SpeedyShippingCost = speedyShippingCost;
            Discounts = discounts;
            TotalCost = parcelsCost - DiscountAmount + speedyShippingCost;
        }
    }
}
