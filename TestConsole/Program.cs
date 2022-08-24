namespace TestConsole;

class Program
{
    private static void TestEventHandler(object? sender, EventArgs e)
    {
        Console.WriteLine("Starting event handler");
        Thread.Sleep(1000);
        Console.WriteLine("Ending event handler");
    }

    private void CreateHandler()
    {
        var eventHandler = new EventHandler(TestEventHandler);
        var asyncCaller = new global::AsyncCaller.AsyncCaller(eventHandler);
        Console.WriteLine(asyncCaller.Invoke(5000, this, EventArgs.Empty) ? "Success!" : "Timeout!");
    }
    
    public static void Main()
    {
        new Program().CreateHandler();
        Console.WriteLine("End.");
    }
}