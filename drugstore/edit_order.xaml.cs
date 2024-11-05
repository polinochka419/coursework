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

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для edit_order.xaml
    /// </summary>
    public partial class edit_order : Window
    {
        int id = 0;
        public edit_order(entities.kurs_05_02_order edited_order)
        {
            InitializeComponent();

            id = edited_order.id_order;

            cb_payment_method.ItemsSource = App.Context.kurs_05_02_payment_method
                .Select(p => p.name)
                .Distinct()
                .ToList();

            tb_total_sum.Text = edited_order.total_sum.ToString();
            tb_bonuses_for_order.Text = edited_order.bonuses_for_order.ToString();
            tb_address.Text = edited_order.address.ToString();
            cb_payment_method.Text = App.Context.kurs_05_02_payment_method.FirstOrDefault(p => p.id_method == edited_order.id_payment_method).name;
            tb_code.Text = edited_order.order_code.ToString();
            if(edited_order.finished == true)
            {
                ch_finished.IsChecked = true;
            }
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
                if(tb_address.Text == "" || tb_total_sum.Text == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Уведомление");
                    return;
                }

            if (MessageBox.Show("Подтвердите изменения в заказе", "Внимание",
               MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {           
                if (!Regex.IsMatch(tb_total_sum.Text, @"^[0-9\s]+$"))
            {
                MessageBox.Show("В поле Цена должны быть только цифры!", "Ошибка");
                return;
            }
            if (!Regex.IsMatch(tb_bonuses_for_order.Text, @"^[0-9\s]+$"))
            {
                MessageBox.Show("В поле Бонусы должны быть только цифры!", "Ошибка");
                return;
            }

            var edited = App.Context.kurs_05_02_order.FirstOrDefault(p => p.id_order == id);

            edited.total_sum = Convert.ToInt32(tb_total_sum.Text);
                    if (tb_bonuses_for_order.Text != "")
                    {
                        edited.bonuses_for_order = Convert.ToInt32(tb_bonuses_for_order.Text);
                    }
                    else edited.bonuses_for_order = 0;
            edited.address = tb_address.Text;
            edited.id_payment_method = App.Context.kurs_05_02_payment_method.FirstOrDefault(p => p.name == cb_payment_method.Text).id_method;
            if (ch_finished.IsChecked == false)
            {
                edited.finished = false;
            }
            else edited.finished = true;

            App.Context.SaveChanges();
            MessageBox.Show("Изменения сохранены.", "Уведомление");

                Window back = new orders_main();
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
                if (MessageBox.Show("Подтвердите удаление заказа", "Внимание",
               MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var deleted = App.Context.kurs_05_02_order.FirstOrDefault(p => p.id_order == id);
                    App.Context.kurs_05_02_order.Remove(deleted);
                    App.Context.SaveChanges();
                    MessageBox.Show("Заказ удалён.", "Уведомление");

                    Window back = new orders_main();
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
