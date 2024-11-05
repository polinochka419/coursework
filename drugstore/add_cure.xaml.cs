using drugstore.entities;
using Microsoft.Win32;
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
using System.IO;
using System.Text.RegularExpressions;

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для add_cure.xaml
    /// </summary>
    public partial class add_cure : Window
    {
        public add_cure()
        {
            InitializeComponent();
            manufacturers();
            categories();
            releaseForm();
        }

        private void manufacturers()
        {

            try
            {
                var manu = App.Context.kurs_05_02_manufacturer.ToList()
                    .Select(p => p.name)
                    .Distinct()
                    .ToList();

                cb_manufacturers.ItemsSource = manu;
                cb_manufacturers.SelectionChanged += (sender, e) =>
                {
                    if (cb_manufacturers.SelectedItem == null)
                    {
                        cb_manufacturers.SelectedItem = cb_manufacturers.Items[0];
                    }
                };

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

                cb_category.ItemsSource = cat;
                cb_category.SelectionChanged += (sender, e) =>
                {
                    if (cb_category.SelectedItem == null)
                    {
                        cb_category.SelectedItem = cb_category.Items[0];
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void releaseForm()
        {
            try
            {

                var form = App.Context.kurs_05_02_release_form.ToList()
                    .Select(p => p.name)
                    .Distinct()
                    .ToList();

                cb_form.ItemsSource = form;
                cb_form.SelectionChanged += (sender, e) =>
                {
                    if (cb_form.SelectedItem == null)
                    {
                        cb_form.SelectedItem = cb_form.Items[0];
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
            Window back = new products();
            back.Show();
            this.Close();
        }
        private byte[] _mainImageData;
        private void btn_add_photo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png; *.jpg; *.jpeg";

            if(ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                if(_mainImageData.Length == 0)
                    {
                        _mainImageData = File.ReadAllBytes("empty.png");

                    }
                    img_image.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }

        private void btn_add_cure_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if(tb_name.Text == "" || tb_price.Text == "" || cb_category.Text == "" || cb_form.Text == "" || cb_manufacturers.Text == ""
                    || dp_life.SelectedDate.Value == null || tb_volume.Text == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Уведомление");
                    return;
                }
            var isPresc = false;
            if(ch_presc.IsChecked == true)
            {
                isPresc = true;
            }
            if (!Regex.IsMatch(tb_price.Text, @"^[0-9\s]+$"))
            {
                MessageBox.Show("Цена должна содержать только цифры", "Ошибка");
                return;
            };

            var idCat = App.Context.kurs_05_02_cure_category.FirstOrDefault(p => p.name == cb_category.Text).id_category;
            var idMan = App.Context.kurs_05_02_manufacturer.FirstOrDefault(p => p.name == cb_manufacturers.Text).id_manufacturer;
            var idForm = App.Context.kurs_05_02_release_form.FirstOrDefault(p => p.name == cb_form.Text).id_form;

                if (_mainImageData == null)
                {
                    _mainImageData = File.ReadAllBytes("empty.png");

                }

                var newCure = new kurs_05_02_cure
            {
                name = tb_name.Text,
                price = Convert.ToInt32(tb_price.Text),
                id_category = idCat,
                id_release_form = idForm,
                id_manufacturer = idMan,
                shelf_life = dp_life.SelectedDate.Value,
                volume = tb_volume.Text,
                in_stock = "В наличии",
                prescription = isPresc,
                image = _mainImageData
            };
            App.Context.kurs_05_02_cure.Add(newCure);
            App.Context.SaveChanges();
            MessageBox.Show("Лекарство добавлено.");
            Window back = new products();
            back.Show();
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }
    }
}
