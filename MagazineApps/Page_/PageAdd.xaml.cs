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
using MagazineApps.DB;
using MagazineApps.Classes;
namespace MagazineApps.Page_
{
    /// <summary>
    /// Логика взаимодействия для PageAdd.xaml
    /// </summary>
    public partial class PageAdd : Page
    {
        public PageAdd()
        {
            InitializeComponent();
        }


        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {

            var tovar = new Товар
            {
                Наименование = TB_name.Text,
                Цена = Convert.ToInt32(TB_price.Text),
                Колличество = Convert.ToInt32(TB_kolvo.Text),
                Производитель = TB_proiz.Text,
                Изображение = @"\Images\3.jpg",
                Тип = TB_tip.Text
            };

            c.con.Товар.Add(tovar); // Добавляем товар в таблицу в контексте
            c.con.SaveChanges(); // Сохраняем изменения в базе данных
            MessageBox.Show("Данные успешно добавлены");

        }

        private void Btn_exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_.PageTovar());
        }
    }
}
