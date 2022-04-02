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
using DiningRoomContracts.ViewModels;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        OrderLogic orderLogic = new OrderLogic(new OrderStorage());
        public OrdersWindow()
        {
            InitializeComponent();

        }
        private void LoadData()
        {
            var list = orderLogic.Read(null);
            if (list != null)
            {
                OrdersData.ItemsSource = list;
                OrdersData.Columns[0].Visibility = Visibility.Hidden;
                OrdersData.Columns[3].Visibility = Visibility.Hidden;
                OrdersData.Columns[1].Header = "Калорийность";
                OrdersData.Columns[2].Header = "Пожелание";
                OrdersData.SelectedItem = null;
            }
        }
        private void OrderWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CreateOrderClick(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.ShowDialog();
            LoadData();
        }
        private void DeleteOrderClick(object sender, RoutedEventArgs e)
        {
            if (OrdersData.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            int selecctedOrderId = ((OrderViewModel)OrdersData.SelectedItem).Id;
            orderLogic.Delete(new OrderBindingModel { Id = selecctedOrderId});
            LoadData();
        }
        private void UpdateOrderClick(object sender, RoutedEventArgs e)
        {
            if (OrdersData.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            int selecctedOrderId = ((OrderViewModel)OrdersData.SelectedItem).Id;
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Id = selecctedOrderId;
            orderWindow.ShowDialog();
            LoadData();
        }
    }
}
