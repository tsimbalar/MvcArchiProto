using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Experimentations;
using WebApp.Experimentations.Commands;

namespace WebApp.Controllers
{
    public class FooController : Controller
    {

        //
        // GET: /Foo/

        public ActionResult Index()
        {
            var command = new CapitalizeCommand();
            command.Blob = "bidule";
            return View();
        }

    }
}
