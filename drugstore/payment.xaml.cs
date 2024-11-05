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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.AvalonDock;
using drugstore.entities;

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для payment.xaml
    /// </summary>
    public partial class payment : Window
    {
        string _code = "";
        public payment(string code)
        {
            _code = code;
            InitializeComponent();
            tbl_sum.Text += App.totalSum.ToString() + " руб.";
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window back = new products();
            back.Show();
            this.Close();
        }

        private void btn_pay_Click(object sender, RoutedEventArgs e)
        {
            if(mtb_number.Text == "" || mtb_life.Text == "" || mtb_cvc.Text == "")
            {
                System.Windows.MessageBox.Show("Пожалуйста, заполните все поля!", "Уведомление");
                return;
            }
            App.Context.kurs_05_02_user.FirstOrDefault(p => p.id_user == App.user.id_user).card_bonuses += App.bonuses;
            App.totalSum = 0;
            App.bonuses = 0;
            App.basket = new List<kurs_05_02_basket>();

            System.Windows.MessageBox.Show($"Заказ оформлен. Код заказа - " + $"{_code}. На ваш номер телефона придёт оповещение как только заказ приедет!");
            Window bask = new products();
            bask.Show();
            this.Close();
        }

        private void ch_pay_bonuses_Checked(object sender, RoutedEventArgs e)
        {
            if (App.totalSum >= Convert.ToInt32(App.user.card_bonuses))
            {
                App.totalSum -= Convert.ToInt32(App.user.card_bonuses);
                App.user.card_bonuses = 0;
            }
            else
            {
                App.totalSum = 0;
                App.user.card_bonuses -= App.totalSum;
            }
            tbl_sum.Text = "К оплате: " + App.totalSum.ToString() + " руб.";
        }
    }
}
