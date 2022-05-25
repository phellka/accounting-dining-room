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
using DiningRoomContracts.BindingModels;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BusinessLogicsContracts;
using Unity;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        private readonly IOrderLogic orderLogic;
        public OrdersWindow(IOrderLogic orderLogic)
        {
            InitializeComponent();
            this.orderLogic = orderLogic;

        }
        private void LoadData()
        {
            var list = orderLogic.Read(new OrderBindingModel { VisitorLogin = AuthorizationWindow.AutorizedVisitor });
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
            OrderWindow orderWindow = App.Container.Resolve<OrderWindow>();
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

            MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int selecctedOrderId = ((OrderViewModel)OrdersData.SelectedItem).Id;
                orderLogic.Delete(new OrderBindingModel { Id = selecctedOrderId, VisitorLogin = AuthorizationWindow.AutorizedVisitor });
                LoadData();
            }
        }
        private void UpdateOrderClick(object sender, RoutedEventArgs e)
        {
            if (OrdersData.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            int selecctedOrderId = ((OrderViewModel)OrdersData.SelectedItem).Id;
            OrderWindow orderWindow = App.Container.Resolve<OrderWindow>();
            orderWindow.Id = selecctedOrderId;
            orderWindow.ShowDialog();
            LoadData();
        }
    }
}
