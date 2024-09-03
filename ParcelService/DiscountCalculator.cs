using ParcelService.Interfaces;
using System.Linq;

namespace ParcelService.Services;

public class DiscountCalculator
{
    private readonly ICostCalculator _costCalculator;

    public DiscountCalculator(ICostCalculator costCalculator)
    {
        _costCalculator = costCalculator;
    }

    public List<Discount> CalculateDiscounts(IReadOnlyList<IParcel> parcels)
    {
        var allDiscounts = new List<Discount>();
        var usedParcels = new HashSet<IParcel>();

        var smallParcelDiscounts = ApplyDiscount(parcels.Where(p => p.Size == ParcelSize.Small).ToList(), 4, "Small Parcel Mania");
        var mediumParcelDiscounts = ApplyDiscount(parcels.Where(p => p.Size == ParcelSize.Medium).ToList(), 3, "Medium Parcel Mania");
        var mixedParcelDiscounts = ApplyDiscount(parcels.ToList(), 5, "Mixed Parcel Mania");

        var allPossibleDiscounts = smallParcelDiscounts.Concat(mediumParcelDiscounts).Concat(mixedParcelDiscounts)
            .OrderByDescending(d => d.Amount)
            .ToList();

        foreach (var discount in allPossibleDiscounts)
        {
            if (!usedParcels.Contains(discount.Parcel))
            {
                allDiscounts.Add(discount);
                usedParcels.Add(discount.Parcel);
            }
        }

        return allDiscounts;
    }

    private List<Discount> ApplyDiscount(List<IParcel> parcels, int nthParcel, string discountName)
    {
        var discounts = new List<Discount>();
        var eligibleParcels = parcels.OrderBy(p => _costCalculator.CalculateParcelCost(p)).ToList();

        for (int i = nthParcel - 1; i < eligibleParcels.Count; i += nthParcel)
        {
            var parcel = eligibleParcels[i];
            var discountAmount = _costCalculator.CalculateParcelCost(parcel);
            discounts.Add(new Discount(discountName, discountAmount, parcel));
        }

        return discounts;
    }
}

public class Discount
{
    public string Name { get; }
    public decimal Amount { get; }
    public IParcel Parcel { get; }

    public Discount(string name, decimal amount, IParcel parcel)
    {
        Name = name;
        Amount = amount;
        Parcel = parcel;
    }
}
