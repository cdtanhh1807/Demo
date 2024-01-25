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
using System.Windows.Controls.Primitives;
using System.Globalization;

namespace Project_1
{
    /// <summary>
    /// Interaction logic for FindTransport.xaml
    /// </summary>
    public partial class FindTransport : Window
    {
        //private bool check = true;
        DateTime rentalDate;
        DateTime DateAcvite;

        public FindTransport()
        {
            InitializeComponent();
        }

        private void btnFindTransport_Click(object sender, RoutedEventArgs e)
        {
            if (cbbFTransSeat.SelectedItem == null || cbbFTransType.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn đầy đủ thông tin xe cần tìm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtp.SelectedDate.HasValue) rentalDate = (DateTime)dtp.SelectedDate;
                else
                {
                    rentalDate = DateTime.Now;
                    dtp.SelectedDate = rentalDate;
                }

                DateAcvite = rentalDate;
                DateAcvite = DateAcvite.AddDays(Convert.ToInt32(txbRentalDays.Text.Trim()));

                ConnectSQL.Connect1();

                string rentalDateToString = rentalDate.ToString("yyyy-MM-dd");

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT * FROM dbo.Transport WHERE Type = @type AND Seat = @seat AND ( Active < CONVERT(DATE, @rentalDate) OR Active > CONVERT(DATE, @dateActive) OR Active IS NULL )";

                sqlCmd.Parameters.AddWithValue("@type", cbbFTransType.SelectedItem.ToString());
                sqlCmd.Parameters.AddWithValue("@seat", cbbFTransSeat.SelectedItem.ToString());
                sqlCmd.Parameters.AddWithValue("@rentalDate", rentalDate.ToString("yyyy-MM-dd"));
                sqlCmd.Parameters.AddWithValue("@dateActive", DateAcvite.ToString("yyyy-MM-dd"));

                sqlCmd.Connection = ConnectSQL.sqlCon;

                SqlDataReader reader = sqlCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                lvTransport.DataContext = dt;

                reader.Close();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectSQL.Connect1();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT Type, Seat FROM dbo.Transport";

            sqlCmd.Connection = ConnectSQL.sqlCon;

            SqlDataReader reader = sqlCmd.ExecuteReader();

            HashSet<string> listType = new HashSet<string>();
            HashSet<string> listSeat = new HashSet<string>();

            cbbFTransType.Items.Clear();
            cbbFTransSeat.Items.Clear();
            while (reader.Read())
            {
                string type = reader.GetString(0);
                string seat = reader.GetString(1);

                listType.Add(type);
                listSeat.Add(seat);
            }
            cbbFTransType.ItemsSource = listType;
            cbbFTransSeat.ItemsSource = listSeat;
            reader.Close();
        }

        private void btnFindALl_Click(object sender, RoutedEventArgs e)
        {
            if (txbRentalDays.Text.Length > 0)
            {
                if (dtp.SelectedDate.HasValue) rentalDate = (DateTime)dtp.SelectedDate;
                else
                {
                    rentalDate = DateTime.Now;
                    dtp.SelectedDate = rentalDate;
                }

                DateAcvite = rentalDate;
                DateAcvite = DateAcvite.AddDays(Convert.ToInt32(txbRentalDays.Text.Trim()));

                ConnectSQL.Connect1();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT * FROM dbo.Transport WHERE Active < CONVERT(DATE, @rentalDate) OR Active > CONVERT(DATE, @dateActive) OR Active IS NULL";

                sqlCmd.Parameters.AddWithValue("@rentalDate", rentalDate.ToString("yyyy-MM-dd"));
                sqlCmd.Parameters.AddWithValue("@dateActive", DateAcvite.ToString("yyyy-MM-dd"));

                sqlCmd.Connection = ConnectSQL.sqlCon;

                SqlDataReader reader = sqlCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                lvTransport.DataContext = dt;

                reader.Close();
            }
            else MessageBox.Show("Hãy chọn đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void lvTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)lvTransport.SelectedItem;
            if (dataRow != null )
            {
                rentalDate = dtp.SelectedDate ?? DateTime.Now;

                string iDTransport = dataRow[0].ToString()!;
                string seat = dataRow[1].ToString()!;
                string type = dataRow[2].ToString()!;
                string brand = dataRow[3].ToString()!;
                int yearOfPurchase = int.Parse(dataRow[4].ToString()!);
                string km = dataRow[5].ToString()!;
                bool insurance = bool.Parse(dataRow[6].ToString()!);
                string price = dataRow[7].ToString()!;
                string status = dataRow[8].ToString()!;
                string cCCCDOwner = dataRow[9].ToString()!;

                Transport transport = new Transport(iDTransport, seat, type, brand, yearOfPurchase, km, insurance, price, status, cCCCDOwner);

                ContractWindow contractWindow = new ContractWindow(transport, rentalDate);
                contractWindow.ShowDialog();
            }
        }

        private void CheckDatePicker(DatePicker? datePicker)
        {
            if (datePicker != null && datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;

                if (selectedDate < DateTime.Today)
                {
                    datePicker.SelectedDate = null;
                    MessageBox.Show("Lỗi !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void dtp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker? datePicker = sender as DatePicker;

            CheckDatePicker(datePicker);
        }

        private void dtp_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DatePicker? datePicker = sender as DatePicker;
                CheckDatePicker(datePicker);
            }
        }
    }
}
