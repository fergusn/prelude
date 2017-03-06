using System;

public class DelegateDisposable : IDisposable
{
    private readonly Action dispose;
    public DelegateDisposable(Action dispose)
    {
        this.dispose = dispose;
    }

    public void Dispose()
    {
        this.dispose();
    }
}