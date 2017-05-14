using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ClientBathUpdateVM
    {
        //[Required]
        public int ClientId { get; set; }

        [Required]
        //[StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        public string FirstName { get; set; }

        [Required]
        //[StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        public string MiddleName { get; set; }

        [Required]
        //[StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        public string LastName { get; set; }

        //[StringLength(1, ErrorMessage = "欄位長度不得大於 1 個字元")]
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<double> CreditRating { get; set; }
    }
}