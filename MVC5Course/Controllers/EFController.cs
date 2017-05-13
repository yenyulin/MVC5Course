using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Net;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        

        // GET: EF
        public ActionResult Index()
        {
          
            //轉成queryable的
            //非常重要
            var all = db.Product.AsQueryable();

            var data = all.Where(p => p.Active == true && p.ProductName.Contains("Black"))
                .OrderByDescending(p => p.ProductId)
                .Take(10);

            return View(data);
            //return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
            }
            return View();
        }



        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Stock = product.Stock;
                item.Active = product.Active;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }


        /// <summary>
        /// del 而且不用httppost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            Product product = db.Product.Find(id);

            //注意這裡的寫法 
            //foreach (var item in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);          
            //}
            ////跟上方效果一樣
            ////db.OrderLine.RemoveRange(product.OrderLine);
            //db.Product.Remove(product);

            product.IsDeleted = true;
            
            try
             {
                    db.SaveChanges();
             }
             catch (DbEntityValidationException ex)
             {
                 throw ex;
             }

         

            return RedirectToAction("Index");
        }


        public ActionResult Revert(int id)
        {

            Product product = db.Product.Find(id);
            
            product.IsDeleted = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            var data = db.Database.SqlQuery<Product>("select * from dbo.Product WHERE ProductID=@p3 ",id,"","","1124").FirstOrDefault();

            return View(data);
        }

    }
}