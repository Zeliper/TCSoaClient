using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Teamcenter.Schemas.Soa._2006_03.Exceptions;
using Teamcenter.Services.Strong.Core;
using Teamcenter.Services.Strong.Core._2006_03.Session;
using Teamcenter.Soa;
using Teamcenter.Soa.Client;
using Teamcenter.Soa.Client.Model;
using Teamcenter.Soa.Exceptions;

using User = Teamcenter.Soa.Client.Model.Strong.User;
using WorkspaceObject = Teamcenter.Soa.Client.Model.Strong.WorkspaceObject;

namespace FourTierSharp.TcSession
{
    public class Session
    {
        #region Private
        private static Connection connection;
        private static FTCredentialManager credentialManager;
        #endregion

        #region Public
        #endregion


        /// <summary>
        /// Session Class Initialize
        /// </summary>
        /// <param name="host">http://localhost/tc 형식의 호스트 주소</param>
        public Session(string host)
        {
            credentialManager = new FTCredentialManager();
            string proto = null;
            string envNameTccs = null;
            if (host.StartsWith("http"))
            {
                proto = SoaConstants.HTTP;
            }
            else if (host.StartsWith("tccs"))
            {
                proto = SoaConstants.TCCS;
                int envNameStart = host.IndexOf('/') + 2;
                envNameTccs = host.Substring(envNameStart, host.Length - envNameStart);
            }
            connection = new Connection(host, new System.Net.CookieCollection(), credentialManager, SoaConstants.REST, proto, false);
            if (proto == SoaConstants.TCCS) connection.SetOption(Connection.TCCS_ENV_NAME, envNameTccs);

            connection.ExceptionHandler = new FTExceptionHandler();

            connection.ModelManager.AddPartialErrorListener(new FTPartialErrorListener());

            connection.ModelManager.AddModelEventListener(new FTModelEventListener());

            Connection.AddRequestListener(new FTRequestListener());
        }
        
        #region Getters for private
        public static Connection GetConnection() { return connection; }
        #endregion

        [Obsolete]
        public User Login(string userId, string passwd)
        {
            SessionService sessionService = SessionService.getService(connection);
            try
            {
                string[] credentials = credentialManager.PromptForCredentials();
                credentials[0] = userId;
                credentials[1] = passwd;
                while (true)
                {
                    try
                    {
                        LoginResponse response = sessionService.Login(credentials[0], credentials[1], credentials[2], credentials[3], credentials[4]);

                        return response.User;
                    }catch (InvalidCredentialsException e)
                    {
                        credentials = credentialManager.GetCredentials(e);
                    }
                }
            }
            catch (CanceledOperationException){}
            return null;
        }

        public void Logout()
        {
            SessionService sessionService = SessionService.getService(connection);
            try
            {
                sessionService.Logout();
            }
            catch (ServiceException) { }
        }

        private static void getUsers(ModelObject[] objects)
        {
            if (objects == null) return;
            DataManagementService dmService = DataManagementService.getService(Session.GetConnection());
            List<User> unKnownUsers = new List<User>();
            foreach(ModelObject obj in objects)
            {
                if(!(obj is WorkspaceObject))
                {
                    WorkspaceObject wo = (WorkspaceObject)obj;
                    User owner = null;
                    try
                    {
                        owner = (User)wo.Owning_user;
                        String userName = owner.User_name;
                    }
                    catch (NotLoadedException)
                    {
                        if(owner != null) unKnownUsers.Add(owner);
                    }
                }
            }
            string[] attributes = { "user_name" };
            dmService.GetProperties(unKnownUsers.ToArray(), attributes);
        }
    }
}
