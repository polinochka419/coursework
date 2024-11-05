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
using Xceed.Wpf.Toolkit.Primitives;

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для products.xaml
    /// </summary>
    public partial class products : Window
    {
        public products()
        {
            InitializeComponent();
            manufacturers();
            categories();
            release_forms();

            lw_cures.ItemsSource = App.Context.kurs_05_02_cure.ToList();

            if(App.user.id_role == 1)
            {
                var app = (App)Application.Current;
                app.ButtonText = "Редактировать";
                btn_buy.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_add.Visibility = Visibility.Hidden;
                var app = (App)Application.Current;
                app.ButtonText = "В корзину";
            }

        }
        string order = "asc";

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            App.basket = null;
            Window auth = new MainWindow();
            auth.Show();
            this.Close();
        }

        private void FilterCures()
        {
            try
            {
            var cures = App.Context.kurs_05_02_cure.ToList().Where(p => p.name.ToLower().Contains(tb_search.Text.ToLower()));

            if(cb_manufacturers.SelectedItem != null)
            {
                if(cb_manufacturers.SelectedItem.ToString() == "Все производители" || cb_manufacturers.SelectedItem == null)
                {
                    lw_cures.ItemsSource = cures;
                }
                    else
                    {
                    var manId = App.Context.kurs_05_02_manufacturer.Where(p => p.name == cb_manufacturers.SelectedItem.ToString()).FirstOrDefault().id_manufacturer;
                    cures = cures.Where(p => p.id_manufacturer == manId).ToList();
                }
            }

            if (cb_category.SelectedItem != null)
            {
                    if (cb_category.SelectedItem.ToString() == "Все категории" || cb_category.SelectedItem == null)
                {
                    lw_cures.ItemsSource = cures;
                }
                else
                {
                    var catId = App.Context.kurs_05_02_cure_category.Where(p => p.name == cb_category.SelectedItem.ToString()).FirstOrDefault().id_category;
                    cures = cures.Where(p => p.id_category == catId).ToList();
                }
            }

                if (cb_release_form.SelectedItem != null)
                {
                    if (cb_release_form.SelectedItem.ToString() == "Все формы выпуска" || cb_release_form.SelectedItem == null)
                    {
                        lw_cures.ItemsSource = cures;
                    }
                    else
                    {
                        var formId = App.Context.kurs_05_02_release_form.Where(p => p.name == cb_release_form.SelectedItem.ToString()).FirstOrDefault().id_form;
                        cures = cures.Where(p => p.id_release_form == formId).ToList();
                    }
                }

                if (order == "asc")
            {
                cures = cures.OrderBy(p => p.price).ToList();
            }
            else
            {
                cures = cures.OrderByDescending(p => p.price).ToList();
            }

            lw_cures.ItemsSource = cures;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void manufacturers()
        {

            try
            {
                var manu = App.Context.kurs_05_02_manufacturer.ToList()
                    .Select(p => p.name)
                    .Distinct()
                    .ToList();

                manu.Insert(0, "Все производители");

                cb_manufacturers.ItemsSource = manu;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void release_forms()
        {

            try
            {
                var forms = App.Context.kurs_05_02_release_form.ToList()
                    .Select(p => p.name)
                    .Distinct()
                    .ToList();

                forms.Insert(0, "Все формы выпуска");

                cb_release_form.ItemsSource = forms;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void categories()
        {
            try
            {

            var cat = App.Context.kurs_05_02_cure_category.ToList()
                .Select(p => p.name)
                .Distinct()
                .ToList();

            cat.Insert(0, "Все категории");

            cb_category.ItemsSource = cat;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_asc_Click(object sender, RoutedEventArgs e)
        {
            order = "asc";
            FilterCures();
        }

        private void btn_desc_Click(object sender, RoutedEventArgs e)
        {
            order = "desc";
            FilterCures();
        }

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterCures();
        }

        private void cb_manufacturers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterCures();
        }

        private void cb_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterCures();
        }

        private void btn_buy_Click(object sender, RoutedEventArgs e)
        {
            Window bye = new order();
            bye.Show();
            this.Close();
        }

        private void btn_card_Click(object sender, RoutedEventArgs e)
        {
            if(App.user.card_number == null)
            {
                Window card = new card_add();
                card.Show();
            }
            else
            {
                Window card = new card();
                card.Show();
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Window add = new add_cure();
            add.Show();
            this.Close();
        }

        private void btn_basket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var edited_cure = (sender as Button).DataContext as kurs_05_02_cure;

                if (App.user.id_role == 1)
                {
                    Window edit = new cure_edit(edited_cure);
                    edit.Show();
                    this.Close();
                }
                else
                {
                    if (edited_cure.prescription == true)
                    {
                        if (MessageBox.Show("Данный препарат можно приобрести только по рецепту. У вас есть рецепт?", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                        {
                            return;
                        }
                    }

                    if (edited_cure.in_stock == "Нет в наличии")
                    {
                        MessageBox.Show("К сожалению, товар закончился... Через пару дней обязательно будет в наличии!", "Уведомление");
                        return;
                    }

                    var new_order = new kurs_05_02_order
                    {
                        finished = false,
                    };
                    App.Context.kurs_05_02_order.Add(new_order);

                    App.id_order = App.Context.kurs_05_02_order.OrderByDescending(p => p.id_order).FirstOrDefault().id_order;

                    tbl_in_basket.Text = Convert.ToString(Convert.ToInt32(tbl_in_basket.Text) + 1);

                if(App.basket ==  null)
                {
                    return;
                }
                    if (App.basket.Any(p => p.id_cure == edited_cure.id_cure) && App.Context.kurs_05_02_basket.Any(p => p.id_cure == edited_cure.id_cure))
                    {
                        App.Context.kurs_05_02_basket.FirstOrDefault(p => p.id_cure == edited_cure.id_cure).cure_quantity += 1;
                        App.basket.FirstOrDefault(p => p.id_cure == edited_cure.id_cure).cure_quantity += 1;
                        App.totalSum += Convert.ToInt32(edited_cure.price);
                        return;
                    }
                    var basket = new kurs_05_02_basket
                    {
                        id_cure = edited_cure.id_cure,
                        id_user = App.user.id_user,
                        cure_quantity = 1,
                        id_order = App.id_order
                    };

                    App.basket.Add(basket);
                    App.totalSum += Convert.ToInt32(edited_cure.price);
                    App.bonuses += Convert.ToInt32(Math.Round((double)edited_cure.price * 0.05, 0));
                    App.Context.kurs_05_02_basket.Add(basket);
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }

}
    }
}
