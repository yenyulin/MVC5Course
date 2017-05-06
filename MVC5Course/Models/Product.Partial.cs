namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [DisplayName("產品名")]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }

        [DisplayName("價格")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public Nullable<decimal> Price { get; set; }

        [DisplayName("啟用")]
        public Nullable<bool> Active { get; set; }

        [DisplayName("庫存")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
