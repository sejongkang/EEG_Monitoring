using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace DXApplication2
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        MySqlConnection conn;
        List<ListView> lstviews;
        List<String> sql;


        public Window2()
        {
            InitializeComponent();


            openConnection();

            lstviews = new List<ListView> { lstView_number, lstView_type, lstView_date, lstView_lsb };
            sql = new List<string> { "SELECT hwNo FROM hardwareinfo", "SELECT hwType FROM hardwareinfo", "SELECT purchaseDate FROM hardwareinfo", "SELECT lsb FROM hardwareinfo" };

            for (int i = 0; i < 4; i++)
            {
                makeItem(SelectQuery(sql[i]), lstviews[i]);
            }
        }

        private void makeItem(List<String> item, ListView lstView)
        {
            TextBox[] numText = new TextBox[item.Count];
            int count = 0;
            foreach (String num in item)
            {
                numText[count] = new TextBox();
                numText[count].Text = num;
                numText[count].HorizontalContentAlignment = HorizontalAlignment.Center;
                if (lstView.Name == "lstView_lsb")
                {
                    numText[count].IsReadOnly = false;
                }
                else
                    numText[count].IsReadOnly = true;
                lstView.Items.Add(numText[count]);
                count++;
            }
        }

        public List<String> SelectQuery(string query)
        {
            List<String> emp = new List<String>();
            try
            {
                string sql = query;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    emp.Add(rdr.GetString(0));
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return emp;
        }

        private void update_lsb()
        {
            try
            {
                int hw_count = 1;
                foreach (TextBox txtb in lstView_lsb.Items)
                {
                    String lsb_value = txtb.Text;
                    string sql = "UPDATE hardwareinfo SET lsb = " + lsb_value + " WHERE hwNo = " + hw_count  + ";";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void openConnection()
        {
            String strConn = "Server=203.250.77.238;User ID=root;Password=root;Database=20190603;Min Pool Size=5;";
            conn = new MySqlConnection(strConn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            closeConnection();
            this.Close();
        }

        private void Btn_ok_Click(object sender, RoutedEventArgs e)
        {
            update_lsb();
            closeConnection();
            this.DialogResult = true;
        }
    }
}
