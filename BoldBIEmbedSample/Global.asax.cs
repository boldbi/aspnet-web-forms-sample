using BoldBIEmbedSample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace BoldBIEmbedSample
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            try
            {
                string embedConfigPath = Server.MapPath("~/embedConfig.json");
                string jsonString = System.IO.File.ReadAllText(embedConfigPath);
                GlobalAppSettings.EmbedDetails = JsonConvert.DeserializeObject<EmbedDetails>(jsonString);
            }
            catch
            {
               RouteTable.Routes.MapPageRoute("EmbedConfigPageRoute", "EmbedConfigErrorLog", "~/EmbedConfigErrorLog.aspx");
            }
        }
    }
}