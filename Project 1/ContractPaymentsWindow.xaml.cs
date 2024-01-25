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
using System.Data;
using System.Data.SqlClient;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for ContractPaymentsWindow.xaml
    /// </summary>
    public partial class ContractPaymentsWindow : Window
    {
        string cccdClient = "";
        string cccdOwner = "";
        string iDContract = "";
        string cccdDriver = "";
        string iDTransport = "";
        Client? client = new Client();
        Owner? owner = new Owner();
        Contract? contract = new Contract();
        Driver driver = new Driver();
        Transport transport = new Transport();

        public ContractPaymentsWindow()
        {
            InitializeComponent();
        }

        private void CreateObject()
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM dbo.Client WHERE CCCDClient = '" + cccdClient + "'";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            if (reader.Read())
            {
                client!.Name = reader.GetString(0);
                client.CCCD = reader.GetString(1);
                client.Phone = reader.GetString(2);
                client.Address = reader.GetString(3);
                double temp = reader.GetDouble(4);
                client.Vote = (float)temp;
            }
            reader.Close();

            ConnectSQL.Connect2();

            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.CommandText = "SELECT * FROM dbo.Owner WHERE CCCDOwner = '" + cccdOwner + "'";

            sqlCmd2.Connection = ConnectSQL.sqlCon2;

            SqlDataReader reader2 = sqlCmd2.ExecuteReader();
            if (reader2.Read())
            {
                owner!.Name = reader2.GetString(0);
                owner.CCCD = reader2.GetString(1);
                owner.Phone = reader2.GetString(2);
                owner.Address = reader2.GetString(3);
                double temp = reader2.GetDouble(4);
                owner.Vote = (float)temp;
            }
            reader2.Close();

            ConnectSQL.Connect3();

            SqlCommand sqlCmd3 = new SqlCommand();
            sqlCmd3.CommandType = CommandType.Text;
            sqlCmd3.CommandText = "SELECT * FROM dbo.Contract WHERE IDContract = '" + iDContract + "'";

            sqlCmd3.Connection = ConnectSQL.sqlCon3;

            SqlDataReader reader3 = sqlCmd3.ExecuteReader();
            if (reader3.Read())
            {
                contract!.CCCDOwner = reader3.GetString(0);
                contract.CCCDClient = reader3.GetString(1);
                contract.CCCDDriver = reader3.GetString(2);
                cccdDriver = contract.CCCDDriver;
                contract.IDTransport = reader3.GetString(3);
                iDTransport = contract.IDTransport;
                contract.RentalDate = reader3.GetDateTime(4);
                contract.RentalDays = reader3.GetInt32(5);
                contract.Deposit = reader3.GetString(6);
                contract.IDContract = reader3.GetString(7);
            }
            reader3.Close();

            ConnectSQL.Connect4();

            SqlCommand sqlCmd4 = new SqlCommand();
            sqlCmd4.CommandType = CommandType.Text;
            sqlCmd4.CommandText = "SELECT * FROM dbo.Driver WHERE CCCDDriver = '" + cccdDriver + "'";

            sqlCmd4.Connection = ConnectSQL.sqlCon4;
            SqlDataReader reader4 = sqlCmd4.ExecuteReader();
            if (reader4.Read())
            {
                driver.Name = reader4.GetString(0);
                driver.CCCD = reader4.GetString(1);
                driver.Phone = reader4.GetString(2);
                driver.Address = reader4.GetString(3);
                double temp = reader4.GetDouble(4);
                driver.Vote = (float)temp;
            }
            reader4.Close();

            ConnectSQL.Connect5();

            SqlCommand sqlCmd5 = new SqlCommand();
            sqlCmd5.CommandType = CommandType.Text;
            sqlCmd5.CommandText = "SELECT * FROM dbo.Transport WHERE IDTransport = '" + iDTransport + "'";

            sqlCmd5.Connection = ConnectSQL.sqlCon5;
            SqlDataReader reader5 = sqlCmd5.ExecuteReader();
            if (reader5.Read())
            {
                transport.IDTransport = reader5.GetString(0);
                transport.Seat = reader5.GetString(1);
                transport.Type = reader5.GetString(2);
                transport.Brand = reader5.GetString(3);
                transport.YearOfPurchase = reader5.GetInt32(4);
                transport.Km = reader5.GetString(5);
                transport.Insurance = reader5.GetBoolean(6);
                transport.Price = reader5.GetString(7);
                transport.Status = reader5.GetString(8);
            }
            reader5.Close();
        }

        private void btnPayments_Click(object sender, RoutedEventArgs e)
        {
            if (txblIDContract.Text.Length > 0)
            {
                CreateObject();
                ContractWindow contractWindow = new ContractWindow(owner!, client!, driver, transport, contract!);
                contractWindow.ShowDialog();
            }
            else MessageBox.Show("Điền đầy đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private string GetCBBPayments(string cccdClient)
        {
            ConnectSQL.Connect2();

            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.CommandText = "SELECT Name FROM dbo.Client WHERE CCCDClient = '" + cccdClient + "'";

            sqlCmd2.Connection = ConnectSQL.sqlCon2;

            SqlDataReader reader = sqlCmd2.ExecuteReader();
            string result = "";
            if (reader.Read())
            {
                result = reader.GetString(0);
            }
            reader.Close();
            return result;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT CCCDClient FROM dbo.Contract WHERE Active = 0";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            List<string> list = new List<string>();

            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            reader.Close();
            
            if (list.Count > 0)
            {
                cbb.Items.Clear();
                for (int i=0; i<list.Count; i++)
                {
                    string name = GetCBBPayments(list[i]);
                    cbb.Items.Add(name);
                }
            }
        }

        private void cbb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConnectSQL.Connect1();

            string? nameClient = cbb.SelectedItem as string;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT CCCDClient FROM dbo.Client WHERE Name = N'" + nameClient + "'";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            string cccdClient = "";
            if (reader.Read())
            {
                cccdClient = reader.GetString(0);
            }
            reader.Close();

            ConnectSQL.Connect2();

            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.CommandText = "SELECT CCCDOwner, IDContract FROM dbo.Contract WHERE CCCDClient = '" + cccdClient + "' AND Active = 0";

            sqlCmd2.Connection = ConnectSQL.sqlCon2;

            string cccdOwner = "";

            SqlDataReader reader2 = sqlCmd2.ExecuteReader();
            if (reader2.Read())
            {
                cccdOwner = reader2.GetString(0);
                txblIDContract.Text = reader2.GetString(1);
            }
            reader2.Close();

            ConnectSQL.Connect3();
            SqlCommand sqlCmd3 = new SqlCommand();
            sqlCmd3 .CommandType = CommandType.Text;
            sqlCmd3.CommandText = "SELECT Name FROM dbo.Owner WHERE CCCDOwner = '" + cccdOwner + "'";

            sqlCmd3.Connection = ConnectSQL.sqlCon3;
            
            SqlDataReader reader3 = sqlCmd3.ExecuteReader();
            if (reader3.Read())
            {
                txblOwner.Text = reader3.GetString(0);
            }
            reader3.Close();

            this.cccdClient = cccdClient;
            this.cccdOwner = cccdOwner;
            this.iDContract = txblIDContract.Text.Trim();
        }
    }
}
