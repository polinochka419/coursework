using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для admin_main.xaml
    /// </summary>
    public partial class admin_main : Window
    {
        public admin_main()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window back = new MainWindow();
            back.Show();
            this.Close();
        }

        private void btn_to_clients_Click(object sender, RoutedEventArgs e)
        {
            Window clients = new users_main();
            clients.Show();
            this.Close();
        }

        private void btn_to_orders_Click(object sender, RoutedEventArgs e)
        {
            Window orders = new orders_main();
            orders.Show();
            this.Close();
        }

        private void btn_to_cures_Click(object sender, RoutedEventArgs e)
        {
            Window cures = new products();
            cures.Show();
            this.Close();
        }
    }
}
