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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.AvalonDock;

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if(tb_login.Text == "" || pb_pass.Password == "")
                {
                    System.Windows.MessageBox.Show("Пожалуйста, заполните все поля для авторизации!", "Уведомление");
                    return;
                }
            var currUser = App.Context.kurs_05_02_user.FirstOrDefault(p => p.login == tb_login.Text && p.password == pb_pass.Password);
            if(currUser != null)
            {
                App.user = currUser;
                if(currUser.id_role == 2)
                {
                    System.Windows.MessageBox.Show("Добро пожаловать, " + currUser.name + " " + currUser.surname);
                    Window farm = new farm();
                    farm.Show();
                    this.Close();
                }
                else if(currUser.id_role == 1)
                    {
                        System.Windows.MessageBox.Show("Добро пожаловать, " + currUser.name + " " + currUser.surname);
                        Window admin = new admin_main();
                        admin.Show();
                        this.Close();
                    }
                else
                {
                    System.Windows.MessageBox.Show("Добро пожаловать, " + currUser.name + " " + currUser.surname);
                    Window products = new products();
                    products.Show();
                    this.Close();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Пользователь с такими данными не найден.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            Window reg = new registration();
            reg.Show();
            this.Close();
        }
    }
}
