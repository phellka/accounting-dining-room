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
using DiningRoomContracts.BusinessLogicsContracts;
using Unity;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для LunchesWindow.xaml
    /// </summary>
    public partial class LunchesWindow : Window
    {
        private readonly ILunchLogic lunchLogic;
        public LunchesWindow(ILunchLogic lunchLogic)
        {
            InitializeComponent();
            this.lunchLogic = lunchLogic;
        }
        private void LoadData()
        {
            var list = lunchLogic.Read(null);
            if (list != null)
            {
                LunchesData.ItemsSource = list;
                LunchesData.Columns[0].Visibility = Visibility.Hidden;
                LunchesData.Columns[4].Visibility = Visibility.Hidden;
                LunchesData.Columns[5].Visibility = Visibility.Hidden;
                LunchesData.Columns[6].Visibility = Visibility.Hidden;
                LunchesData.Columns[1].Header = "Стоимость";
                LunchesData.Columns[2].Header = "Вес";
                LunchesData.Columns[3].Header = "Дата";
                LunchesData.SelectedItem = null;
            }
        }
        private void LunchesWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CreateCutleryClick(object sender, RoutedEventArgs e)
        {
            LunchWindow lunchWindow = App.Container.Resolve<LunchWindow>();
            lunchWindow.ShowDialog();
            LoadData();
        }
        private void DeleteCutleryClick(object sender, RoutedEventArgs e)
        {
            if (LunchesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите обед");
                return;
            }
            int selecctedLunchId = ((LunchViewModel)LunchesData.SelectedItem).Id;
            lunchLogic.Delete(new LunchBindingModel { Id = selecctedLunchId });
            LoadData();
        }
        private void UpdateCutleryClick(object sender, RoutedEventArgs e)
        {
            if (LunchesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите обед");
                return;
            }
            int selecctedLunchId = ((LunchViewModel)LunchesData.SelectedItem).Id;
            LunchWindow lunchWindow = App.Container.Resolve<LunchWindow>();
            lunchWindow.Id = selecctedLunchId;
            lunchWindow.ShowDialog();
            LoadData();
        }
        private void BindingClick(object sender, RoutedEventArgs e)
        {
            BindOrderLunchesWindow bindOrderLunchesWindow = App.Container.Resolve<BindOrderLunchesWindow>();
            bindOrderLunchesWindow.ShowDialog();
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
