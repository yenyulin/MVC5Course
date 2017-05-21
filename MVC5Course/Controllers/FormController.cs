using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : BaseController
    {
        // GET: Form
        public ActionResult Index()
        {
            
            return View(repo.All().Take(10));
        }

        public ActionResult Edit(int id )
        {
            ViewData.Model = repo.GetByID(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection from)
        {
            var product = repo.GetByID(id);
            if (TryUpdateModel(product,
                includeProperties:new string[] { "ProductName" }))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}