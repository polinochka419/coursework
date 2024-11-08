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

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для cure_edit.xaml
    /// </summary>
    public partial class cure_edit : Window
    {
        int id = 0;
        public cure_edit(entities.kurs_05_02_cure edited_cure)
        {
            InitializeComponent();

            id = edited_cure.id_cure;
            tbl_name.Text = edited_cure.name;
            tb_price.Text = edited_cure.price.ToString();
            cb_in_stock.Text = edited_cure.in_stock;

            cb_in_stock.Items.Add("В наличии");
            cb_in_stock.Items.Add("Нет в наличии");
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Window cures = new products();
            cures.Show();
            this.Close();
        }
        private byte[] _mainImageData;
        private void btn_add_photo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);

                    if (_mainImageData.Length == 0)
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

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_price.Text == "")
                {
                    MessageBox.Show("Поле Цена не может быть пустым!", "Уведомление");
                    return;
                }
                if (MessageBox.Show("Подтвердите изменения в товаре", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (_mainImageData == null)
                    {
                        if (id != 0)
                        {
                            _mainImageData = App.Context.kurs_05_02_cure.FirstOrDefault(p => p.id_cure == id).image;
                        }
                        else _mainImageData = File.ReadAllBytes("empty.png");

                    }

                    var edited = App.Context.kurs_05_02_cure.FirstOrDefault(p => p.name == tbl_name.Text);
                    edited.price = Convert.ToInt32(tb_price.Text);
                    edited.in_stock = cb_in_stock.Text;
                    edited.image = _mainImageData;
                    App.Context.SaveChanges();
                    MessageBox.Show("Изменение успешно");

                    Window cures = new products();
                    cures.Show();
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло нет так...");
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (MessageBox.Show("Подтвердите удаление товара", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var edited = App.Context.kurs_05_02_cure.FirstOrDefault(p => p.name == tbl_name.Text);
                App.Context.kurs_05_02_cure.Remove(edited);
                App.Context.SaveChanges();
                MessageBox.Show("Удаление успешно");

                Window cures = new products();
                cures.Show();
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
