using System;
using System.Collections.Generic;

namespace OCP_Demo
{
    // The Rule: Open for extension, closed for modification.
    
    public interface IShape
    {
        double Area();
    }

    public class Circle : IShape
    {
        public double Radius { get; set; }
        public double Area() => Math.PI * Radius * Radius;
    }

    public class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Area() => Width * Height;
    }
    
    // NEW Shape - added without modifying existing code (AreaCalculator)
    public class Triangle : IShape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public double Area() => 0.5 * Base * Height;
    }

    public class AreaCalculator
    {
        public double TotalArea(IEnumerable<IShape> shapes)
        {
            double area = 0;
            foreach (var shape in shapes)
            {
                area += shape.Area();
            }
            return area;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Open/Closed Principle (OCP) Demo ===");
            var shapes = new List<IShape>
            {
                new Circle { Radius = 5 },
                new Rectangle { Width = 4, Height = 6 },
                new Triangle { Base = 3, Height = 4 }
            };

            var calculator = new AreaCalculator();
            Console.WriteLine($"Total Area of all shapes: {calculator.TotalArea(shapes):F2}");
        }
    }
}
