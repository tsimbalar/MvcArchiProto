namespace WebApp.Experimentations
{
    public interface IExecutableService<TRequest, TResponse>
    {
        TResponse Execute(TRequest request);
    }
}