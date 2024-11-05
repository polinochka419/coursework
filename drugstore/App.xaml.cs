using drugstore.entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.AvalonDock;
using System.ComponentModel;

namespace drugstore
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        private string buttonText = "В корзину";
        public string ButtonText
        {
            get => buttonText;
            set
            {
                if (buttonText != value)
                {
                    buttonText = value;
                    OnPropertyChanged(nameof(ButtonText));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static user95_dbEntities11 Context { get; set; } = new user95_dbEntities11();

        public static entities.kurs_05_02_user user = null;

        public static entities.kurs_05_02_cure cure = null;

        public static List<kurs_05_02_basket> basket = new List<kurs_05_02_basket>();

        public static int id_order = 0;

        public static int totalSum = 0;

        public static int bonuses = 0;

    }
}
