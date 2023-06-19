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

namespace MagazineApps.Win_
{
    /// <summary>
    /// Логика взаимодействия для AutWin.xaml
    /// </summary>
    public partial class AutWin : Window
    {
        public AutWin()
        {
            InitializeComponent();
        }

        private void Btn_vhod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = DB.c.con.Пользователь.FirstOrDefault(i => i.Логин == TB_log.Text && i.Пароль == TB_pas.Password);
                if (user != null)
                {
                    Classes.ClassID.UserID = user.ID;
                    MessageBox.Show("Добро пожаловать");
                    Close();
                }
                else MessageBox.Show("Ошибка.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex}");
            }
        }
    }
}
