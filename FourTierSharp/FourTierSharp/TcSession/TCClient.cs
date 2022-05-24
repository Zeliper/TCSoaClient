using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Teamcenter.Soa.Client.Model.Strong;

namespace FourTierSharp.TcSession
{
    public class TCClient
    {
        public string Host { get; set; } = "http://localhost:8080/tc";
        public Session CurrentSession { get; set; } = null;
        public User CurrentUser { get; set; } = null;
        public bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        [Obsolete]
        public TCClient(string userId, string passwd, string host = null)
        {
            if (host != null) Host = host;
            try
            {
                CurrentSession = new Session(Host);
                CurrentUser = CurrentSession.Login(userId, passwd);
            }
            catch (SystemException e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.InnerException.Message);
            }
        }
    }
}
