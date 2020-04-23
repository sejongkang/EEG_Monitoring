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
using MySql.Data.MySqlClient;

namespace DXApplication2
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        MySqlConnection conn;
        String sel_deart = String.Empty;
        String sel_name = String.Empty;
        String query = String.Empty;
        List<String> departItem;
        List<String> nameItem;
        CollectionView view_depart;
        CollectionView view_name;

        public Window1()
        {
            InitializeComponent();
            openConnection();
            query ="SELECT Distinct department FROM workerinfo";
            departItem = SelectQuery(query);
            lstView_depart.ItemsSource = departItem;
            view_depart = (CollectionView)CollectionViewSource.GetDefaultView(lstView_depart.ItemsSource);
            view_depart.Filter = DepartFilter;
            
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            closeConnection();
            this.Close();
        }

        private void Btn_ok_Click(object sender, RoutedEventArgs e)
        {
            closeConnection();
            this.DialogResult = true;
        }

        public string Employee
        {
            get { return sel_name; }
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

        private void lstView_depart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListView)sender;
            sel_deart = (String)item.SelectedItem;
            query = "SELECT name FROM workerinfo WHERE department='"+sel_deart+"'";
            nameItem = SelectQuery(query);
            lstView_name.ItemsSource = nameItem;
            view_name = (CollectionView)CollectionViewSource.GetDefaultView(lstView_name.ItemsSource);
            view_name.Filter = NameFilter;
        }

        private void lstView_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListView)sender;
            sel_name = (String)item.SelectedItem;
        }

        private bool DepartFilter(object item)
        {
            if (String.IsNullOrEmpty(TxtBox_depart.Text))
                return true;
            else
                return ((item as String).IndexOf(TxtBox_depart.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool NameFilter(object item)
        {
            if (String.IsNullOrEmpty(TxtBox_name.Text))
                return true;
            else
                return ((item as String).IndexOf(TxtBox_name.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TxtBox_depart_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lstView_depart.ItemsSource).Refresh();
        }

        private void TxtBox_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lstView_name.ItemsSource).Refresh();
        }
        
    }
}
