using Npgsql;
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
using KP_BAH;

namespace KP_BAH
{
    /// <summary>
    /// Логика взаимодействия для DATABASE.xaml
    /// </summary>
    public partial class DATABASE : Window
    {
        private readonly DataSet ds = new DataSet();
        private readonly DataTable dt = new DataTable();

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=cabs;Username=postgres;Password=123;Include Error Detail=true;";
        readonly string sql = ("SELECT * FROM public.\"cpus\";");
        public DATABASE()
        {
            InitializeComponent();
            var con = new NpgsqlConnection(conString);
            con.Open();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            adapter.Fill(ds);
            dt = ds.Tables[0];
            Supplier_DG.ItemsSource = dt.DefaultView;

            con.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Add AddSupplier = new Add();
            AddSupplier.Show();
            Close();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            Delete DeleteSupplier = new Delete();
            DeleteSupplier.Show();
            Close();
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            Edit EditSupplier = new Edit();
            EditSupplier.Show();
            Close();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            MainWindow WarehouseList = new MainWindow();
            WarehouseList.Show();
            Close();
        }
    }
}
