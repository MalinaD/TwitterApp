﻿using System.Web;
using System.Web.Mvc;

namespace Twitter.Models
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}