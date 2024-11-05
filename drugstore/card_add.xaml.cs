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
    /// Логика взаимодействия для card_add.xaml
    /// </summary>
    public partial class card_add : Window
    {
        public card_add()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Random random = new Random();
            App.user.card_number = string.Join("", Enumerable.Range(0, 9).Select(_ => random.Next(0, 10)));
            App.user.card_bonuses = 0;
            MessageBox.Show("карта успешно добавлена!");
            Window card = new card();
            card.Show();
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так...");
            }
        }
    }
}
