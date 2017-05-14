using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class LocatOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
    
    public class ShareViewBagAttribute : ActionFilterAttribute
    {
        public string MyProperty { get; set; }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.Controller.ViewBag.Message = "Your application description page.";
            filterContext.Controller.ViewBag.Message = "Your application description page.";
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "dt=" + DateTime.Now; ;
            base.OnActionExecuted(filterContext);
        }
    }
}