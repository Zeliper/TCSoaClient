using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using FourTierSharp.TcSession;
using Teamcenter.Services.Strong.Core;
using Teamcenter.Services.Strong.Classification;
using Teamcenter.Soa.Client.Model;
using Teamcenter.Soa.Client.Model.Strong;
using Session = FourTierSharp.TcSession.Session;
using System.Net;

namespace FourTierSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TCClient Client { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Client = LoginSession());
            //MessageBox.Show(Client.CurrentUser.User_name);
            //DataManagementService dmService = DataManagementService.getService(Session.GetConnection());
            //MessageBox.Show(Client.AssetId);

            //Folder homeFolder = Client.CurrentUser.Home_folder;
            //ModelObject[] objects = { homeFolder };
            //string[] attributes = { "contents" };
            //dmService.GetProperties(objects, attributes);
            //var contents = homeFolder.Contents;
            //string msg = "";
            ////var fndIcon = new Fnd0Icon(contents[0].SoaType,contents[0].Uid);
            //dmService.LoadObjects(contents.ToArray().Select(_e=>_e.Uid).ToArray());
            //ClassificationService cfService = ClassificationService.getService(Session.GetConnection());
            //string[] attrs = { "object_name", "object_desc", "fnd0objectId" };
            //dmService.GetProperties(contents, attrs);

            //foreach (WorkspaceObject o in contents)
            //{
            //    msg += String.Format("{0} : {1}\n",o.Uid,o.Object_name);
            //}
            //MessageBox.Show(msg);
        }

        [Obsolete]
        private TCClient LoginSession()
        {
            
            return new TCClient(TB_ID.Text, TB_PW.Text);
        }
    }
}
