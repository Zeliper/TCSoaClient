using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Teamcenter.Schemas.Soa._2006_03.Exceptions;
using Teamcenter.Soa;
using Teamcenter.Soa.Common;
using Teamcenter.Soa.Client;
using Teamcenter.Soa.Exceptions;

namespace FourTierSharp.TcSession
{
    public class FTCredentialManager : CredentialManager
    {

        private string name = null;
        private string password = null;
        private string group = "";
        private string role = "";
        private string discriminator = "SoaFT";
       
        public int CredentialType
        {
            get { return SoaConstants.CLIENT_CREDENTIAL_TYPE_STD; }
        }

        public string[] GetCredentials(InvalidCredentialsException e)
        {
            //Console.WriteLine(e.Message);
            return PromptForCredentials();
        }

        public string[] GetCredentials(InvalidUserException e)
        {
            if (name == null) return PromptForCredentials();

            string[] tokens = { name, password, group, role, discriminator };
            return tokens;
        }

        public void SetGroupRole(String group, String role)
        {
            this.group = group;
            this.role = role;
        }

        public void SetUserPassword(String user, String password, String discriminator)
        {
            this.name = user;
            this.password = password;
            this.discriminator = discriminator;
        }


        public string[] PromptForCredentials()
        {
            String[] tokens = { name, password, group, role, discriminator };
            return tokens;
        }

    }
}
