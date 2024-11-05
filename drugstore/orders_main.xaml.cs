using drugstore.entities;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для orders_main.xaml
    /// </summary>
    public partial class orders_main : Window
    {
        public orders_main()
        {
            InitializeComponent();

            lw_orders.ItemsSource = App.Context.kurs_05_02_order.ToList();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window back = new admin_main();
            back.Show();
            this.Close();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var edited_order = (sender as Button).DataContext as kurs_05_02_order;
            Window edit = new edit_order(edited_order);
            edit.Show();
            this.Close();
        }
    }
}
