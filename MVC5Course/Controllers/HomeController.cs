using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Unknown()
        {
            return View();
        }

        [ShareViewBag]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            throw new ArgumentException("Error Handled!!");

            return View();
        }
        [ShareViewBag(MyProperty = "")]
        public ActionResult PartialAbout()
        {
            //如果是ajax的request
            if (Request.IsAjaxRequest())
            {
                //會回傳只有內容不含masterpage的html內容
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult GetFile()
        {
            //會直接download
            return File(Server.MapPath("~/Content/Joker.jpg"), "image/jpeg", "TheJoker.jpg");
        }

        public ActionResult SomeAction()
        {
            //Response.Write("<script>alert('建立成功!'); location.href='/';</script>");
            //return "<script>alert('建立成功!'); location.href='/';</script>";
            //return Content("<script>alert('建立成功!'); location.href='/';</script>");
            //以上三種千萬別寫

            //要建立一個partialview在view / shared裡面的SuccessRedirect
            return PartialView("SuccessRedirect","/");
        }


        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return Json(db.Product.Take(5),
                JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index()
        {
            return View();
        }

     
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Test()
        {
            return View();
        }
    }
}