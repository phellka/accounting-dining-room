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
using DiningRoomBusinessLogic.BusinessLogics;
using DiningRoomDatabaseImplement.Implements;
using DiningRoomBusinessLogic.OfficePackage.Implements;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CookLogic cookLogic = new CookLogic(new CookStorage());
        ProductLogic productLogic = new ProductLogic(new ProductStorage(), new CookStorage());
        public MainWindow()
        {
            InitializeComponent();
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
            OrdersWindow ordersWindow = new OrdersWindow();
            ordersWindow.ShowDialog();
        }
        private void CutleriesClick(object sender, RoutedEventArgs e)
        {
            CutleriesWindow cutleriesWindow = new CutleriesWindow();
            cutleriesWindow.ShowDialog();
        }
        private void LunchesClick(object sender, RoutedEventArgs e)
        {
            LunchesWindow lunchesWindow = new LunchesWindow();
            lunchesWindow.ShowDialog();
        }
        private void ReportLunchesClick(object sender, RoutedEventArgs e)
        {
            ReportLunchesWindow reportLunchesWindow = new ReportLunchesWindow(
                new ReportLogic(new LunchStorage(), new CutleryStorage(), new CookStorage(), new ProductStorage(), new SaveToPdf(), new SaveToWord(), new SaveToExcel()));
            reportLunchesWindow.ShowDialog();
        }
        private void ReportCooksbyLunchesClick(object sender, RoutedEventArgs e)
        {
            ReportCooksByLanchesWindow reportCooksByLanchesWindow = new ReportCooksByLanchesWindow(new LunchStorage(), 
                new ReportLogic(new LunchStorage(), new CutleryStorage(), new CookStorage(), new ProductStorage(), new SaveToPdf(), new SaveToWord(), new SaveToExcel()));
            reportCooksByLanchesWindow.ShowDialog();
        }
    }
}
