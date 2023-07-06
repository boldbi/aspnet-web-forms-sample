using BoldBIEmbedSample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BoldBIEmbedSample
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string jsonFilePath = Server.MapPath("~/embedConfig.json"); // Path to the JSON file
            //string json = File.ReadAllText(jsonFilePath);
            //var serializer = new JavaScriptSerializer();
            //var jsonData = serializer.Deserialize<EmbedDetails>(json);
            //string dashboardId = jsonData.DashboardId;
            //string serverUrl = jsonData.ServerUrl;
            //string userEmail = jsonData.UserEmail;
        }
    }
}