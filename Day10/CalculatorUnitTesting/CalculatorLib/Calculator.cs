namespace CalculatorLib
{
    /// <summary>
    /// A simple calculator class that supports basic arithmetic operations.
    /// Implements addition, subtraction, multiplication, and division.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Adds two numbers and returns the result.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>The sum of a and b.</returns>
        public double Add(double a, double b)
        {
            return a + b;
        }

        /// <summary>
        /// Subtracts the second number from the first and returns the result.
        /// </summary>
        /// <param name="a">The first operand (minuend).</param>
        /// <param name="b">The second operand (subtrahend).</param>
        /// <returns>The difference of a and b.</returns>
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        /// <summary>
        /// Multiplies two numbers and returns the result.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>The product of a and b.</returns>
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        /// <summary>
        /// Divides the first number by the second and returns the result.
        /// Throws a DivideByZeroException if the divisor is zero.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <returns>The quotient of a divided by b.</returns>
        /// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return a / b;
        }
    }
}
