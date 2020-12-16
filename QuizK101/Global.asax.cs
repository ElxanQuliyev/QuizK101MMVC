using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QuizK101
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        //{
        //    string currentUrl = HttpContext.Current.Request.Path.ToLower();
        //    if (currentUrl.Contains("/quiz/StartExam"))
        //    {
        //        Response.Status = "301 Moved Permanently";
        //        Response.AddHeader("Location", "/Quiz/EndQuiz");
        //        Response.End();
        //    }
        //}
    }
}
