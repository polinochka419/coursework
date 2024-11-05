﻿using System;
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
    /// Логика взаимодействия для card.xaml
    /// </summary>
    public partial class card : Window
    {
        public card()
        {
            InitializeComponent();

            tbl_number.Text = App.user.card_number.ToString();
            tbl_bonuses.Text = App.user.card_bonuses.ToString();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
