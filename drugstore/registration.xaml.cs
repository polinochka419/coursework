using drugstore.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
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

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        public registration()
        {
            InitializeComponent();
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            Window auth = new MainWindow();
            auth.Show();
            this.Close();
        }

        //проверка валидности пароля
        private bool IsValidText(string text)
        {
            return text.Length >= 10 &&
                   Regex.IsMatch(text, @"[0-9]") &&
                   Regex.IsMatch(text, @"[\W_]");
        }

        private void btn_auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(tb_name.Text == "" || tb_surname.Text == "" || tb_fathername.Text == "" || tb_phone.Text == ""
                    || tb_login.Text == "" || pb_password.Password == "")
                {
                    System.Windows.MessageBox.Show("Пожалуйста, заполните все поля!", "Уведомление");
                    return;
                }
                if(!IsValidText(pb_password.Password))
                {
                    MessageBox.Show("Пароль должен содержать минимум 10 символов, хотя бы один специальный символ и хотя бы одну цифру.", "Внимание");
                    return;
                }
                if(App.Context.kurs_05_02_user.Any(p => p.login == tb_login.Text))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует. Пожалуйста, придумайте другой.", "Уведомление");
                    return;
                }
                if (!Regex.IsMatch(tb_surname.Text, @"^[A-Za-zА-Яа-яЁё\s]+$"))
                {
                    MessageBox.Show("Фамилия должна содержать только буквы", "Ошибка");
                }

                else if (!Regex.IsMatch(tb_name.Text, @"^[A-Za-zА-Яа-яЁё\s]+$"))
                {
                    MessageBox.Show("Имя должно содержать только буквы", "Ошибка");
                }

                else if (!Regex.IsMatch(tb_fathername.Text, @"^[A-Za-zА-Яа-яЁё\s]*$"))
                {
                    MessageBox.Show("Отчество должно содержать только буквы", "Ошибка");
                }
                else
                {
                var newUser = new kurs_05_02_user
                {
                    name = tb_name.Text,
                    surname = tb_surname.Text,
                    fathername = tb_fathername.Text,
                    date_of_birth = dp_birth.SelectedDate.Value,
                    phone = tb_phone.Text,
                    id_role = 3,
                    login = tb_login.Text,
                    password = pb_password.Password
                };
                App.Context.kurs_05_02_user.Add(newUser);
                App.Context.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!");

                    Window auth = new MainWindow();
                    auth.Show();
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window auth = new MainWindow();
            auth.Show();
            this.Close();
        }
    }
}
