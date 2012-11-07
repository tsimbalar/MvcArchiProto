namespace WebApp.Experimentations
{
    /// <summary>
    /// just a "marker" interface ... a command tells us what its return value is
    /// </summary>S
    public interface ICommand<TRequest, TResponse>
    {
        TRequest Self { get;  }
    }
}