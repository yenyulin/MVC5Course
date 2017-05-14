namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
  

    using System.Linq;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product: IValidatableObject
    {
        public int 產品數量 {
            get
            {
                //這邊可能會有問題
                //return this.OrderLine.Count;

                //這是先取全部的資料行數後再計算
                //return this.OrderLine.Where(p=>p.Qty>400).Count();

                //↓直接count  取回結果
                return this.OrderLine.Count(p => p.Qty > 400);
            }
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 100 && this.Stock > 5)
            {
                yield return new ValidationResult("價格與庫存數量不合理",
                    new string[] { "Price", "Stock" });
            }

            using (var db = new FabricsEntities())
            {
                var prod = db.Product.FirstOrDefault(p => p.ProductId == this.ProductId);
                if (prod != null && prod.OrderLine.Count() > 5 && this.Stock == 0)
                {
                    yield return new ValidationResult("Stock 與訂單數量不匹配",
                        new string[] { "Stock" });
                }
            }
            yield break;
        }
        
    }

  
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [DisplayName("產品名")]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [商品名稱必須包含Will字串(ErrorMessage ="名字必須包含Will")]
        public string ProductName { get; set; }

        [DisplayName("價格")]


        [DisplayFormat(DataFormatString = "{0:N0}")]
        public Nullable<decimal> Price { get; set; }

        [DisplayName("啟用")]
        public Nullable<bool> Active { get; set; }

        [DisplayName("庫存")]
        //[Range(0, 100, ErrorMessage = "請設定正確的商品庫存數量")]
        public Nullable<decimal> Stock { get; set; }

        [DisplayName("已刪除")]
        public bool IsDeleted { get; set; }

        [DisplayName("時間")]
        public System.DateTime CreateDate { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
