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
using System.Windows.Media.Animation;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        private bool check = true;

        public OwnerWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT Name, CCCDOwner FROM dbo.Owner";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            cbbOwner.Items.Clear();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                string cccd = reader.GetString(1);
                cbbOwner.Items.Add(name + " :                                                            " + cccd);
            }
            reader.Close();
        }

        private void ShowTransport(string value)
        {
            ConnectSQL.Connect1();
            
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM dbo.Transport WHERE CCCDOwner = '" + value + "'";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            lvTransport.DataContext = dt;

            reader.Close();
        }

        private void cbbOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (check)
            {
                string[] part = cbbOwner.SelectedItem.ToString()!.Split(':');
                string value = part[1].Trim();
                check = false;
                ShowTransport(value);
                check = true;

                txbOwnerName.Text = part[0].Trim();
                txbOwnerCccd.Text = value;
                GetOwnerInfo(value);
            }
        }

        private void lvTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (check)
            {
                if (lvTransport.SelectedItem != null)
                {
                    DataRowView dataRow = (DataRowView)lvTransport.SelectedItem;

                    string km = dataRow["Km"].ToString()!;
                    string price = dataRow["Price"].ToString()!;
                    string status = dataRow["Status"].ToString()!;

                    txbOwnerTransKm.Text = km;
                    txbOwnerPrice.Text = price;
                    txbOwnerStatus.Text = status.ToString();
                }
            }
        }

        private void GetOwnerInfo(string cccd)
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT Phone, Address, Vote FROM dbo.Owner WHERE CCCDOwner = '" + cccd + "'";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            if (reader.Read())
            {
                string phone = reader.GetString(0);
                string address = reader.GetString(1);
                double vote = reader.GetDouble(2);

                txbOwnerPhone.Text = phone;
                txbOwnerAddress.Text = address;
                txbOwnerVote.Text = vote.ToString();
            }
            reader.Close();
        }

        private void btnSaveOwner_Click(object sender, RoutedEventArgs e)
        {
            //if ()
            ConnectSQL.Connect2();

            string km = txbOwnerTransKm.Text.Trim();
            string price = txbOwnerPrice.Text.Trim();
            string status = txbOwnerStatus.Text.Trim();
            DataRowView dataRow = (DataRowView)lvTransport.SelectedItem;
            string iDTransport = dataRow["IDTransport"].ToString()!;

            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.CommandText = "UPDATE dbo.Transport SET Km = @km, Price = @price, Status = @status WHERE IDTransport = @iDTransport";

            sqlCmd2.Parameters.AddWithValue("@km", km);
            sqlCmd2.Parameters.AddWithValue("@price", price);
            sqlCmd2.Parameters.AddWithValue("@status", status);
            sqlCmd2.Parameters.AddWithValue("@iDTransport", iDTransport);

            sqlCmd2.Connection = ConnectSQL.sqlCon2;

            string phone = txbOwnerPhone.Text.Trim();
            string address = txbOwnerAddress.Text.Trim();
            string cccd = txbOwnerCccd.Text.Trim();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "UPDATE dbo.Owner SET Phone = @phone, Address = @address WHERE CCCDOwner = @cccd";

            sqlCmd.Parameters.AddWithValue("@phone", phone);
            sqlCmd.Parameters.AddWithValue("@address", address);
            sqlCmd.Parameters.AddWithValue("@cccd", cccd);

            sqlCmd.Connection = ConnectSQL.sqlCon;

            try
            {
                int equal = sqlCmd.ExecuteNonQuery();
                int equal2 = sqlCmd2.ExecuteNonQuery();
                if (equal > 0 && equal2 > 0)
                {
                    MessageBox.Show("Chỉnh sửa dữ liệu thành công!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Chỉnh sửa dữ liệu không thành công!");
                MessageBox.Show("Lỗi SQL: " + ex.Message);
            }
        }
    }
}
