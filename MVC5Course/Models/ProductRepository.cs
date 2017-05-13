using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{

        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.IsDeleted);
        }

        public IQueryable<Product> All(bool showAll)
        {
            if (showAll)
            {
                return base.All(); 
            }
            else
            {
                return this.All();
            }
        }

        public Product GetByID(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public void Update(Product product)
        {
            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }

    public IQueryable<Product> GetProductByActive(bool Active = true, bool showAll = false)
    {
            //以下為老師寫法
            //IQueryable<Product> all = this.All();
            //if (showAll)
            //{
            //    all = base.All();
            //}
            //return all.Aggregate.Where(p => p.Active.HasValue && p.Active.Value == Active)
            //    .OrderByDescending(p => p.ProductId).Take(10);

            //這是我的寫法
            return All(showAll: showAll).Where(p => p.Active.HasValue && p.Active.Value == Active)
                .OrderByDescending(p => p.ProductId).Take(10);
        }

        public override void Delete(Product entity)
        {
           
            entity.IsDeleted = true;
            //base.Delete(entity);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}