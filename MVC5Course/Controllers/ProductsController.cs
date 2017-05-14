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
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {

        //這邊是快取的意思
        [OutputCache(Duration = 5, Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]
        //private FabricsEntities db = new FabricsEntities();
        //ProductRepository repo = RepositoryHelper.GetProductRepository();ㄟ
        // GET: Products
        public ActionResult Index(bool Active =false)
        {
            //Repository 的用法
            var dt = repo.GetProductByActive(Active);
            ViewData.Model = dt;

            ViewData["ppp"] = dt;
            ViewBag.qqq = dt;

            return View(dt);

            
            //var dt = db.Product.
            //    Where(p=>p.Active.HasValue && p.Active.Value== Active)
            //    .OrderByDescending(p => p.ProductId).Take(10);
            ////return View(db.Product.Take(10));
            //return View(dt);
        }

        //public ActionResult ListProducts(string q,string w)
        //{
        //    q=ViewBag.p;


        //    var data = repo.GetProductByActive(true);
        //    if (!string.IsNullOrEmpty(q))
        //    {
        //        var keyword = q;
        //        data = data.Where(p => p.ProductName.Contains(keyword));
        //    }
        //    if (!string.IsNullOrEmpty(w))
        //    {
        //        int  intPrice =Convert.ToInt32(w);
        //        data = data.Where(p => p.Price<= intPrice);
        //    }


        //    ViewData.Model = data
        //     .Select(p => new ProductLiteVM()
        //     {
        //         ProductId = p.ProductId,
        //         ProductName = p.ProductName,
        //         Price = p.Price,
        //         Stock = p.Stock
        //     })
        //      .OrderByDescending(p => p.ProductId).Take(10);
        //   return View(data);
        //}

        //public ActionResult ListProducts(string q, int Stock_S = 0, int Stock_E = 9999)
        public ActionResult ListProducts(ListProductQueryVM searchCondition)
        {
            
            var data = repo.GetProductByActive(true);
            ViewBag.s = "";
            //if (!String.IsNullOrEmpty(q))
            if (ModelState.IsValid)
            {
                //data = data.Where(p => p.ProductName.Contains(q
                data = data.Where(p => p.ProductName.Contains(searchCondition.q));
            }

            //data = data.Where(p => p.Stock > Stock_S && p.Stock < Stock_E);
            data = data.Where(p => p.Stock > searchCondition.Stock_S && p.Stock < searchCondition.Stock_E);

            ViewData.Model = data
                .Select(p => new ProductLiteVM()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                });

            return View();
        }

   

        public ActionResult CreateProduct()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateProduct(ProductLiteVM data)
        {
            if (ModelState.IsValid)
            {
                //儲存資料進資料庫
                //db.Product.Add(product);
                //db.SaveChanges();
                TempData["CreateProduct_Result"] = "商品新增成功";
                return RedirectToAction("ListProducts");
            }
            //驗證失敗 進入原本表單
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Repository的寫法 自已加一個
            Product product = repo.GetByID(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        ////[HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                //db.Product.Add(product);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.GetByID(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //db.Entry(product).State = EntityState.Modified;
        //        //db.SaveChanges();'
        //        repo.Update(product);
        //        repo.UnitOfWork.Commit(); 
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            //這邊這種寫法 如果是原本的 只要沒有bind其中一個 會變回預設值
            
            var product = repo.GetByID(id);
            //if (TryUpdateModel (product))
                //如這樣寫就是只bind這幾個
            if (TryUpdateModel(product, new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" })) 
            {
               
                    repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
            
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.GetByID(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repo.GetByID(id);
            //強迫關閉驗證 
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        
    }
}
