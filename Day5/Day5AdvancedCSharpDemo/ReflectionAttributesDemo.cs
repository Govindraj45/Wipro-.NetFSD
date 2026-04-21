using System;
using System.Reflection;

namespace Day5AdvancedCSharpDemo
{
    // 1. Define a Custom Attribute
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DemoInfoAttribute : Attribute
    {
        public string DeveloperName { get; }
        public string Version { get; }

        public DemoInfoAttribute(string devName, string version)
        {
            DeveloperName = devName;
            Version = version;
        }
    }

    // Apply the custom attribute to a class
    [DemoInfo("AI Assistant", "1.0")]
    public class TargetClass
    {
        [DemoInfo("AI Assistant", "1.1")]
        public void DoSomething()
        {
            Console.WriteLine("TargetClass is doing something...");
        }

        public void DoAnotherThing()
        {
            Console.WriteLine("TargetClass is doing another thing without attribute...");
        }
    }

    public class ReflectionAttributesDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("--- Reflection and Attributes Demo ---\n");

            // 1. Inspect Class Attributes
            Type targetType = typeof(TargetClass);
            Console.WriteLine($"Inspecting type: {targetType.Name}");
            
            var classAttr = (DemoInfoAttribute)Attribute.GetCustomAttribute(targetType, typeof(DemoInfoAttribute));
            if (classAttr != null)
            {
                Console.WriteLine($"Class Attribute -> Developer: {classAttr.DeveloperName}, Version: {classAttr.Version}");
            }

            Console.WriteLine("\nInspecting Methods:");
            // 2. Inspect Method Attributes using Reflection
            MethodInfo[] methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            
            foreach (var method in methods)
            {
                var methodAttr = (DemoInfoAttribute)Attribute.GetCustomAttribute(method, typeof(DemoInfoAttribute));
                if (methodAttr != null)
                {
                    Console.WriteLine($" - {method.Name}: Developer={methodAttr.DeveloperName}, Version={methodAttr.Version}");
                }
                else
                {
                    Console.WriteLine($" - {method.Name}: No DemoInfoAttribute found.");
                }
            }

            // 3. Invoke Method dynamically using Reflection
            Console.WriteLine("\nInvoking DoSomething dynamically using Reflection:");
            object instance = Activator.CreateInstance(targetType);
            MethodInfo doSomethingMethod = targetType.GetMethod("DoSomething");
            doSomethingMethod?.Invoke(instance, null);
            
            Console.WriteLine();
        }
    }
}
