using Npgsql;
using System;
using System.Management;
using System.Windows;

namespace KP_BAH
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var con = new NpgsqlConnection(connectionString: "Host=127.0.0.1;Port=5432;Database=cabs;Username=postgres;Password=123");
            con.Open();
        }

        private void Button_Components(object sender, RoutedEventArgs e)
        {
            string processor = String.Empty;
            string videoCard = String.Empty;
            string motherboard = String.Empty;
            string powerSupply = String.Empty;
            string ram = String.Empty;
            string net = String.Empty;
            string os = String.Empty;
            string comp_name = String.Empty;
            string comp_manuf = String.Empty;
            string bs = String.Empty;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                processor = obj["Name"].ToString();
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject obj in searcher.Get())
            {
                videoCard = obj["Name"].ToString();
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                motherboard = obj["Product"].ToString();
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                ram = (obj["Capacity"]).ToString();
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj in searcher.Get())
            {
                net = obj["Description"].ToString();
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                os = obj["Caption"].ToString();
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                comp_manuf = obj["Manufacturer"].ToString();
                comp_name = obj["Name"].ToString();
                bs = obj["TotalPhysicalMemory"].ToString();
            }

            MessageBox.Show($"Процессор: {processor}\nВидеокарта: {videoCard}\nМатеринская плата: {motherboard}\nОперативная память: " +
                $"{ram}\nИнтернет: {net}\nОперационная система: {os}\nПроизводитель: {comp_manuf}" +
                $"\nИмя: {comp_name}\nОбщий объем физической памяти: {bs}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This software is created for the purpose of a course project and does not carry any rights, enjoy!\nЭто программное обеспечение создано в целях выполнения курсового проекта, и не несет в себе каких-то прав, наслаждайтесь!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DATABASE window = new DATABASE();
            window.Show();
            
        }
    }
}
