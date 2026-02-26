using System;
using System.Net.Http;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using BoldBIEmbedSample.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BoldBIEmbedSample
{
    public partial class _Default : Page
    {
        [WebMethod()]
        public static string TokenGeneration()
        {
            var embedDetails = new
            {
                email = GlobalAppSettings.EmbedDetails.UserEmail,
                serverurl = GlobalAppSettings.EmbedDetails.ServerUrl,
                siteidentifier = GlobalAppSettings.EmbedDetails.SiteIdentifier,
                embedsecret = GlobalAppSettings.EmbedDetails.EmbedSecret,
                dashboard = new  // Dashboard ID property is mandatory only when using BoldBI version 14.1.11.
                {
                    id = GlobalAppSettings.EmbedDetails.DashboardId
                }
            };

            //Post call to Bold BI server
            var client = new HttpClient();
            var requestUrl = $"{embedDetails.serverurl}/api/{embedDetails.siteidentifier}/embed/authorize";

            var jsonPayload = JsonConvert.SerializeObject(embedDetails);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var result = client.PostAsync(requestUrl, httpContent).Result;
            var resultContent = result.Content.ReadAsStringAsync().Result;

            // Extract token from server response
            var json = JObject.Parse(resultContent);
            var token = json["Data"]?["access_token"]?.ToString() ?? json["access_token"]?.ToString();

            // Write raw token to the HTTP response so client-side `response.text()` returns the token string
            var resp = System.Web.HttpContext.Current.Response;
            resp.Clear();
            resp.ContentType = "text/plain";
            resp.Write(token);
            resp.Flush();
            resp.End();

            return token;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}