using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Project_1
{
    internal class ConnectSQL
    {
        public static string strCon = @"Data Source=TUF-BIN\SQLEXPRESS;Initial Catalog=ThueXe;Integrated Security=True;Encrypt=False";
        public static SqlConnection? sqlCon = null;
        public static SqlConnection? sqlCon2 = null;
        public static SqlConnection? sqlCon3 = null;
        public static SqlConnection? sqlCon4 = null;
        public static SqlConnection? sqlCon5 = null;

        public static void Connect1()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
        }

        public static void Connect2()
        {
            if (sqlCon2 == null)
            {
                sqlCon2 = new SqlConnection(strCon);
            }

            if (sqlCon2.State == ConnectionState.Closed)
            {
                sqlCon2.Open();
            }
        }

        public static void Connect3()
        {
            if (sqlCon3 == null)
            {
                sqlCon3 = new SqlConnection(strCon);
            }

            if (sqlCon3.State == ConnectionState.Closed)
            {
                sqlCon3.Open();
            }
        }

        public static void Connect4()
        {
            if (sqlCon4 == null)
            {
                sqlCon4 = new SqlConnection(strCon);
            }

            if (sqlCon4.State == ConnectionState.Closed)
            {
                sqlCon4.Open();
            }
        }

        public static void Connect5()
        {
            if (sqlCon5 == null)
            {
                sqlCon5 = new SqlConnection(strCon);
            }

            if (sqlCon5.State == ConnectionState.Closed)
            {
                sqlCon5.Open();
            }
        }
    }
}
