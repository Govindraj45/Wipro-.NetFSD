using System;

namespace EcommerceInventory
{
    public class Inventory
    {
        private Product[] products;

        public Inventory(int size)
        {
            products = new Product[size];
        }

        // User Story 2, 4, 5: Indexer to access and modify products
        public Product this[int index]
        {
            get
            {
                if (index >= 0 && index < products.Length)
                {
                    // User story 4: Returns the correct product based on the index
                    return products[index];
                }
                throw new IndexOutOfRangeException("Invalid product index.");
            }
            set
            {
                if (index >= 0 && index < products.Length)
                {
                    // User story 5: Updates the correct product in the collection
                    products[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Invalid product index.");
                }
            }
        }

        public int Capacity => products.Length;
    }
}
