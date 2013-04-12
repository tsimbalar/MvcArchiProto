using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using WebApp.Experimentations;
using WebApp.Experimentations.Requests;
using WebApp.Experimentations.Tuyauterie;

namespace WebApp.Controllers
{
    public class FooController : Controller
    {
        private IRequestDispatcher dispatcher;
        public FooController()
        {
            var container = new UnityContainer();
            var registry = new UnityServiceRegistry(container);
            dispatcher = new RequestDispatcher(registry);
        }

        //
        // GET: /Foo/

        public ActionResult Index()
        {
            var request = new CapitalizeRequest();
            request.Blob = "bidule";

            var response = dispatcher.Execute(request);
            return View();
        }

    }
}
