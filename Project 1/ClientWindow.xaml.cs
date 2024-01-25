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
using System.Data.SqlClient;
using System.Data;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void btnFindTranspost_Click(object sender, RoutedEventArgs e)
        {
            FindTransport findTransport = new FindTransport();
            findTransport.ShowDialog();
        }

        private void btnContractPayments_Click(object sender, RoutedEventArgs e)
        {
            ContractPaymentsWindow contractPaymentsWindow = new ContractPaymentsWindow();
            contractPaymentsWindow.ShowDialog();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT Name, CCCDClient, Vote FROM dbo.Client";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            lvClient.DataContext = dt;

            reader.Close();
        }
    }
}
