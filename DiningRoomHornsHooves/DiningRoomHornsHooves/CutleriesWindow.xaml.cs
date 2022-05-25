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
    /// Логика взаимодействия для CutleriesWindow.xaml
    /// </summary>
    public partial class CutleriesWindow : Window
    {
        private readonly ICutleryLogic cutleryLogic;
        public CutleriesWindow( ICutleryLogic cutleryLogic)
        {
            InitializeComponent();
            this.cutleryLogic = cutleryLogic;
        }
        private void LoadData()
        {
            var list = cutleryLogic.Read(new CutleryBindingModel { VisitorLogin = AuthorizationWindow.AutorizedVisitor});
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
            CutleryWindow cutleryWindow = App.Container.Resolve<CutleryWindow>();
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
            MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int selecctedCutleryId = ((CutleryViewModel)CutleriesData.SelectedItem).Id;
                cutleryLogic.Delete(new CutleryBindingModel { Id = selecctedCutleryId, VisitorLogin = AuthorizationWindow.AutorizedVisitor });
                LoadData();
            }
        }
        private void UpdateCutleryClick(object sender, RoutedEventArgs e)
        {
            if (CutleriesData.SelectedItem == null)
            {
                MessageBox.Show("Выберите приборы");
                return;
            }
            int selecctedCutleryId = ((CutleryViewModel)CutleriesData.SelectedItem).Id;
            CutleryWindow cutleryWindow = App.Container.Resolve<CutleryWindow>();
            cutleryWindow.Id = selecctedCutleryId;
            cutleryWindow.ShowDialog();
            LoadData();
        }
    }
}
