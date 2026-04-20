namespace Day4PracticeAssignmentApp.Scenarios.ECommerce;

class Customer
{
    public Customer(int customerId, string name, string email)
    {
        CustomerId = customerId;
        Name = name;
        Email = email;
    }

    public int CustomerId { get; }
    public string Name { get; }
    public string Email { get; }
}
