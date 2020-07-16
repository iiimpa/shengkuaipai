using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebApi.Migrations;

namespace WebApi.Helpers
{
    public class _5118apis
    {
        private DbContext _db;
        private IConfiguration _config;
        public _5118apis(DbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public JObject Get(string api,string domain, int pageindex,int page_size)
        {
            String querys = "";
            String bodys = $"url={domain}&page_index={pageindex}&page_size={page_size}";
            String url = _config["_5118apis:host"] + _config[$"_5118apis:apis:{api}:path"];
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }
            if (_config["_5118apis:host"].Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = _config[$"_5118apis:apis:{api}:method"];
            httpRequest.Headers.Add("Authorization", _config[$"_5118apis:apis:{api}:key"]);
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));   
            return (JObject)JsonConvert.DeserializeObject(reader.ReadToEnd());
        }
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
