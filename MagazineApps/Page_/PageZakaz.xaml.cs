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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagazineApps.Page_
{
    /// <summary>
    /// Логика взаимодействия для PageZakaz.xaml
    /// </summary>
    public partial class PageZakaz : Page
    {
        public PageZakaz()
        {
            InitializeComponent();
            this.DataContext = DB.c.con;
            DGridZakaz.ItemsSource = DB.c.con.Заказ.ToList();
        }

        private void TB_exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageTovar());
        }

        private void DGridZakaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Находим элемент DGridZakTov по имени
            var dGridZakTov = FindName("DGridZakTov") as DataGrid;
            if (dGridZakTov != null)
            {
                // Устанавливаем источник данных для DGridZakTov
                var selectedOrder = DGridZakaz.SelectedItem as DB.Заказ;
                if (selectedOrder != null)
                {
                    dGridZakTov.ItemsSource = selectedOrder.Товар_заказа.ToList();
                }
            }
        }
    }
}
