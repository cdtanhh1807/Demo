using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Project_1
{
    /// <summary>
    /// Interaction logic for ReturnTransportWindow.xaml
    /// </summary>
    public partial class ReturnTransportWindow : Window
    {
        private Transport? transport;
        private Owner? owner;
        private Client? client;
        private Driver? driver;
        private Contract? contract;

        public ReturnTransportWindow(Owner owner, Client client, Driver driver, Transport transport, Contract contract)
        {
            InitializeComponent();
            this.owner = owner;
            this.client = client;
            this.driver = driver;
            this.transport = transport;
            this.contract = contract;
            btnEvaluate.IsEnabled = false;
            cbbOwnerVote.IsEnabled = false;
            cbbClientVote.IsEnabled = false;
            btnPayments.IsEnabled = false;
            txbOwnerEvaluate.IsEnabled = false;
            txbClientEvaluate.IsEnabled = false;
            txbPriceContract.IsEnabled = false;
        }

        private void btnReturnTransport_Click(object sender, RoutedEventArgs e)
        {
            if (cbbCost.SelectedValue != null && cbbGift.SelectedValue != null)
            {
                string[] partCost = cbbCost.SelectedValue.ToString()!.Split(':');
                string valueCost = partCost[1].Trim();

                string[] partGift = cbbGift.SelectedValue.ToString()!.Split(':');
                string valueGift = partGift[1].Trim();

                string priceContract = ReturnTransport.PreBill(contract!, transport!, valueGift, valueCost).ToString();
                txbPriceContract.Text = priceContract + "VNĐ";

                btnReturnTransport.IsEnabled = false;
                btnPayments.IsEnabled = true;
            }
            else MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnPayments_Click(object sender, RoutedEventArgs e)
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "UPDATE dbo.Contract SET Active = 1 WHERE IDContract = '" + contract!.IDContract + "'";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            reader.Read();
            reader.Close();

            ConnectSQL.Connect2();

            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.CommandText = "UPDATE dbo.Transport SET Active = NULL WHERE IDTransport = '" + transport!.IDTransport + "'";

            sqlCmd2.Connection = ConnectSQL.sqlCon2;

            SqlDataReader reader2 = sqlCmd2.ExecuteReader();
            reader2.Read();
            reader2.Close();

            MessageBox.Show("Thanh toán hợp đồng thành công! Hãy đánh giá cho lần hợp tác này", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            btnEvaluate.IsEnabled = true;
            txbOwnerEvaluate.IsEnabled = true;
            txbClientEvaluate.IsEnabled = true;
            cbbClientVote.IsEnabled = true;
            cbbOwnerVote.IsEnabled = true;
            btnPayments.IsEnabled = false;
        }

        private void CloseWindow()
        {
            var window = Application.Current.Windows;
            var returnWindow = window.OfType<ReturnTransportWindow>().FirstOrDefault();
            if (returnWindow != null)
            {
                returnWindow.Close();
            }
        }

        private void btnEvaluate_Click(object sender, RoutedEventArgs e)
        {
            string[] partOwner = cbbOwnerVote.SelectedValue.ToString()!.Split(':');
            string valueOwner = partOwner[1].Trim();

            string[] partClient = cbbClientVote.SelectedValue.ToString()!.Split(':');
            string valueClient = partClient[1].Trim();

            string[] partDriver = cbbDriverVote.SelectedValue.ToString()!.Split(':');
            string valueDriver = partDriver[1].Trim();

            owner!.AddEvaluate(valueOwner, txbOwnerEvaluate.Text.Trim());
            client!.AddEvaluate(valueClient, txbClientEvaluate.Text.Trim());
            driver!.AddEvaluate(valueDriver, txbDriverEvaluate.Text.Trim());

            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT dbo.Evaluate VALUES (@cccdOwner, @cccdClient, @vote, @comment)";

            sqlCmd.Parameters.AddWithValue("@cccdOwner", owner.CCCD);
            sqlCmd.Parameters.AddWithValue("@cccdClient", client.CCCD);
            sqlCmd.Parameters.AddWithValue("@vote", valueOwner);
            sqlCmd.Parameters.AddWithValue("@comment", txbOwnerEvaluate.Text.Trim());

            sqlCmd.Connection = ConnectSQL.sqlCon;

            int kq = sqlCmd.ExecuteNonQuery();

            ConnectSQL.Connect2();

            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.CommandText = "UPDATE dbo.Client SET Vote = @vote WHERE CCCDClient = '" + client.CCCD + "'";

            sqlCmd2.Parameters.AddWithValue("@vote", client.Vote.ToString());

            sqlCmd2.Connection = ConnectSQL.sqlCon2;

            int kq2 = sqlCmd2.ExecuteNonQuery();

            ConnectSQL.Connect3();

            SqlCommand sqlCmd3 = new SqlCommand();
            sqlCmd3.CommandType = CommandType.Text;
            sqlCmd3.CommandText = "UPDATE dbo.Owner SET Vote = @vote WHERE CCCDOwner = '" + owner.CCCD + "'";

            sqlCmd3.Parameters.AddWithValue("@vote", owner.Vote.ToString());

            sqlCmd3.Connection = ConnectSQL.sqlCon3;

            int kq3 = sqlCmd3.ExecuteNonQuery();

            ConnectSQL.Connect4();

            SqlCommand sqlCmd4 = new SqlCommand();
            sqlCmd4.CommandType = CommandType.Text;
            sqlCmd4.CommandText = "UPDATE dbo.Driver SET Vote = @vote WHERE CCCDDriver = '" + driver.CCCD + "'";

            sqlCmd4.Parameters.AddWithValue("@vote", driver.Vote.ToString());

            sqlCmd4.Connection = ConnectSQL.sqlCon4;

            int kq4 = sqlCmd4.ExecuteNonQuery();

            if (kq > 0 && kq2 > 0 && kq3 > 0 && kq4 > 0)
            {
                MessageBox.Show("Đánh giá thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            btnEvaluate.IsEnabled = false;
            CloseWindow();
        }
    }
}
