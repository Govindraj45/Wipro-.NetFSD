OrderManager orderManager = new();

orderManager.AddCustomer(new Customer(1, "Aarav Sharma", "aarav@example.com"));
orderManager.AddCustomer(new Customer(2, "Diya Patel", "diya@example.com"));
orderManager.AddCustomer(new Customer(3, "Rahul Verma", "rahul@example.com"));

orderManager.AddOrder(new Order(1001, 1, "Laptop", "Electronics", 75000m));
orderManager.AddOrder(new Order(1002, 2, "Running Shoes", "Fashion", 3200m));
orderManager.AddOrder(new Order(1003, 3, "Coffee Maker", "Home Appliances", 5400m));

Console.WriteLine("INITIAL ORDERS");
orderManager.DisplayOrders();
orderManager.DisplayUniqueProductCategories();

Console.WriteLine("UPDATING ORDER 1002");
orderManager.UpdateOrder(1002, "Sports Shoes", "Sports", 3500m);
orderManager.UpdateOrderStatus(1002, "Packed");
orderManager.DisplayOrders();
orderManager.DisplayUniqueProductCategories();

Console.WriteLine("REMOVING ORDER 1003");
orderManager.RemoveOrder(1003);
orderManager.DisplayOrders();
orderManager.DisplayUniqueProductCategories();

Console.WriteLine("PROCESSING ORDERS");
while (orderManager.HasOrdersToProcess)
{
    orderManager.ProcessNextOrder();
}

Console.WriteLine("STATUS HISTORY");
orderManager.DisplayStatusHistory(1001);
orderManager.DisplayStatusHistory(1002);
