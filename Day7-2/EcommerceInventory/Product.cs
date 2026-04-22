using System;

namespace EcommerceInventory
{
    public class Product
    {
        private decimal price;
        private int quantity;

        // User Story 1: Accessing Name using property
        public string Name { get; set; }

        // User Story 1 & 3: Accessing Price with validation
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative.");
                price = value;
            }
        }

        // User Story 1 & 3: Accessing Quantity with validation
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative.");
                quantity = value;
            }
        }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price; // Uses property to enforce validation
            Quantity = quantity; // Uses property to enforce validation
        }

        public override string ToString()
        {
            return $"Product: {Name}, Price: {Price:C}, Quantity: {Quantity}";
        }
    }
}
