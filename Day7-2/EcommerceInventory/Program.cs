using System;

namespace EcommerceInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== E-Commerce Inventory System ===\n");

            // Create an inventory
            Inventory inventory = new Inventory(5);

            try
            {
                // User Story 1: Accessing properties
                Console.WriteLine("[User Story 1] Creating a product and accessing properties:");
                Product p1 = new Product("Laptop", 1200.50m, 10);
                Console.WriteLine($"Product Name: {p1.Name}");
                Console.WriteLine($"Product Price: {p1.Price:C}");
                Console.WriteLine($"Product Quantity: {p1.Quantity}\n");
                
                // Add to inventory
                inventory[0] = p1;
                inventory[1] = new Product("Smartphone", 800.00m, 25);
                inventory[2] = new Product("Headphones", 150.00m, 50);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // User Story 3: Ensuring Price/Quantity cannot be negative
            Console.WriteLine("[User Story 3] Ensuring Price/Quantity cannot be negative:");
            try
            {
                Console.WriteLine("Attempting to create a product with negative price...");
                Product pInvalid = new Product("Faulty Item", -50.00m, 5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation working as expected: {ex.Message}");
            }

            try
            {
                Console.WriteLine("Attempting to update a product with negative quantity...");
                Product pInvalid2 = new Product("Keyboard", 20.00m, 10);
                pInvalid2.Quantity = -2;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation working as expected: {ex.Message}\n");
            }

            // User Story 2 & 4: Accessing products using Indexer
            Console.WriteLine("[User Story 2 & 4] Accessing products using Indexer:");
            Product retrievedProduct = inventory[1];
            Console.WriteLine($"Retrieved product at index 1: {retrievedProduct}\n");

            // User Story 5: Modifying product using Indexer
            Console.WriteLine("[User Story 5] Modifying product using Indexer:");
            Console.WriteLine($"Before modification (Index 1): {inventory[1]}");
            
            // Updating the product at index 1 using the indexer
            inventory[1] = new Product("Upgraded Smartphone", 950.00m, 20);
            
            Console.WriteLine($"After modification (Index 1): {inventory[1]}\n");

            // Display all items to verify everything is working
            Console.WriteLine("--- Current Inventory ---");
            for (int i = 0; i < inventory.Capacity; i++)
            {
                if (inventory[i] != null)
                {
                    Console.WriteLine($"Index {i}: {inventory[i]}");
                }
            }
            
            Console.WriteLine("\nPress any key to exit...");
            // Console.ReadKey();
        }
    }
}
