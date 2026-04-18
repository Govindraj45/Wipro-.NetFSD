OrderService orderService = new();

orderService.AddCustomer(new Customer(1, "Anaya"));
orderService.AddCustomer(new Customer(2, "Rohan"));

orderService.AddOrder(new Order(101, 1, "Laptop", 79999, "Electronics"));
orderService.AddOrder(new Order(102, 2, "Shoes", 2499, "Fashion"));
orderService.AddOrder(new Order(103, 1, "Mixer Grinder", 3999, "Home Appliances"));

Console.WriteLine("ALL ORDERS");
orderService.DisplayOrders();
orderService.DisplayCategories();

Console.WriteLine("UPDATING ORDER 102");
orderService.UpdateOrder(102, "Sports Shoes", 2799, "Sports");
orderService.UpdateStatus(102, "Packed");
orderService.DisplayOrders();
orderService.DisplayCategories();

Console.WriteLine("REMOVING ORDER 103");
orderService.RemoveOrder(103);
orderService.DisplayOrders();
orderService.DisplayCategories();

Console.WriteLine("PROCESSING ORDERS");
while (orderService.HasOrdersToProcess)
{
    orderService.ProcessNextOrder();
}

Console.WriteLine("ORDER STATUS HISTORY");
orderService.DisplayStatusHistory(101);
orderService.DisplayStatusHistory(102);
