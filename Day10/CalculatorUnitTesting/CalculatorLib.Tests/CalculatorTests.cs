using CalculatorLib;
using Xunit;

namespace CalculatorLib.Tests
{
    /// <summary>
    /// Unit tests for the Calculator class.
    /// Covers valid inputs, edge cases, and exception handling for all four arithmetic operations.
    /// </summary>
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        /// <summary>
        /// Initializes a new Calculator instance before each test.
        /// </summary>
        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        // ==============================
        //  Addition Tests
        // ==============================

        [Fact]
        public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
        {
            // Arrange
            double a = 5;
            double b = 3;

            // Act
            double result = _calculator.Add(a, b);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void Add_TwoNegativeNumbers_ReturnsCorrectSum()
        {
            double result = _calculator.Add(-4, -6);
            Assert.Equal(-10, result);
        }

        [Fact]
        public void Add_PositiveAndNegativeNumber_ReturnsCorrectSum()
        {
            double result = _calculator.Add(10, -3);
            Assert.Equal(7, result);
        }

        [Fact]
        public void Add_Zero_ReturnsOtherNumber()
        {
            // Edge case: adding zero should return the other number
            Assert.Equal(5, _calculator.Add(5, 0));
            Assert.Equal(5, _calculator.Add(0, 5));
        }

        [Fact]
        public void Add_BothZero_ReturnsZero()
        {
            double result = _calculator.Add(0, 0);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Add_DecimalNumbers_ReturnsCorrectSum()
        {
            double result = _calculator.Add(1.5, 2.3);
            Assert.Equal(3.8, result, 10); // precision of 10 decimal places
        }

        [Fact]
        public void Add_LargeNumbers_ReturnsCorrectSum()
        {
            double result = _calculator.Add(1_000_000, 2_000_000);
            Assert.Equal(3_000_000, result);
        }

        // ==============================
        //  Subtraction Tests
        // ==============================

        [Fact]
        public void Subtract_TwoPositiveNumbers_ReturnsCorrectDifference()
        {
            double result = _calculator.Subtract(10, 4);
            Assert.Equal(6, result);
        }

        [Fact]
        public void Subtract_ResultIsNegative_ReturnsCorrectDifference()
        {
            double result = _calculator.Subtract(3, 7);
            Assert.Equal(-4, result);
        }

        [Fact]
        public void Subtract_TwoNegativeNumbers_ReturnsCorrectDifference()
        {
            double result = _calculator.Subtract(-5, -3);
            Assert.Equal(-2, result);
        }

        [Fact]
        public void Subtract_Zero_ReturnsOriginalNumber()
        {
            // Edge case: subtracting zero should return the original number
            Assert.Equal(7, _calculator.Subtract(7, 0));
        }

        [Fact]
        public void Subtract_FromZero_ReturnsNegation()
        {
            // Edge case: subtracting from zero returns the negation
            Assert.Equal(-5, _calculator.Subtract(0, 5));
        }

        [Fact]
        public void Subtract_SameNumbers_ReturnsZero()
        {
            double result = _calculator.Subtract(9, 9);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Subtract_DecimalNumbers_ReturnsCorrectDifference()
        {
            double result = _calculator.Subtract(5.7, 2.3);
            Assert.Equal(3.4, result, 10);
        }

        // ==============================
        //  Multiplication Tests
        // ==============================

        [Fact]
        public void Multiply_TwoPositiveNumbers_ReturnsCorrectProduct()
        {
            double result = _calculator.Multiply(4, 5);
            Assert.Equal(20, result);
        }

        [Fact]
        public void Multiply_PositiveAndNegative_ReturnsNegativeProduct()
        {
            double result = _calculator.Multiply(3, -4);
            Assert.Equal(-12, result);
        }

        [Fact]
        public void Multiply_TwoNegativeNumbers_ReturnsPositiveProduct()
        {
            double result = _calculator.Multiply(-3, -4);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Multiply_ByZero_ReturnsZero()
        {
            // Edge case: multiplying by zero always returns zero
            Assert.Equal(0, _calculator.Multiply(100, 0));
            Assert.Equal(0, _calculator.Multiply(0, 100));
        }

        [Fact]
        public void Multiply_ByOne_ReturnsSameNumber()
        {
            // Edge case: multiplying by one returns the same number
            Assert.Equal(7, _calculator.Multiply(7, 1));
            Assert.Equal(7, _calculator.Multiply(1, 7));
        }

        [Fact]
        public void Multiply_DecimalNumbers_ReturnsCorrectProduct()
        {
            double result = _calculator.Multiply(2.5, 4.0);
            Assert.Equal(10.0, result, 10);
        }

        // ==============================
        //  Division Tests
        // ==============================

        [Fact]
        public void Divide_TwoPositiveNumbers_ReturnsCorrectQuotient()
        {
            double result = _calculator.Divide(10, 2);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_ResultIsDecimal_ReturnsCorrectQuotient()
        {
            double result = _calculator.Divide(7, 2);
            Assert.Equal(3.5, result);
        }

        [Fact]
        public void Divide_NegativeByPositive_ReturnsNegativeQuotient()
        {
            double result = _calculator.Divide(-10, 2);
            Assert.Equal(-5, result);
        }

        [Fact]
        public void Divide_TwoNegativeNumbers_ReturnsPositiveQuotient()
        {
            double result = _calculator.Divide(-10, -2);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_ByOne_ReturnsSameNumber()
        {
            // Edge case: dividing by one returns the same number
            double result = _calculator.Divide(8, 1);
            Assert.Equal(8, result);
        }

        [Fact]
        public void Divide_ZeroByNumber_ReturnsZero()
        {
            // Edge case: zero divided by any non-zero number is zero
            double result = _calculator.Divide(0, 5);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            // Exception handling: division by zero must throw DivideByZeroException
            var exception = Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
            Assert.Equal("Cannot divide by zero.", exception.Message);
        }

        [Fact]
        public void Divide_ZeroByZero_ThrowsDivideByZeroException()
        {
            // Edge case: 0/0 should also throw, not return NaN
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(0, 0));
        }

        [Fact]
        public void Divide_DecimalNumbers_ReturnsCorrectQuotient()
        {
            double result = _calculator.Divide(7.5, 2.5);
            Assert.Equal(3.0, result, 10);
        }

        [Fact]
        public void Divide_LargeBySmall_ReturnsCorrectQuotient()
        {
            double result = _calculator.Divide(1_000_000, 0.001);
            Assert.Equal(1_000_000_000, result, 5);
        }
    }
}
