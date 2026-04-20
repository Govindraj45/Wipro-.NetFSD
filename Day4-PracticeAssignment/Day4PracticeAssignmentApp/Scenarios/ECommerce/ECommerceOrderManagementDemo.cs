using Day4PracticeAssignmentApp.Common;

namespace Day4PracticeAssignmentApp.Scenarios.ECommerce;

class ECommerceOrderManagementDemo : IScenarioDemo
{
    public string Title => "Scenario 1: E-Commerce Order Management System";

    public void Run()
    {
        IOrderManagementService orderService = new OrderManagementService();

        orderService.AddCustomer(new Customer(1, "Anaya Sharma", "anaya@example.com"));
        orderService.AddCustomer(new Customer(2, "Rohan Mehta", "rohan@example.com"));

        orderService.AddOrder(new Order(101, 1, "Laptop", 75000, "Electronics"));
        orderService.AddOrder(new Order(102, 2, "Shoes", 2500, "Fashion"));
        orderService.AddOrder(new Order(103, 1, "Coffee Maker", 4500, "Home Appliances"));

        ConsoleOutput.WriteSubHeading("Add Orders");
        orderService.DisplayOrders();
        orderService.DisplayCategories();

        ConsoleOutput.WriteSubHeading("Update and Remove Orders");
        orderService.UpdateOrder(102, "Sports Shoes", 2800, "Sports");
        orderService.UpdateStatus(102, "Packed");
        orderService.RemoveOrder(103);
        orderService.DisplayOrders();
        orderService.DisplayCategories();

        ConsoleOutput.WriteSubHeading("Process Orders FIFO");
        while (orderService.HasOrdersToProcess)
        {
            orderService.ProcessNextOrder();
        }

        ConsoleOutput.WriteSubHeading("Status History LIFO");
        orderService.DisplayStatusHistory(101);
        orderService.DisplayStatusHistory(102);
    }
}
