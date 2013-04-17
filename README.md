MvcArchiProto
=============

Just an experiment of architecture for loose coupling in an ASP.NET MVC App.

The idea is that client code should just need to create an instance of a [`Request`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/IRequest.cs) object (similar to a Command object, that is, it contains all the necessary information for an action to be executed) and pass it to a [`IRequestDispatcher`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/IRequestDispatcher.cs) that would take care of : 
- finding which implementation of a service to pass it to (using a [`ServiceRegistry`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/IServiceRegistry.cs), implemented [thanks to an IoC Container, for instance](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/Tuyauterie/UnityServiceRegistry.cs))
- invoke `IExecutableService.Execute()` on that service, passing it the request
- return the `Response` returned by the Execute() method.

For the client code, it looks like this : 
```C#
public ActionResult Index()
{
    var request = new CapitalizeRequest(); // CapitalizeRequest : IRequest<CapitalizeRequest, CapitalizeResponse>
    request.Blob = "bidule";

    var response = _dispatcher.Execute(request); // _dispatcher is a IRequestDispatcher initialized via constructor injection
    // response is of type CapitalizeResponse
    return View(response.CapitalizedBlob);
}
```


What the [`RequestDispatcher`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/Tuyauterie/RequestDispatcher.cs) does is to look inside our IoC container (in our case, Unity) if it finds any implementation of interface [`IExecutableService`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/IExecutableService.cs) defined as :
```C#
public interface IExecutableService
{
    TResponse Execute(TRequest request);
}
```
... and call that service with the request and return the Response.

We also have an Async version : [`IAsyncExecutableService`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/IAsyncExecutableService.cs) and corresponding [Async version of `RequestDispatcher`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/Tuyauterie/AsyncRequestDispatcher.cs) : [`IAsyncRequestDispatcher`](https://github.com/tsimbalar/MvcArchiProto/blob/master/WebApp/Experimentations/IAsyncRequestDispatcher.cs).

It is not quite Command-Query Segregation, hence the names **Response/Request**.


See Also
--------

You may also be interested in this post by Jef Claes : http://www.jefclaes.be/2013/01/separating-command-data-from-logic-and.html , which uses the same idea to execute Commands...
