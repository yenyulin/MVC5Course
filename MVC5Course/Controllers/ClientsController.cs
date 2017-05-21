using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;

namespace MVC5Course.Controllers
{
    public class ClientsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        public ActionResult Index(int CreditRatingFilter=-1,string LastNameFilter="",int intPageNo = 1)
        {
            var ratings = (from p in db.Client
                                select p.CreditRating)
                           .Distinct()
                           .OrderBy(p => p).ToList();

            ViewBag.CreditRatingFilter = new SelectList(ratings);

            var lastNames = (from p in db.Client
                             select p.LastName)
                           .Distinct()
                           .OrderBy(p => p).ToList();

            ViewBag.LastNameFilter = new SelectList(lastNames);

            var client = db.Client.AsQueryable();


            if (CreditRatingFilter >= 0)
            {
                client = client.Where(p => p.CreditRating == CreditRatingFilter);
            }
            if (!String.IsNullOrEmpty(LastNameFilter))
            {
                client = client.Where(p => p.LastName == LastNameFilter);
            }
            
            ViewData.Model = client.OrderBy(w => w.ClientId).ToPagedList(intPageNo, 10);

            return View();
        }

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

        public ActionResult Edit(int id)
        {
            Client client = db.Client.Find(id);

            List<SelectListItem> listItem = new List<SelectListItem>();
            for (double i = 0; i <= 9; i++)
            {
                listItem.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            

            // ViewData["Rat"] = new SelectList(listItem, "Value", "Text", "");

            var items = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ViewBag.CreditRating = new SelectList(items);

            //ViewData.Model = db.Client.Find(id);
            return View(client);
            
        }



        [HttpPost]
        public ActionResult Edit(int id, FormCollection from)
        {
            var Client = db.Client.Find(id);
            //var items = new int[0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

            //ViewBag.CreditRating = new SelectList(items);

   
            //ViewBag.Rating= new SelectList(listItem, "Value", "Text", "");



            if (TryUpdateModel(Client,
              includeProperties: new string[] { "CreditRating" }))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

      
     
        


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
