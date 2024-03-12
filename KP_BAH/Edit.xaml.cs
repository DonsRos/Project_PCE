using Npgsql;
using System.Text.RegularExpressions;
using System;
using System.Windows;
using System.Windows.Input;
using System.Security.Policy;
using System.Xml.Linq;

namespace KP_BAH
{
    public partial class Edit : Window
    {
        public Edit()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=123;";
        readonly string sql = "UPDATE public.\"cpus\" " +
            "SET name = @value1, " +
            "number = @value2, " +
            "description = @value3 " +
            "WHERE id = @id;";
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(name.Text)
                || string.IsNullOrEmpty(site.Text)
                || string.IsNullOrEmpty(description.Text)))
                {
                    using (NpgsqlConnection con = new NpgsqlConnection(conString))
                    {
                        con.Open();

                        NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("value1", name.Text);
                        cmd.Parameters.AddWithValue("value2", site.Text);
                        cmd.Parameters.AddWithValue("value3", description.Text);
                        cmd.Parameters.AddWithValue("id", Int32.Parse(number.Text));
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    DATABASE SupplierList = new DATABASE();
                    SupplierList.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Заполните данные.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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