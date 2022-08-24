namespace AsyncCaller;

public class AsyncCaller
{
    private readonly EventHandler _handler;
    private Thread? _thread;

    public AsyncCaller(EventHandler handler)
    {
        _handler = handler;
    }

    private static void Wait(object? timeout)
    {
        Thread.Sleep((int)timeout!);
    }
    
    private void Aborter(IAsyncResult asyncResult)
    {
        _thread?.Abort();
    }
    
    // EventHandler.BeginInvoke не поддерживается в текущей версии .NET, но
    // я использовал его, т.к. он был в примере кода из задания
    public bool Invoke(int timeout, object sender, EventArgs eventArgs)
    {
        _thread = new Thread(Wait);
        var result = _handler?.BeginInvoke(sender, eventArgs, Aborter, this);
        _thread.Start(timeout);
        _thread.Join();
        _thread = null;
        return result is { IsCompleted: true };
    }
}