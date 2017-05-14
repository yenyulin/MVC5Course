using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttributes
{
    public class 商品名稱必須包含Will字串Attribute : DataTypeAttribute
    {
        public 商品名稱必須包含Will字串Attribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            var str = (string)value;
            //return str.Contains("Will");

            return true;
        }
    }


    public class 太囉嗦Attribute : DataTypeAttribute
    {


        private readonly int _Maxwords;

        
        public 太囉嗦Attribute(int maxwords) : base("{0} 太多字")
        {
            _Maxwords = maxwords;
        }

        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value!=null)
            {
                var valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > _Maxwords)
                {
                    // 這樣的寫法可以寫出自訂的錯誤訊息
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }



}