using BoldBIEmbedSample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
            try
            {
                string embedConfigPath = Server.MapPath("~/embedConfig.json");
                string jsonString = System.IO.File.ReadAllText(embedConfigPath);
                GlobalAppSettings.EmbedDetails = JsonConvert.DeserializeObject<EmbedDetails>(jsonString);


                // Check for null to avoid potential issues
                if (GlobalAppSettings.EmbedDetails != null)
                {
                    // Create an instance of DashboardInfo and set properties
                    var dashboardInfo = new DashboardInfo
                    {
                        DashboardId = GlobalAppSettings.EmbedDetails.DashboardId,
                        SiteIdentifier = GlobalAppSettings.EmbedDetails.SiteIdentifier,
                        ServerUrl = GlobalAppSettings.EmbedDetails.ServerUrl,
                        EmbedType = GlobalAppSettings.EmbedDetails.EmbedType,
                        Environment = GlobalAppSettings.EmbedDetails.Environment
                    };

                    // Store the instance in HttpContext.Current.Items
                    HttpContext.Current.Items["DashboardInfo"] = dashboardInfo;

                    // Log or debug the values
                    System.Diagnostics.Debug.WriteLine($"DashboardId: {dashboardInfo.DashboardId}, SiteIdentifier: {dashboardInfo.SiteIdentifier}");
                }
            }
            catch
            {
                RouteTable.Routes.MapPageRoute("EmbedConfigPageRoute", "EmbedConfigErrorLog", "~/EmbedConfigErrorLog.aspx");
            }
        }
    }
}