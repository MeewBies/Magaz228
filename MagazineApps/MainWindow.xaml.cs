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

namespace MagazineApps
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Page_.PageTovar());
        }

        private void Btn_vhod_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_vhod.Content != "Выйти")
            {
                Win_.AutWin win = new Win_.AutWin();
                win.ShowDialog();
                if (Classes.ClassID.UserID != 8)
                {
                    var user = DB.c.con.Пользователь.FirstOrDefault(i => i.ID == Classes.ClassID.UserID);
                    Btn_vhod.Content = "Выйти";
                    TB_name.Text = $"Здравствуйте, {user.ФИО}!";
                    MainFrame.Navigate(new Page_.PageTovar());
                }
            }
            else
            {

                Btn_vhod.Content = "Войти";
                TB_name.Text = $"Здравствуйте, Гость!";
                
                Classes.ClassID.UserID = 8;
                var tov_korz = DB.c.con.Товар_корзина.Where(i => i.ID_Корзина == 4);
                var korz = DB.c.con.Корзина.FirstOrDefault(i => i.ID_Пользователь == 8);
                DB.c.con.Товар_корзина.RemoveRange(tov_korz);
                korz.Общая_сумма = 0;
                DB.c.con.SaveChanges();
                MainFrame.Navigate(new Page_.PageTovar());
            }
        }

        private void Btn_zakaz_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page_.PageZakaz());
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page_.PageAdd());
        }
    }
}
