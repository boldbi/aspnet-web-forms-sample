using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoldBIEmbedSample.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BoldBIEmbedSample
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonFilePath = Server.MapPath("~/embedConfig.json"); // Path to the JSON file
            string json = File.ReadAllText(jsonFilePath);
            var serializer = new JavaScriptSerializer();
            var jsonData = serializer.Deserialize<EmbedDetails>(json);
            string dashboardId = jsonData.DashboardId;
            string serverUrl = jsonData.ServerUrl;
            string userEmail = jsonData.UserEmail;

            //  var message = "content";

        }

        //[WebMethod()]
        //public IActionResult GetConfig()
        //{
        //    var jsonData = System.IO.File.ReadAllText("embedConfig.json");
        //    string basePath = AppDomain.CurrentDomain.BaseDirectory;
        //    string jsonString = System.IO.File.ReadAllText(Path.Combine(basePath, "embedConfig.json"));
        //    GlobalAppSettings.EmbedDetails = JsonConvert.DeserializeObject<EmbedDetails>(jsonString);
        //    return OK(jsonData);
        //}

        [WebMethod()]
        public static void GetEmbedDetails(string embedQuerString, string dashboardServerApiUrl)
        {
            embedQuerString += "&embed_user_email=" + EmbedProperties.UserEmail;
            //To set embed_server_timestamp to overcome the EmbedCodeValidation failing while different timezone using at client application.
            double timeStamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            embedQuerString += "&embed_server_timestamp=" + timeStamp;
            var embedDetailsUrl = "/embed/authorize?" + embedQuerString + "&embed_signature=" + GetSignatureUrl(embedQuerString);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(dashboardServerApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                var result = client.GetAsync(dashboardServerApiUrl + embedDetailsUrl).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                //return resultContent;
                var js = new System.Web.Script.Serialization.JavaScriptSerializer();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                HttpContext.Current.Response.Write(js.Serialize(resultContent));
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        public static string GetSignatureUrl(string message)
        {
            var encoding = new System.Text.UTF8Encoding();
            var keyBytes = encoding.GetBytes(EmbedProperties.EmbedSecret);
            var messageBytes = encoding.GetBytes(message);
            using (var hmacsha1 = new HMACSHA256(keyBytes))
            {
                var hashMessage = hmacsha1.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashMessage);
            }
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
    }
}