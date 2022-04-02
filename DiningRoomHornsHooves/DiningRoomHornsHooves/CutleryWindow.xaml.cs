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
    /// Логика взаимодействия для CutleryWindow.xaml
    /// </summary>
    public partial class CutleryWindow : Window
    {
        private readonly ICutleryLogic cutleryLogic;
        private readonly IOrderLogic orderLogic;
        public int Id { set { id = value; } }
        private int? id;
        public CutleryWindow(ICutleryLogic cutleryLogic, IOrderLogic orderLogic)
        {
            InitializeComponent();
            this.cutleryLogic = cutleryLogic;
            this.orderLogic = orderLogic;
        }
        private void OrderWindowLoad(object sender, RoutedEventArgs e)
        {
            var list = orderLogic.Read(null);
            OrdersComboBox.ItemsSource = list;
            OrdersComboBox.SelectedItem = null;
            //OrdersComboBox.DisplayMemberPath = "Calorie";
            if (id.HasValue)
            {
                try
                {
                    var view = cutleryLogic.Read(new CutleryBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        NameBox.Text = view.Name;
                        CountBox.Text = view.Count.ToString();
                        OrdersComboBox.SelectedIndex = list.IndexOf(list.FirstOrDefault(rec => rec.Id == view.CulteryOrder));
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
            if (NameBox.Text == "")
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (CountBox.Text == "" || !int.TryParse(CountBox.Text, out int number))
            {
                MessageBox.Show("Введите количество в виде числа");
                return;
            }
            if (OrdersComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }
            string name = NameBox.Text;
            int count = Convert.ToInt32(CountBox.Text);
            cutleryLogic.CreateOrUpdate(new CutleryBindingModel { Id = id, Name = name, 
                Count = count, CulteryOrder = ((OrderViewModel)OrdersComboBox.SelectedItem).Id});
            this.Close();
        }
    }
}
