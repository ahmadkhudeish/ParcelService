namespace ParcelService
{
    public class OrderCostResult
    {
        public decimal TotalCost { get; }
        public decimal SpeedyShippingCost { get; }
        public decimal ParcelsCost { get; }

        public OrderCostResult(decimal parcelsCost, decimal speedyShippingCost)
        {
            ParcelsCost = parcelsCost;
            SpeedyShippingCost = speedyShippingCost;
            TotalCost = parcelsCost + speedyShippingCost;
        }
    }
}
