namespace Day4PracticeAssignmentApp.Scenarios.ECommerce;

interface IOrderManagementService
{
    bool HasOrdersToProcess { get; }

    void AddCustomer(Customer customer);

    void AddOrder(Order order);

    void UpdateOrder(int orderId, string productName, double price, string category);

    void RemoveOrder(int orderId);

    void ProcessNextOrder();

    void UpdateStatus(int orderId, string status);

    void DisplayOrders();

    void DisplayCategories();

    void DisplayStatusHistory(int orderId);
}
