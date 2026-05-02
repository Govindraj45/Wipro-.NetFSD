using System;

namespace LSP_Demo
{
    // The Rule: Derived classes must be substitutable for their base class.

    public class Vehicle
    {
        public string Name { get; set; }
    }

    public interface IDriveable
    {
        void Drive();
    }

    public class Car : Vehicle, IDriveable
    {
        public void Drive()
        {
            Console.WriteLine($"{Name} is driving on the road using an engine.");
        }
    }

    // Bicycle is a Vehicle but we don't force it to implement IDriveable
    // because it doesn't have an engine to "Drive" in the same way.
    public class Bicycle : Vehicle
    {
        public void Pedal()
        {
            Console.WriteLine($"{Name} is being pedaled on the bike path.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Liskov Substitution Principle (LSP) Demo ===");
            
            IDriveable myCar = new Car { Name = "Toyota Sedan" };
            myCar.Drive();

            Bicycle myBike = new Bicycle { Name = "Trek Mountain Bike" };
            myBike.Pedal();
            
            // With this design, we don't accidentally call Drive() on a Bicycle
            // which prevents NotImplementedExceptions at runtime.
        }
    }
}
