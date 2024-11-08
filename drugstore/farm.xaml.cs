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
    /// Логика взаимодействия для farm.xaml
    /// </summary>
    public partial class farm : Window
    {
        user95_dbEntities11 data = new user95_dbEntities11();

        public farm()
        {
            InitializeComponent();
            tbl_payed.Visibility = Visibility.Hidden;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window auth = new MainWindow();
            auth.Show();
            this.Close();
        }

        private void btn_to_cures_Click(object sender, RoutedEventArgs e)
        {
            App.basket = new List<kurs_05_02_basket>();
            Window products = new products();
            products.Show();
            this.Close();
        }
        int id = 0;
        private void btn_find_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(tb_order.Text == null)
                {
                    MessageBox.Show("Пожалуйста, введите номер заказа!");
                    return;
                }
                else
                {
                    if(!App.Context.kurs_05_02_order.Any(p => p.order_code == tb_order.Text))
                    {
                        MessageBox.Show("Ничего не найдено...", "Усп");
                        return;
                    }
                    var orderId = App.Context.kurs_05_02_order.FirstOrDefault(p => p.order_code == tb_order.Text);
                    id = orderId.id_order;
                    var basket = App.Context.kurs_05_02_basket.Where(p => p.id_order == orderId.id_order).ToList();
                    lw_basket.ItemsSource = basket;
                    tbl_sum.Text += Convert.ToInt32(orderId.total_sum) + "руб.";
                    tbl_payed.Visibility = Visibility.Visible;
                    App.bonuses = Convert.ToInt32(orderId.bonuses_for_order);

                    if(orderId.id_payment_method == 2)
                    {
                        tbl_payed.Text = "Не оплачено";
                    }
                    else
                    {
                        tbl_payed.Text = "Оплачено";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            var clear = App.Context.kurs_05_02_basket.Where(p => p.id_order == App.id_order);
            App.Context.kurs_05_02_basket.RemoveRange(clear);
            App.Context.SaveChanges();
            tbl_payed.Visibility = Visibility.Hidden;
            tbl_sum.Text = "К оплате: ";
            lw_basket.ItemsSource = null;
            tb_order.Text = "";
            App.user.card_bonuses += App.bonuses;
            App.Context.kurs_05_02_order.FirstOrDefault(p => p.id_order == id).finished = true;
            App.totalSum = 0;
            App.bonuses = 0;
            App.basket = null;

                MessageBox.Show("Заказ завершен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }
    }
}
