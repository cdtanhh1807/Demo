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
    /// Interaction logic for ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        private Transport? transport;
        private Owner? owner;
        private Client? client;
        private Driver? driver;
        private Contract? contract;
        private DateTime? rentalDate;
        private int? rentalDays;
        private string? deposit = "";
        private string? iDContract = "";

        public ContractWindow(Transport transport, DateTime rentalDate)
        {
            InitializeComponent();
            this.transport = transport;
            this.rentalDate = rentalDate;
            btnCancelCon.IsEnabled = false;
            btnContractPayments.IsEnabled = false;

            GetOWner();
            GetTrans();
            GetDriver();
            txbContractRentalDate.Text = this.rentalDate.HasValue ? this.rentalDate.Value.ToString("yyyy-MM-dd") : "N/A";
        }

        private void GetContract()
        {
            txbContractRentalDate.Text = contract!.RentalDate.ToString("yyyy-MM-dd");
            txbContractRentalDays.Text = contract.RentalDays.ToString();
            txbContractDeposit.Text = contract.Deposit;
            txbContractIDCon.Text = contract.IDContract;

            txbContractIDCon.IsEnabled = false;
            txbContractDeposit.IsEnabled = false;
            txbContractRentalDate.IsEnabled = false;
            txbContractRentalDays.IsEnabled = false;
        }

        private void GetClient()
        {
            txbClientName.Text = client!.Name;
            txbClientCCCD.Text = client.CCCD;
            txbClientPhone.Text = client.Phone;
            txbClientAddress.Text = client.Address;

            txbClientName.IsEnabled = false;
            txbClientCCCD.IsEnabled = false;
            txbClientPhone.IsEnabled = false;
            txbClientAddress.IsEnabled = false;
        }

        private void CountDatePayments()
        {
            DateTime dt = DateTime.Now;
            DateTime temp = DateTime.Now;
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT Active FROM dbo.Transport WHERE IDTransport = '" + transport!.IDTransport + "'";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            if (reader.Read())
            {
                temp = reader.GetDateTime(0);
                reader.Close();
            }
            

            if (dt >= temp) btnContractPayments.IsEnabled = true;
            else btnContractPayments.IsEnabled = false;
        }

        public ContractWindow(Owner owner, Client client, Driver driver, Transport transport, Contract contract)
        {
            InitializeComponent();
            this.owner = owner;
            this.client = client;
            this.driver = driver;
            this.transport = transport;
            this.contract = contract;

            txbOwnerName.Text = this.owner.Name;
            txbOwnerCCCD.Text = this.owner.CCCD;
            txbOwnerPhone.Text = this.owner.Phone;
            txbOwnerAddress.Text = this.owner.Address;
            
            GetTrans();
            GetClient();
            GetContract();
            cbbDriver.Items.Add(this.driver.Name.Trim());
            cbbDriver.SelectedItem = this.driver.Name.Trim();
            cbbDriver.IsEnabled = false;

            txbOwnerName.IsEnabled = false;
            txbOwnerCCCD.IsEnabled = false;
            txbOwnerPhone.IsEnabled = false;
            txbOwnerAddress.IsEnabled = false;

            txbTransBrand.IsEnabled = false;
            txbTransID.IsEnabled = false;
            txbTransInsu.IsEnabled = false;
            txbTransSeat.IsEnabled = false;
            txbTransType.IsEnabled = false;
            txbTransKm.IsEnabled = false;
            txbTransStatus.IsEnabled = false;
            txbTransPrice.IsEnabled = false;

            btnCreatCon.IsEnabled = false;
            CountDatePayments();
        }

        private void GetTrans()
        {
            txbTransID.Text = transport!.IDTransport;
            txbTransSeat.Text = transport.Seat;
            txbTransType.Text = transport.Type;
            txbTransBrand.Text = transport.Brand;
            txbTransKm.Text = transport.Km;
            txbTransStatus.Text = transport.Status;
            txbTransInsu.Text = transport.Insurance.ToString();
            txbTransPrice.Text = transport.Price.ToString();
        }

        private void GetOWner()
        {
            ConnectSQL.Connect1();

            string cccdOwner = transport!.CCCDOwner;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM dbo.Owner WHERE CCCDOwner = '" + cccdOwner + "'";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();

            if (reader.Read())
            {
                txbOwnerName.Text = reader.GetString(0);
                txbOwnerCCCD.Text = reader.GetString(1);
                txbOwnerPhone.Text = reader.GetString(2);
                txbOwnerAddress.Text = reader.GetString(3);
            }
            reader.Close();
        }
        
        private void GetDriver()
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM dbo.Driver";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            cbbDriver.Items.Clear();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                double temp = reader.GetDouble(4);
                string vote = temp.ToString();
                cbbDriver.Items.Add(name + " :       " + vote);
            }
            reader.Close();

        }

        private void CalActive(string iDTransport)
        {
            int calActive = int.Parse(txbContractRentalDays.Text.Trim());
            string format = "yyyy-MM-dd";
            DateTime dtCreatContract = DateTime.ParseExact(txbContractRentalDate.Text.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtContractTerm = dtCreatContract.AddDays(calActive);

            string dtTerm = dtContractTerm.ToString("yyyy-MM-dd");

            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType= CommandType.Text;
            sqlCmd.CommandText = "UPDATE dbo.Transport SET Active = CONVERT(date, @dtTerm) WHERE IDTransport = @iDTransport";

            sqlCmd.Parameters.AddWithValue("@dtTerm", dtTerm);
            sqlCmd.Parameters.AddWithValue("@iDTransport", iDTransport);

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();
            reader.Close();
        }

        private bool CheckClient(string cccdClient)
        {
            ConnectSQL.Connect1();

            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Client WHERE CCCDClient = @cccdClient) THEN 1 ELSE 0 END";

                sqlCmd.Parameters.AddWithValue("@cccdClient", cccdClient);
                sqlCmd.Connection = ConnectSQL.sqlCon;

                bool result = Convert.ToBoolean(sqlCmd.ExecuteScalar());
                return result;
            }
        }

        private void btnCreatCon_Click(object sender, RoutedEventArgs e)
        {
            if (txbClientCCCD.Text.Length > 0 && txbOwnerCCCD.Text.Length > 0 && txbContractDeposit.Text.Length > 0 && txbContractIDCon.Text.Length > 0 && txbContractRentalDate.Text.Length > 0 && txbContractRentalDays.Text.Length > 0)
            {
                ConnectSQL.Connect1();

                string cccdClient = txbClientCCCD.Text!.Trim();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "IF NOT EXISTS (SELECT 1 FROM dbo.Client WHERE CCCDClient = @cccdClient) BEGIN INSERT dbo.Client VALUES (@nameClient, @cccdClient, @phoneClient, @addressClient, 5) END ";

                sqlCmd.Parameters.AddWithValue("@cccdClient", cccdClient);
                sqlCmd.Parameters.AddWithValue("@nameClient", txbClientName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@phoneClient", txbClientPhone.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@addressClient", txbClientAddress.Text.Trim());

                sqlCmd.Connection = ConnectSQL.sqlCon;

                ConnectSQL.Connect2();

                string[] partDriver = cbbDriver.SelectedValue.ToString()!.Split(':');
                string valueDriver = partDriver[0].Trim();

                SqlCommand sqlCmd2 = new SqlCommand();
                sqlCmd2.CommandType = CommandType.Text;
                sqlCmd2.CommandText = "SELECT CCCDDriver From dbo.Driver WHERE Name = @nameDriver";

                sqlCmd2.Parameters.AddWithValue("nameDriver", valueDriver);

                sqlCmd2.Connection = ConnectSQL.sqlCon2;

                SqlDataReader reader = sqlCmd2.ExecuteReader();
                string cccdDriver = "";
                if (reader.Read())
                {
                    cccdDriver = reader["CCCDDriver"].ToString()!;
                }
                reader.Close();

                ConnectSQL.Connect3();

                string iDTransport = this.transport!.IDTransport;

                SqlCommand sqlCmd3 = new SqlCommand();
                sqlCmd3.CommandType = CommandType.Text;
                sqlCmd3.CommandText = "INSERT dbo.Contract VALUES(@cccdOwner, @cccdClient, @cccdDriver, @iDTransport, CONVERT(date, @rentalDate), @rentalDays, @deposit, @iDContract, 0)";

                sqlCmd3.Parameters.AddWithValue("@cccdOwner", txbOwnerCCCD.Text.Trim());
                sqlCmd3.Parameters.AddWithValue("@cccdClient", cccdClient);
                sqlCmd3.Parameters.AddWithValue("@cccdDriver", cccdDriver);
                sqlCmd3.Parameters.AddWithValue("@iDTransport", iDTransport);
                sqlCmd3.Parameters.AddWithValue("@rentalDate", txbContractRentalDate.Text.Trim());
                sqlCmd3.Parameters.AddWithValue("@rentalDays", txbContractRentalDays.Text.Trim());
                sqlCmd3.Parameters.AddWithValue("@deposit", txbContractDeposit.Text.Trim());
                sqlCmd3.Parameters.AddWithValue("@iDContract", txbContractIDCon.Text.Trim());

                sqlCmd3.Connection = ConnectSQL.sqlCon3;

                CalActive(iDTransport);

                try
                {
                    int equal = sqlCmd.ExecuteNonQuery();
                    int equal2 = sqlCmd2.ExecuteNonQuery();
                    int equal3 = sqlCmd3.ExecuteNonQuery();
                    MessageBox.Show("Tạo hợp đồng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnCreatCon.IsEnabled = false;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Tạo hợp đồng không thành công!");
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }

                btnCancelCon.IsEnabled = false;
            }
            else MessageBox.Show("Điền đầy đủ thông tin hợp đồng !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnContractPayments_Click(object sender, RoutedEventArgs e)
        {
            ReturnTransportWindow returnTransportWindow = new ReturnTransportWindow(owner!, client!, driver!, transport!, contract!);
            returnTransportWindow.ShowDialog();
        }

        private void btnCancelCon_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("BẠN CÓ MUỐN HỦY HỢP ĐỒNG NÀY ?", "THÔNG BÁO", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                ConnectSQL.Connect1();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "DELETE dbo.Contract WHERE IDContract = '" + contract!.IDContract + "'";

                sqlCmd.Connection = ConnectSQL.sqlCon;

                int kq = sqlCmd.ExecuteNonQuery();

                ConnectSQL.Connect2();

                SqlCommand sqlCmd2 = new SqlCommand();
                sqlCmd2.CommandType = CommandType.Text;
                sqlCmd2.CommandText = "UPDATE dbo.Transport SET Active = NULL WHERE IDTransport = '" + transport!.IDTransport + "'";

                sqlCmd2.Connection = ConnectSQL.sqlCon2;

                int kq2 = sqlCmd2.ExecuteNonQuery();

                if (kq > 0 && kq2 > 0) MessageBox.Show("Hủy hợp đồng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                btnCancelCon.IsEnabled = false;
            }
        }
    }
}
