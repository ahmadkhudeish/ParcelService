# Parcel Service

This project implements a parcel pricing and discount system for a delivery service.

## How to Run Tests

1. Ensure you have .NET 7.0 SDK installed on your machine.
2. Clone this repository to your local machine.
3. Open a terminal and navigate to the project root directory.
4. Run the following command to execute all tests:

   ```
   dotnet test
   ```

## Assumptions Made

1. Parcel sizes are determined solely by the largest dimension (length, width, or height).
2. Weight limits are applied after determining the size category.
3. The Heavy parcel category overrides any size-based categorization if the weight is 50kg or more.
4. All monetary values are in a single currency with two decimal places of precision.
5. All weights are in kilograms (kg) and represented as positive numbers.
6. Dimensions (length, width, height) are in the same unit of measurement (e.g., centimeters).
7. There is no maximum weight limit implemented (beyond what a double can represent).
8. The minimum weight is assumed to be greater than 0 kg.
9. We do not handle currency conversion or multi-currency support.
10. Very small weight differences may be subject to floating-point precision limitations.

## Implementation Notes

- The project uses a factory pattern to create parcels, orders, and calculators.
- Discount calculation is separated into its own class for better separation of concerns.
- Unit tests cover various scenarios for parcel cost calculation and discount application.
- Discount functionality is not currently implemented or tested.

## Next Steps and Improvements

1. Implement more comprehensive error handling and input validation.
2. Add integration tests to cover end-to-end scenarios.
3. Improve the efficiency of discount calculations for large orders.
4. Implement a user interface (console or web-based) for easier interaction with the system.
5. Add logging for better debugging and monitoring.
6. Consider using dependency injection for better testability and flexibility.

## Known Limitations

- The current implementation does not fully optimize for the best combination of discounts in all scenarios (Step 5f of the discount requirements).
- Edge cases with extremely large or heavy parcels may not be fully covered.

## Commit History

The project was developed with a series of well-structured commits, each focusing on a specific feature or improvement. You can view the commit history to see the progression of the implementation.

## Feedback and Self-Criticism

- The discount calculation logic could be optimized for better performance with large orders.
- More extensive documentation within the code would improve readability and maintainability.
- Some parts of the code, particularly in the `CostCalculator`
  class, could benefit from refactoring for better separation of concerns.
- I noticed, in `CalculatorFactory`, the parameters for `CreateParcel` are a bit too much and makes the code a bit harder to read, it could be simplified with passing a single model.

Despite these areas for improvement, the current implementation provides a solid foundation for the parcel service pricing system, with good test coverage and a clear structure.
