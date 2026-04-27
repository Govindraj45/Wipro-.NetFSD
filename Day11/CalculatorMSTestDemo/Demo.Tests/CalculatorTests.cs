using Demo;

namespace Demo.Tests;

[TestClass]
public sealed class CalculatorTests
{
    [TestMethod]
    public void Multiply_TwoPositiveNumbers_ReturnsExpectedProduct()
    {
        // Creating an object of the class.
        Calculator calculator = new();

        // Passing values.
        int result = calculator.Multiply(2, 6);

        // Checking for the expected value.
        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void Multiply_ByZero_ReturnsZero()
    {
        Calculator calculator = new();

        int result = calculator.Multiply(9, 0);

        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Multiply_NegativeAndPositiveNumber_ReturnsNegativeProduct()
    {
        Calculator calculator = new();

        int result = calculator.Multiply(-4, 5);

        Assert.AreEqual(-20, result);
    }
}
