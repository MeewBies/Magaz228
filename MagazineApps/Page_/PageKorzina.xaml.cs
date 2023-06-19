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
    /// Логика взаимодействия для PageKorzina.xaml
    /// </summary>
    public partial class PageKorzina : Page
    {
        public PageKorzina()
        {
            InitializeComponent();
            var korz = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            DGridKorzina.ItemsSource = DB.c.con.Товар_корзина.Where(i => i.ID_Корзина == korz.ID).ToList();
            var st = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID).Общая_сумма.ToString();
            moneycash.Text = $"Общая стоимость: {st} руб.";
        }

        private void TB_exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageTovar());
            DGridKorzina.ItemsSource = DB.c.con.Товар_корзина.ToList();
        }

        private void AddTovar_Click(object sender, RoutedEventArgs e)
        {
            var tovar = DGridKorzina.SelectedItem as DB.Товар_корзина;
            var mon = DB.c.con.Товар.FirstOrDefault(i => i.ID == tovar.ID_Товар);
            int money = mon.Цена;
            tovar.Цена += money;
            var trash = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            trash.Общая_сумма += money;
            tovar.Колличество += 1;
            DB.c.con.SaveChanges();
            var st = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID).Общая_сумма.ToString();
            moneycash.Text = $"Общая стоимость: {st} руб.";

            var korz = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            DGridKorzina.ItemsSource = DB.c.con.Товар_корзина.Where(i => i.ID_Корзина == korz.ID).ToList();
        }

        private void RemTovar_Click(object sender, RoutedEventArgs e)
        {
            var tovar = DGridKorzina.SelectedItem as DB.Товар_корзина;
            var mon = DB.c.con.Товар.FirstOrDefault(i => i.ID == tovar.ID_Товар);
            int money = mon.Цена;
            var trash = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);

            if (tovar.Колличество != 1)
            {
                tovar.Колличество -= 1;
                trash.Общая_сумма -= money;
                tovar.Цена -= money;
                DB.c.con.SaveChanges();
                var st = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID).Общая_сумма.ToString();
                moneycash.Text = $"Общая стоимость: {st} руб.";

                var korz = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
                DGridKorzina.ItemsSource = DB.c.con.Товар_корзина.Where(i => i.ID_Корзина == korz.ID).ToList();
            }
        }

        private void RemoveTovar_Click(object sender, RoutedEventArgs e)
        {
            var tovar = DGridKorzina.SelectedItem as DB.Товар_корзина;
            var trash = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            trash.Общая_сумма -= tovar.Цена;
            DB.c.con.Товар_корзина.Remove(tovar);
            DB.c.con.SaveChanges();
            var st = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID).Общая_сумма.ToString();
            moneycash.Text = $"Общая стоимость: {st} руб.";

            var korz = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            DGridKorzina.ItemsSource = DB.c.con.Товар_корзина.Where(i => i.ID_Корзина == korz.ID).ToList();
        }

        private void TB_zakazat_Click(object sender, RoutedEventArgs e)
        {
            var user = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            var zakaz = new DB.Заказ();
            zakaz.ID_Пользователь = user.ID_Пользователь;
            zakaz.Сумма = Convert.ToInt32(user.Общая_сумма);
            DB.c.con.Заказ.Add(zakaz);
            DB.c.con.SaveChanges(); // сохраняем изменения в БД, чтобы получить ID созданного заказа

            var usZakaz = DB.c.con.Заказ.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            foreach (var tovkorz in user.Товар_корзина)
            {
                var tovzakaz = new DB.Товар_заказа();
                tovzakaz.ID_Товар = tovkorz.ID_Товар;
                tovzakaz.ID_Заказ = usZakaz.ID; // используем ID заказа, полученный из БД
                tovzakaz.Колличество = tovkorz.Колличество;
                tovzakaz.Цена = tovkorz.Цена;
                DB.c.con.Товар_заказа.Add(tovzakaz);
            }

            DB.c.con.SaveChanges();

            var tov_korz = DB.c.con.Товар_корзина.Where(i => i.ID_Корзина == user.ID);
            DB.c.con.Товар_корзина.RemoveRange(tov_korz);
            var korz = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == ClassID.UserID);
            korz.Общая_сумма = 0;
            DB.c.con.SaveChanges();

            MessageBox.Show("Заказ успешно сформирован");
            DGridKorzina.ItemsSource = DB.c.con.Товар_корзина.Where(i => i.ID_Корзина == korz.ID).ToList();
        }
    }
}
