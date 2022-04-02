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

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для BindOrderLunchesWindow.xaml
    /// </summary>
    public partial class BindOrderLunchesWindow : Window
    {
        private readonly ILunchLogic lunchLogic;
        private readonly IOrderLogic orderLogic;
        public BindOrderLunchesWindow(ILunchLogic lunchLogic, IOrderLogic orderLogic)
        {
            InitializeComponent();
            this.lunchLogic = lunchLogic;
            this.orderLogic = orderLogic;
        }
        private void LoadData()
        {
            var listLunch = lunchLogic.Read(null);
            if (listLunch != null)
            {
                LunchesListBox.ItemsSource = listLunch;
                LunchesListBox.SelectedItem = null;
            }
            var listOrder = orderLogic.Read(null);
            if (listLunch != null)
            {
                OrdersListBox.ItemsSource = listOrder;
                OrdersListBox.SelectedItem = null;
            }
        }
        private void BindOrderLunchesWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BindClick(object sender, RoutedEventArgs e)
        {
            if (OrdersListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            if (LunchesListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите обеды");
                return;
            }
            if (CountBox.Text == "" || !int.TryParse(CountBox.Text, out int number))
            {
                MessageBox.Show("Введите количество в виде числа");
                return;
            }
            int orderId = ((OrderViewModel)OrdersListBox.SelectedItem).Id;
            foreach(LunchViewModel i in LunchesListBox.SelectedItems)
            {
                lunchLogic.AddOrder((i.Id, (orderId, Convert.ToInt32(CountBox.Text))));
            }
            MessageBox.Show("Привязка создана");
        }
    }
}
