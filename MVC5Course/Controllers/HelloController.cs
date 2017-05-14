using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HelloController : BaseController
    {
        // GET: Hello
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VT()
        {
            ViewBag.IsEnabled = true;
            return View();
        }

        public ActionResult RazorTest()
        {
            int[] model=new int[] { 1, 2, 3, 4, 5, };
            
            return PartialView(model);
        }


    }
}