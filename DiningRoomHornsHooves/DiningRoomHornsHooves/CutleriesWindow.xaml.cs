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
    /// Логика взаимодействия для CutleriesWindow.xaml
    /// </summary>
    public partial class CutleriesWindow : Window
    {
        CutleryLogic cutleryLogic = new CutleryLogic(new CutleryStorage());
        public CutleriesWindow()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            var list = cutleryLogic.Read(null);
            if (list != null)
            {
                CutleriesData.ItemsSource = list;
                CutleriesData.Columns[0].Visibility = Visibility.Hidden;
                CutleriesData.Columns[3].Visibility = Visibility.Hidden;
                CutleriesData.Columns[1].Header = "Название";
                CutleriesData.Columns[2].Header = "количество";
                CutleriesData.Columns[4].Header = "Id заказа";
                CutleriesData.SelectedItem = null;
            }
        }
        private void CutleriesWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CreateCutleryClick(object sender, RoutedEventArgs e)
        {
            CutleryWindow cutleryWindow = new CutleryWindow();
            cutleryWindow.ShowDialog();
            LoadData();
        }
        private void DeleteCutleryClick(object sender, RoutedEventArgs e)
        {
            if (CutleriesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите приборы");
                return;
            }
            int selecctedCutleryId = ((CutleryViewModel)CutleriesData.SelectedItem).Id;
            cutleryLogic.Delete(new CutleryBindingModel { Id = selecctedCutleryId });
            LoadData();
        }
        private void UpdateCutleryClick(object sender, RoutedEventArgs e)
        {
            if (CutleriesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите приборы");
                return;
            }
            int selecctedCutleryId = ((CutleryViewModel)CutleriesData.SelectedItem).Id;
            CutleryWindow cutleryWindow = new CutleryWindow();
            cutleryWindow.Id = selecctedCutleryId;
            cutleryWindow.ShowDialog();
            LoadData();
        }
    }
}
