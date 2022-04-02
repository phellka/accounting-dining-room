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
using DiningRoomBusinessLogic.OfficePackage.Implements;
using DiningRoomContracts.BusinessLogicsContracts;
using Unity;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICookLogic cookLogic;
        private readonly IProductLogic productLogic;
        public MainWindow(ICookLogic cookLogic, IProductLogic productLogic)
        {
            InitializeComponent();
            this.cookLogic = cookLogic;
            this.productLogic = productLogic;
        }
        private void CreateCookClick(object sender, RoutedEventArgs e)
        {
            cookLogic.Create();
        }
        private void CreateProductClick(object sender, RoutedEventArgs e)
        {
            productLogic.Create();
        }
        private void CreateProductCooksClick(object sender, RoutedEventArgs e)
        {
            productLogic.AddCooks();
        }
        private void OrdersClick(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = App.Container.Resolve<OrdersWindow>();
            ordersWindow.ShowDialog();
        }
        private void CutleriesClick(object sender, RoutedEventArgs e)
        {
            CutleriesWindow cutleriesWindow = App.Container.Resolve<CutleriesWindow>();
            cutleriesWindow.ShowDialog();
        }
        private void LunchesClick(object sender, RoutedEventArgs e)
        {
            LunchesWindow lunchesWindow = App.Container.Resolve<LunchesWindow>();
            lunchesWindow.ShowDialog();
        }
        private void ReportLunchesClick(object sender, RoutedEventArgs e)
        {
            ReportLunchesWindow reportLunchesWindow = App.Container.Resolve<ReportLunchesWindow>();
            reportLunchesWindow.ShowDialog();
        }
        private void ReportCooksbyLunchesClick(object sender, RoutedEventArgs e)
        {
            ReportCooksByLanchesWindow reportCooksByLanchesWindow = App.Container.Resolve<ReportCooksByLanchesWindow>();
            reportCooksByLanchesWindow.ShowDialog();
        }
    }
}
