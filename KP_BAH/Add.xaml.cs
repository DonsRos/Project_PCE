using Npgsql;
using System;
using KP_BAH;
using System.Windows;

namespace KP_BAH
{
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=123;Include Error Detail=true;";
        readonly string sql_client = "INSERT INTO public.\"cpus\" " +
            "(name, number, description) " +
            "VALUES (@value1, @value2, @value3);";
        private void Button_Add(object sender, RoutedEventArgs e)
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

                        NpgsqlCommand cmd = new NpgsqlCommand(sql_client, con);
                        cmd.Parameters.AddWithValue("value1", name.Text);
                        cmd.Parameters.AddWithValue("value2", site.Text);
                        cmd.Parameters.AddWithValue("value3", description.Text);
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    DATABASE ClientsList = new DATABASE();
                    ClientsList.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Заполните данные.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                MessageBox.Show(message);
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            DATABASE ClientsList = new DATABASE();
            ClientsList.Show();
            Close();
        }
    }
}