using drugstore.entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для order.xaml
    /// </summary>
    public partial class order : Window
    {
        public order()
        {
            InitializeComponent();
            payment();
            try
            {
            lw_basket.ItemsSource = App.basket.ToList();
            tbl_sum.Text = App.totalSum.ToString() + " " + "руб.";
            tbl_bonuses.Text += App.bonuses.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void payment()
        {

            try
            {
                var pay = App.Context.kurs_05_02_payment_method.ToList()
                    .Select(p => p.name)
                    .Distinct()
                    .ToList();


                cb_payment.ItemsSource = pay;
                cb_payment.SelectionChanged += (sender, e) =>
                {
                    if (cb_payment.SelectedItem == null)
                    {
                        cb_payment.SelectedItem = cb_payment.Items[0];
                    }
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            App.basket = new List<kurs_05_02_basket>();
            App.totalSum = 0;
            App.bonuses = 0;
            Window back = new products();
            back.Show();
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_pay_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (cb_payment.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберете способ оплаты!", "Уведомление");
                    return;
                }
                if(tb_address.Text == "")
                {
                    MessageBox.Show("Пожалуйста, укажите адрес доставки!", "Уведомление");
                    return;
                }
                var method = App.Context.kurs_05_02_payment_method.FirstOrDefault(p => p.name == cb_payment.Text).id_method;
                Random random = new Random();
                var code = string.Join("", Enumerable.Range(0, 8).Select(_ => random.Next(0, 10)));
                var newOrder = App.Context.kurs_05_02_order.FirstOrDefault(p => p.id_order == App.id_order);
                if (cb_payment.SelectedItem.ToString() == "Картой в приложении")
                {

                    newOrder.total_sum = App.totalSum;
                    if (App.Context.kurs_05_02_user.FirstOrDefault(p => p.id_user == App.user.id_user).card_number != null)
                    {
                        newOrder.bonuses_for_order = App.bonuses;
                    }
                    newOrder.address = tb_address.Text;
                    newOrder.id_payment_method = method;
                    newOrder.order_code = code;
                    newOrder.finished = false;

                    App.Context.SaveChanges();
                    App.basket = new List<kurs_05_02_basket>();
                    Window pay = new payment(code);
                    pay.Show();
                    this.Close();
                }
                else
                {
                    method = App.Context.kurs_05_02_payment_method.FirstOrDefault(p => p.name == cb_payment.Text).id_method;

                    newOrder.total_sum = App.totalSum;
                    if (App.Context.kurs_05_02_user.FirstOrDefault(p => p.id_user == App.user.id_user).card_number != null)
                    {
                        newOrder.bonuses_for_order = App.bonuses;
                    }
                    newOrder.address = tb_address.Text;
                    newOrder.id_payment_method = method;
                    newOrder.order_code = code;
                    newOrder.finished = false;

                    App.Context.SaveChanges();
                    App.totalSum = 0;
                    App.bonuses = 0;
                    App.basket = new List<kurs_05_02_basket>();
                    MessageBox.Show($"Заказ оформлен. Код заказа - " + $"{code}. На ваш номер телефона придёт оповещение как только заказ приедет!");
                    Window back = new products();
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
