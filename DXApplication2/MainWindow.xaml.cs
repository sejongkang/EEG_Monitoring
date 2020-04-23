using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using System.Threading;

namespace DXApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        MySqlConnection conn;
        double[] data = new double[8];
        Thread realtime_th;
        Thread check_th;
        Thread gauge_th;
        int limit = 500;
        string strConn = "Server=203.250.77.238;User ID=root;Password=root;Database=20190603;Min Pool Size=5;";
        string worker_name = "김영훈";
        Separator sep = new Separator();

        public MainWindow()
        {
            InitializeComponent();
            openConnection();
            realtime_th = new Thread(new ThreadStart(realtime_chart));
            check_th = new Thread(new ThreadStart(check_danger));
            gauge_th = new Thread(new ThreadStart(gauge_worker));
            default_worker();
            check_th.Start();
            realtime_th.Start();
            gauge_th.Start();
            daily_chart();
        }

        private void default_worker()
        {
            try
            {
                using (var conn = new MySqlConnection(strConn))
                {
                    conn.Open();

                    string sql = "Select name from workerinfo where selected=1";     //마지막 행

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        rdr.Read();
                        worker_name = rdr.GetString(0);
                        Txt_Now_Emp.Text = worker_name;
                        rdr.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void Btn_Emp_Sel_Click(object sender, RoutedEventArgs e)
        {
            Window1 selector = new Window1();
            selector.ShowDialog();
            if (selector.DialogResult.HasValue && selector.DialogResult.Value)
                worker_name = selector.Employee;
            Txt_Now_Emp.Text = worker_name;
            select_worker();
            daily_chart();
        }

        public void realtime_chart()
        {
            while (true)
            {
                DataSet ds = new DataSet();
                try
                {
                    using (var conn = new MySqlConnection(strConn))
                    {
                        conn.Open();

                        string sql = "SELECT * FROM realtime WHERE worker_id = (select workerNo from workerinfo where name = '"+worker_name+"') ORDER BY id_realtime DESC LIMIT " + limit.ToString();     //마지막 행

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            da.Fill(ds, "realtime");

                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                Chart_real1.DataSource = ds.Tables[0];
                                Chart_real2.DataSource = ds.Tables[0];
                                Chart_real3.DataSource = ds.Tables[0];
                                Chart_real4.DataSource = ds.Tables[0];
                            }));
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                Thread.Sleep(100);
            }
        }

        public void check_danger()
        {
            while (true)
            {
                try
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    ListBox_danger.Items.Clear();
                }));
                    using (var conn = new MySqlConnection(strConn))
                    {
                        conn.Open();

                        string sql = "Select name from workerinfo where danger=1";     //마지막 행

                        using (var cmd = new MySqlCommand(sql, conn))
                        {

                            MySqlDataReader rdr_check = cmd.ExecuteReader();
                            
                            while (rdr_check.Read())
                            {
                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                                {
                                    ListBox_danger.Items.Add(rdr_check.GetString(0));

                                    ListBox_danger.Items.Add(new Separator());
                                }));
                            }
                            rdr_check.Close();
                        }
                    }
                    Thread.Sleep(10000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        public void gauge_worker()
        {
            while (true)
            {
                try
                {
                    using (var conn = new MySqlConnection(strConn))
                    {
                        conn.Open();

                        string sql = "select resultInfo from eegresult where eegdata_eegNo IN" +
                                    "(select infoNo from datainfo where workerinfo_workerNo = " +
                                    "(Select workerno from workerinfo where name = '" + worker_name + "')) " +
                                    "ORDER BY resultNo DESC LIMIT 1";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            MySqlDataReader rdr = cmd.ExecuteReader();
                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                if (rdr.Read() == false)
                                {
                                    Gauge_now.Scales[0].Markers[0].Value = 0;
                                    Gauge_now.Scales[0].Needles[0].Value = 0;
                                }
                                else
                                {
                                    Gauge_now.Scales[0].Needles[0].Value = rdr.GetInt16(0);
                                    Gauge_now.Scales[0].Markers[0].Value = rdr.GetInt16(0);
                                }
                            }));
                            rdr.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                Thread.Sleep(1000);
            }
        }

        public void select_worker()
        {
            try
            {
                using (var conn = new MySqlConnection(strConn))
                {
                    conn.Open();

                    string sql = "UPDATE workerinfo SET selected = 0 WHERE name <> '" + worker_name + "';" +
                    "UPDATE workerinfo SET selected = 1 WHERE name = '" + worker_name + "';";
                    

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void daily_chart()
        {
            try
            {
                Chart_daily.Diagram.Series[0].Points.Clear();
                using (var conn = new MySqlConnection(strConn))
                {
                    conn.Open();

                    string sql = "SELECT result, date FROM eegresult_daily WHERE workerno = " +
                                "(select distinct workerNo from workerinfo where name = '" + worker_name + "')";     //마지막 행
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader rdr_daily = cmd.ExecuteReader();
                        List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
                        while (rdr_daily.Read())
                        {
                            int health_point = rdr_daily.GetInt16(0);
                            Chart_daily.Diagram.Series[0].Points.Add(new SeriesPoint(rdr_daily.GetDateTime(1), health_point));
                            if (health_point == 1)
                            {
                                Chart_daily.Diagram.Series[0].Points.Last().Brush = new SolidColorBrush(Color.FromRgb(22, 125, 45));
                            }
                            else if (health_point == 2)
                            {
                                Chart_daily.Diagram.Series[0].Points.Last().Brush = new SolidColorBrush(Color.FromRgb(219, 219, 29));
                            }
                            else
                            {
                                Chart_daily.Diagram.Series[0].Points.Last().Brush = new SolidColorBrush(Color.FromRgb(219, 41, 29));
                            }
                        }
                        rdr_daily.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void openConnection()
        {
            //String strConn = "Server=localhost;Database=eeg;port=3306;username=sejong;password=offset01";
            string strConn = "Server=203.250.77.238;User ID=root;Password=root;Database=20190603;Min Pool Size=5;";
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

        private void ThemedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            realtime_th.Interrupt();
            realtime_th.Abort();
            check_th.Interrupt();
            check_th.Abort();
            gauge_th.Interrupt();
            gauge_th.Abort();
        }

        private void Track_Range_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            limit = Convert.ToInt16(Track_Range.Value);
        }

        private void Btn_DEmp_Sel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListBox_danger.SelectedItem != null)
                {
                    worker_name = ListBox_danger.SelectedItem.ToString();
                    Txt_Now_Emp.Text = worker_name;
                    select_worker();
                    daily_chart();
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.StackTrace);
            }
        }

        private void menuOption_set_Click(object sender, RoutedEventArgs e)
        {
            Window2 setlsb = new Window2();
            setlsb.ShowDialog();
        }
    }
}
