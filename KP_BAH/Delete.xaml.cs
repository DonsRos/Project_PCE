using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KP_BAH
{
    /// <summary>
    /// Логика взаимодействия для Delete.xaml
    /// </summary>
    public partial class Delete : Window
    {
        public Delete()
        {
            InitializeComponent();
        }
        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=123;Include Error Detail=true;";
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conString))
                {
                    con.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM public.\"cpus\" WHERE id = @id;";
                    cmd.Parameters.AddWithValue("id", Int32.Parse(number.Text));
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                MessageBox.Show("Строка удалена успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления строки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Limits_id(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            DATABASE SupplierList = new DATABASE();
            SupplierList.Show();
            Close();
        }
    }
}
