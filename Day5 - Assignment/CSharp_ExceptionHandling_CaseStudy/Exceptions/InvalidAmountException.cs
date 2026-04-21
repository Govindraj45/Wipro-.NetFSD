using System;

namespace CSharp_ExceptionHandling_CaseStudy
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base("The amount entered is invalid.")
        {
        }

        public InvalidAmountException(string message) : base(message)
        {
        }

        public InvalidAmountException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
