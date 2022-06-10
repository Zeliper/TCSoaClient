using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Teamcenter.Soa.Client.Model.Strong;

namespace FourTierSharp.TcSession
{
    public class TCClient
    {
        public string Host { get; set; } = "https://qps-detest.singlex.com";
        public string AssetId { get; set; }
        public Dictionary<string, string> TypeIconMap { get; set; } = new Dictionary<string, string>();
        public Session CurrentSession { get; set; } = null;
        public User CurrentUser { get; set; } = null;
        public bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        [Serializable]
        private class TypeFiles
        {
            public List<string> typeFiles { get; set; } = new List<string>();
        }

        [Obsolete]
        public TCClient(string userId, string passwd, string host = null)
        {
            if (host != null) Host = host;
            try
            {
                using (WebClient web = new WebClient())
                {
                    string data = web.DownloadString((this.Host + "/tc.html"));
                    string assetPattern = @"src=""./(?<assetId>[\w\d]+)/";
                    AssetId = Regex.Match(data, assetPattern).Groups["assetId"].Value;

                    if (!Directory.Exists("Icons"))
                        Directory.CreateDirectory("Icons");

                    //foreach (var item in Directory.GetFiles("Icons"))
                    //{
                    //    File.Delete(item);
                    //}

                    string typeFileJson = web.DownloadString($"{this.Host}/{this.AssetId}/config/typeFiles.json");
                    TypeFiles typeFiles = JsonSerializer.Deserialize<TypeFiles>(typeFileJson);
                    foreach (var type in typeFiles.typeFiles)
                    {
                        string typeName = type.Substring(4, type.Length - 6);
                        string url = $@"{this.Host}/{this.AssetId}/image/type{typeName}48.svg";
                        //MessageBox.Show(url);
                        if (!TypeIconMap.ContainsKey(typeName))
                            TypeIconMap.Add(typeName, url);
                    }
                    foreach(var iconInfo in TypeIconMap)
                    {
                        try
                        {
                            if(!File.Exists($@"Icons\{iconInfo.Key}.svg"))
                                web.DownloadFile(iconInfo.Value, $@"Icons\{iconInfo.Key}.svg");
                        }catch (Exception ex)
                        {
                            
                        }
                    }
                }
                CurrentSession = new Session((Host + "/tc"));
                CurrentUser = CurrentSession.Login(userId, passwd);
            }
            catch (SystemException e)
            {
                MessageBox.Show(e.Message);
                throw e;
            }
        }
    }
}
