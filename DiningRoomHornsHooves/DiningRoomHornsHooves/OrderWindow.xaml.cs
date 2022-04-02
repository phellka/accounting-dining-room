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
using DiningRoomBusinessLogic.BusinessLogics;
using DiningRoomDatabaseImplement.Implements;
using DiningRoomContracts.BindingModels;


namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        OrderLogic orderLogic = new OrderLogic(new OrderStorage());
        public int Id { set { id = value; } }
        private int? id;
        public OrderWindow()
        {
            InitializeComponent();
        }
        private void OrderWindowLoad(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = orderLogic.Read(new OrderBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        CaloriesBox.Text = view.Calorie.ToString();
                        WishesBox.Text = view.Wishes;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (CaloriesBox.Text == "" || !int.TryParse(CaloriesBox.Text, out int number))
            {
                MessageBox.Show("Введите калорийность в виде числа");
                return;
            }
            int calories = Convert.ToInt32(CaloriesBox.Text);
            string wishes = WishesBox.Text;
            orderLogic.CreateOrUpdate(new OrderBindingModel { Id = id, Calorie = calories, Wishes = wishes });
            this.Close();
        }
    }
}
