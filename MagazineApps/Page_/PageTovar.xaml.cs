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
using MagazineApps.Classes;

namespace MagazineApps.Page_
{
    /// <summary>
    /// Логика взаимодействия для PageTovar.xaml
    /// </summary>
    public partial class PageTovar : Page
    {
        public PageTovar()
        {
            InitializeComponent();
            DGridTovar.ItemsSource = DB.c.con.Товар.ToList();
        }

        private void Btn_korz_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageKorzina());
        }

        private void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = DB.c.con.Товар.ToList();
            search = search.Where(i => i.Наименование.ToLower().Contains(Tb_search.Text)).ToList();
            DGridTovar.ItemsSource = search.OrderBy(p => p.Наименование).ToList();
        }

        private void AddKorz_Click_1(object sender, RoutedEventArgs e)
        {
            var tovar = DGridTovar.SelectedItem as DB.Товар;
            var korzina = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            var tovar_korz = new DB.Товар_корзина();
            tovar_korz.ID_Товар = tovar.ID;
            tovar_korz.ID_Корзина = korzina.ID;
            tovar_korz.Колличество = 1;
            tovar_korz.Цена = tovar.Цена;
            var prov = DB.c.con.Товар_корзина.Any(i => i.ID_Товар == tovar.ID && i.ID_Корзина == korzina.ID);
            if (prov)
            {
                MessageBox.Show("Данный товар уже в корзине");
            }
            else
            {
                korzina.Общая_сумма += tovar.Цена;
                DB.c.con.Товар_корзина.Add(tovar_korz);
                DB.c.con.SaveChanges();
            }
        }

        private void Dell_Click(object sender, RoutedEventArgs e)
        {
            var tovar = DGridTovar.SelectedItem as DB.Товар;
            DB.c.con.Товар.Remove(tovar);
            DB.c.con.SaveChanges();
            MessageBox.Show("Товар удалён.");
            DGridTovar.ItemsSource = DB.c.con.Товар.ToList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedTovar = (sender as Button).DataContext as DB.Товар;
            NavigationService.Navigate(new PageAdd() { Tag = selectedTovar });
        }
    }
}
