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
using System.Collections.ObjectModel;
using DiningRoomContracts.BusinessLogicsContracts;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для LunchWindow.xaml
    /// </summary>
    public partial class LunchWindow : Window
    {
        private readonly ILunchLogic lunchLogic;
        private readonly IProductLogic productLogic;
        public int Id { set { id = value; } }
        private int? id;
        public LunchWindow(ILunchLogic lunchLogic, IProductLogic productLogic)
        {
            InitializeComponent();
            this.lunchLogic = lunchLogic;
            this.productLogic = productLogic;
        }
        private void LunchWindowLoad(object sender, RoutedEventArgs e)
        {
            var list = productLogic.Read(null);
            ReadyListBox.ItemsSource = list;
            ReadyListBox.SelectedItem = null;
            SelectedListBox.SelectedItem = null;
            if (id.HasValue)
            {
                try
                {
                    var view = lunchLogic.Read(new LunchBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        PriceBox.Text = view.Price.ToString();
                        WeightBox.Text = view.Weight.ToString();
                        DatePicker.SelectedDate = view.Date;
                        var selectedList = productLogic.Read(null).Where(rec => view.LunchProduts.ContainsKey(rec.Id));
                        if (view.LunchProduts.Count != 0)
                        {
                            CountBox.Text = view.LunchProduts.First().Value.ToString();
                        }
                        foreach(ProductViewModel i in ReadyListBox.Items)
                        {
                            if (selectedList.FirstOrDefault(rec => rec.Id == i.Id) != null)
                            {
                                SelectedListBox.Items.Add(i);
                            }
                        }
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
            if (PriceBox.Text == "" || !int.TryParse(PriceBox.Text, out int number))
            {
                MessageBox.Show("Введите цену в виде числа");
                return;
            }
            if (WeightBox.Text == "" || !int.TryParse(WeightBox.Text, out int numberWei))
            {
                MessageBox.Show("Введите вес в виде числа");
                return;
            }
            if (CountBox.Text == "" || !int.TryParse(CountBox.Text, out int numberCount))
            {
                MessageBox.Show("Введите количество в виде числа");
                return;
            }
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }
            if (SelectedListBox.Items.Count == 0)
            {
                MessageBox.Show("Выберите продукты");
                return;
            }
            int price = Convert.ToInt32(PriceBox.Text);
            int weight = Convert.ToInt32(WeightBox.Text);
            int count = Convert.ToInt32(CountBox.Text);
            DateTime date = (DateTime)DatePicker.SelectedDate;
            Dictionary<int, int> lunchProducts = new Dictionary<int, int>();
            foreach(ProductViewModel i in SelectedListBox.Items)
            {
                lunchProducts.Add(i.Id, count);
            }
            lunchLogic.CreateOrUpdate(new LunchBindingModel
            {
                Id = id,
                Price = price,
                Weight = weight,
                Date = date,
                LunchProduts = lunchProducts
            });
            this.Close();
        }
        private void ReadyListBoxChange(object sender, SelectionChangedEventArgs args)
        {
            if (ReadyListBox.SelectedItem != null)
            {
                var changeItem = ReadyListBox.SelectedItem;
                if (!SelectedListBox.Items.Contains(changeItem))
                {
                    SelectedListBox.Items.Add(changeItem);
                }
                ReadyListBox.SelectedItem = null;
            }
        }
        private void SelectedListBoxChange(object sender, SelectionChangedEventArgs args)
        {
            if (SelectedListBox.SelectedItem != null)
            {
                int removeIndex = SelectedListBox.SelectedIndex;
                SelectedListBox.SelectedItem = null;
                SelectedListBox.Items.RemoveAt(removeIndex);
            }
        }
    }
}
