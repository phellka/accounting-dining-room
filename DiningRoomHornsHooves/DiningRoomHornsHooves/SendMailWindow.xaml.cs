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
using System.Text.RegularExpressions;
using DiningRoomContracts.BusinessLogicsContracts;
using DiningRoomContracts.BindingModels;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для SendMailWindow.xaml
    /// </summary>
    public partial class SendMailWindow : Window
    {
        private readonly IReportLogic reportLogic;
        private readonly IVisitorLogic visitorLogic;
        public DateTime DateAfter { get; set; }
        public DateTime DateBefore { get; set; }
        public SendMailWindow(IReportLogic reportLogic, IVisitorLogic visitorLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            this.visitorLogic = visitorLogic;
        }
        private void SendMailWindowLoad(object sender, RoutedEventArgs e)
        {
            MailAdressBox.Text = visitorLogic.GetVisitorData(new VisitorBindingModel { Login = AuthorizationWindow.AutorizedVisitor}).Email;
        }
        private void SendMessageClick(object sender, RoutedEventArgs e)
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(MailAdressBox.Text);
            if (!match.Success)
            {
                MessageBox.Show("Данные введенные в поле \"Адрес почты\" должны соответствовать адресу электронной почте");
                return;
            }
            reportLogic.saveLunchesToPdfFile(new ReportBindingModel()
            {
                DateAfter = DateAfter,
                DateBefore = DateBefore,
                FileName = "reportOrdersByDate.pdf",
                VisitorLogin = AuthorizationWindow.AutorizedVisitor
            });
            MessageBox.Show("Письмо успешно отправлено");
            Close();
        }
    }
}
