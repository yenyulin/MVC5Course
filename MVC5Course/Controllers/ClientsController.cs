using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ClientsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();


    // GET: Clients
    public ActionResult BathUpdate()
        {
            GetClients();
            return View();
        }

        private void GetClients()
        {
            var client = db.Client.Include(c => c.Occupation).Take(10);
            ViewData.Model = client;
          
        }

       

        [HttpPost]
        public ActionResult BathUpdate(ClientBathUpdateVM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var c = db.Client.Find(item.ClientId);
                    c.FirstName = item.FirstName;
                    c.MiddleName = item.MiddleName;
                    c.LastName = item.LastName;
                }
                db.SaveChanges();

                return RedirectToAction("BathUpdate");
            }

            GetClients();

            return View();
        }

    }
}
