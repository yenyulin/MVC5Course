using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;



namespace MVC5Course.Controllers
{
    //abstract 必須得加
    public abstract class BaseController : Controller
    {
        //需用public 或protected 才能讓有被繼承的使用
        protected ProductRepository repo = RepositoryHelper.GetProductRepository();
        protected FabricsEntities db = new FabricsEntities();

        ////找不到的話導到首頁 但一般來說沒特別說是不用做的
        //protected override void HandleUnknownAction(string actionName)
        //{

        //    //this.RedirectToAction("Index", "Home").ExecuteResult(this.ControllerContext);
        //    base.HandleUnknownAction(actionName);
        //}

        public ActionResult Debug()
        {
            return Content("oops");
        }
    }
}