﻿using System.Web;
using System.Web.Mvc;

namespace MVC5Course
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //[HandleError(View = "Error2", ExceptionType = typeof(ArgumentException))]
        }
    }
}
