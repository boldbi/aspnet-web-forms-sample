using BoldBIEmbedSample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BoldBIEmbedSample
{
    public partial class SiteMaster : MasterPage
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
            protected string GetDashboardInfoJson()
            {
            DashboardInfo dashboardInfo1 = null;
            string embedConfigPath = Server.MapPath("~/embedConfig.json");
            string jsonString = System.IO.File.ReadAllText(embedConfigPath);
            GlobalAppSettings.EmbedDetails = JsonConvert.DeserializeObject<EmbedDetails>(jsonString);


            // Check for null to avoid potential issues
            if (GlobalAppSettings.EmbedDetails != null)
            {
                // Create an instance of DashboardInfo and set properties
                 dashboardInfo1 = new DashboardInfo
                {
                    DashboardId = GlobalAppSettings.EmbedDetails.DashboardId,
                    SiteIdentifier = GlobalAppSettings.EmbedDetails.SiteIdentifier,
                    ServerUrl = GlobalAppSettings.EmbedDetails.ServerUrl,
                    EmbedType = GlobalAppSettings.EmbedDetails.EmbedType,
                    Environment = GlobalAppSettings.EmbedDetails.Environment
                };

            }
            HttpContext.Current.Items["DashboardInfo"] = dashboardInfo1;
            // Retrieve the dashboardInfo from HttpContext.Current.Items
            var dashboardInfo = HttpContext.Current.Items["DashboardInfo"] as DashboardInfo;

                if (dashboardInfo != null)
                {
                    // Serialize the dashboardInfo to JSON
                    return JsonConvert.SerializeObject(dashboardInfo);
                }
                else
                {
                    // Return an empty object if dashboardInfo is null
                    return "{}";
                }
            }
        //}
    }
}