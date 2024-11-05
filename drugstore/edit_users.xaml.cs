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
    /// Логика взаимодействия для edit_users.xaml
    /// </summary>
    public partial class edit_users : Window
    {
        int id = 0;
        public edit_users(entities.kurs_05_02_user edited_user)
        {
            InitializeComponent();

            id = edited_user.id_user;

            cb_role.ItemsSource = App.Context.kusr_05_02_role
                .Select(p => p.name)
                .Distinct()
                .ToList();

            tb_name.Text = edited_user.name;
            tb_surname.Text = edited_user.surname;
            tb_fathername.Text = edited_user.fathername;
            tb_phone.Text = edited_user.phone;
            dp_birth.SelectedDate = edited_user.date_of_birth;
            tb_card_number.Text = edited_user.card_number;
            tb_bonuses.Text = edited_user.card_bonuses.ToString();
            cb_role.Text = App.Context.kusr_05_02_role.FirstOrDefault(p => p.id_role ==  edited_user.id_role).name;
            tb_login.Text = edited_user.login;
            tb_password.Text = edited_user.password;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window back = new admin_main();
            back.Show();
            this.Close();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(tb_surname.Text == "" || tb_name.Text == "" || tb_fathername.Text == "" || tb_phone.Text == "" 
                    || tb_login.Text == "" || tb_password.Text == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Уведомление");
                    return;
                }
                if (MessageBox.Show("Подтвердите изменение данных пользователя.", "Внимание",
                   MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var edited = App.Context.kurs_05_02_user.FirstOrDefault(p => p.id_user == id);
                    edited.surname = tb_surname.Text;
                    edited.name = tb_name.Text;
                    edited.fathername = tb_fathername.Text;
                    edited.phone = tb_phone.Text;
                    edited.date_of_birth = dp_birth.SelectedDate.Value;
                    edited.id_role = App.Context.kusr_05_02_role.FirstOrDefault(p => p.name == cb_role.Text).id_role;
                    edited.login = tb_login.Text;
                    edited.password = tb_password.Text;
                    App.Context.SaveChanges();
                    MessageBox.Show("Данные пользователя обновлены.", "Уведомление");
                    Window back = new users_main();
                    back.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (MessageBox.Show("Подтвердите удаление пользователя.", "Внимание",
               MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                    var deleted = App.Context.kurs_05_02_user.FirstOrDefault(p => p.id_user == id);
                    App.Context.kurs_05_02_user.Remove(deleted);
                    App.Context.SaveChanges();
                    MessageBox.Show("Пользователь удалён.", "Уведомление");
                    Window back = new users_main();
                    back.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }
    }
}
