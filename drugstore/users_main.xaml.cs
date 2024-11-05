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
    /// Логика взаимодействия для users_main.xaml
    /// </summary>
    public partial class users_main : Window
    {
        public users_main()
        {
            InitializeComponent();

            lw_clients.ItemsSource = App.Context.kurs_05_02_user.ToList();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window back = new admin_main();
            back.Show();
            this.Close();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var edited_user = (sender as Button).DataContext as kurs_05_02_user;
            Window edit = new edit_users(edited_user);
            edit.Show();
            this.Close();
        }
    }
}
