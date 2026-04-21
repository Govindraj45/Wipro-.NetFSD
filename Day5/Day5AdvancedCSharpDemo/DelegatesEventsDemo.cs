using System;

namespace Day5AdvancedCSharpDemo
{
    // 1. Define a custom delegate
    public delegate void ProcessMessageDelegate(string message);

    // Publisher Class for Events
    public class MessagePublisher
    {
        // 2. Define an Event based on EventHandler
        public event EventHandler<string> OnMessagePublished;

        public void PublishMessage(string msg)
        {
            Console.WriteLine($"[Publisher] Publishing: {msg}");
            
            // Raise the event
            OnMessagePublished?.Invoke(this, msg);
        }
    }

    // Subscriber Class for Events
    public class MessageSubscriber
    {
        public string Name { get; }

        public MessageSubscriber(string name)
        {
            Name = name;
        }

        // Event Handler Method
        public void HandleMessage(object sender, string message)
        {
            Console.WriteLine($"[Subscriber {Name}] Received: {message}");
        }
    }

    public class DelegatesEventsDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("--- Delegates and Events Demo ---\n");

            // A. Delegate Demo
            Console.WriteLine(">> Delegate Demo");
            ProcessMessageDelegate printUpper = msg => Console.WriteLine($"UPPERCASE: {msg.ToUpper()}");
            ProcessMessageDelegate printLower = msg => Console.WriteLine($"lowercase: {msg.ToLower()}");
            
            // Multicast Delegate
            ProcessMessageDelegate multiDelegate = printUpper;
            multiDelegate += printLower;
            
            Console.WriteLine("Invoking multicast delegate:");
            multiDelegate("Hello Delegates!");
            Console.WriteLine();

            // B. Events Demo
            Console.WriteLine(">> Events Demo");
            var publisher = new MessagePublisher();
            var sub1 = new MessageSubscriber("Alice");
            var sub2 = new MessageSubscriber("Bob");

            // Subscribe to event
            publisher.OnMessagePublished += sub1.HandleMessage;
            publisher.OnMessagePublished += sub2.HandleMessage;

            publisher.PublishMessage("System Update Available");

            // Unsubscribe Bob
            Console.WriteLine("\nBob is unsubscribing...");
            publisher.OnMessagePublished -= sub2.HandleMessage;

            publisher.PublishMessage("System Reboot Required");
            Console.WriteLine();
        }
    }
}
