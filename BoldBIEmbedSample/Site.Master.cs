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
        protected string GetClientEmbedConfigJson()
        {
            string embedConfigPath = Server.MapPath("~/embedConfig.json");
            string jsonString = System.IO.File.ReadAllText(embedConfigPath);
            GlobalAppSettings.EmbedDetails = JsonConvert.DeserializeObject<EmbedDetails>(jsonString);

            var embedConfig = new EmbedDetails
            {
                DashboardId = GlobalAppSettings.EmbedDetails.DashboardId,
                SiteIdentifier = GlobalAppSettings.EmbedDetails.SiteIdentifier,
                ServerUrl = GlobalAppSettings.EmbedDetails.ServerUrl,
                EmbedType = GlobalAppSettings.EmbedDetails.EmbedType,
                Environment = GlobalAppSettings.EmbedDetails.Environment
            };

            HttpContext.Current.Items["EmbedConfigData"] = embedConfig;
            var embedConfigData = HttpContext.Current.Items["EmbedConfigData"] as EmbedDetails;
            return JsonConvert.SerializeObject(embedConfigData);
        }
    }
}