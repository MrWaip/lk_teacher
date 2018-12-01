using LK_Teacher.Modules.Utility;
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
using System.Windows.Shapes;

namespace LK_Teacher.Modules.View
{

    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();

            //Инициализациия настроек

            ServerHost.Text = Properties.Settings.Default.ServerHost;
            ServerPort.Text = Properties.Settings.Default.ServerPort;
            DatabaseName.Text = Properties.Settings.Default.Database;
            Username.Text = Properties.Settings.Default.Username;
            Password.Text = Properties.Settings.Default.Password;
            ServerUrl.Text = Properties.Settings.Default.ServerUrl;
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ServerHost = ServerHost.Text;
            Properties.Settings.Default.ServerPort = ServerPort.Text;
            Properties.Settings.Default.Database = DatabaseName.Text;
            Properties.Settings.Default.Username = Username.Text;
            Properties.Settings.Default.Password = Password.Text;
            Properties.Settings.Default.ServerUrl = ServerUrl.Text;
            Properties.Settings.Default.ApiUrl = ServerUrl.Text + @"/api/";
            Properties.Settings.Default.ImagesUrl = ServerUrl.Text + @"/images/";
            Properties.Settings.Default.Save();

            string connstr = $"server={Properties.Settings.Default.ServerHost};" +
                $"port={Properties.Settings.Default.ServerPort};" +
                $"user={Properties.Settings.Default.Username};" +
                $"database={Properties.Settings.Default.Database};" +
                $"password={Properties.Settings.Default.Password};" +
                $"SslMode=none;";

            DBApi.InitializeApi(connstr);
            this.Close();
        }
    }
}
