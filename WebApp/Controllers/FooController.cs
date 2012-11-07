using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using WebApp.Experimentations;
using WebApp.Experimentations.Commands;
using WebApp.Experimentations.Tuyauterie;

namespace WebApp.Controllers
{
    public class FooController : Controller
    {
        private ICommandDispatcher dispatcher;
        public FooController()
        {
            var container = new UnityContainer();
            var registry = new UnityServiceRegistry(container);
            dispatcher = new CommandDispatcher(registry);
        }

        //
        // GET: /Foo/

        public ActionResult Index()
        {
            var command = new CapitalizeCommand();
            command.Blob = "bidule";

            var response = dispatcher.Execute(command);
            return View();
        }

    }
}
