using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zero_Hunger.Auth
{
    public class restAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["user"] != null && httpContext.Session["type"].ToString().Equals("restaurant"); 
        }
    }
}