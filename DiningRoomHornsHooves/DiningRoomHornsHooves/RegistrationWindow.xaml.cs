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
using System.Text.RegularExpressions;
using DiningRoomContracts.BusinessLogicsContracts;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly IVisitorLogic workerLogic;
        public RegistrationWindow(IVisitorLogic workerLogic)
        {
            InitializeComponent();
            this.workerLogic = workerLogic;
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;
            string name = NameTextBox.Text;
            string email = EmailTextBox.Text;
            if (login == "" || password == "")
            {
                MessageBox.Show("Для регистрации необходимо ввести логин и пароль");
                return;
            }
            if(name == "" || email == "")
            {
                MessageBox.Show("Поля \"имя\" и \"Email\" - обязательны к заполнению");
                return;
            }
            if (password.Length < 5)
            {
                MessageBox.Show("Минимальная длина пароля 5 символов");
                return;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                MessageBox.Show("Данные введенные в поле \"Email\" должны соответствовать адресу электронной почте");
                return;
            }
            try
            {
                workerLogic.Create(new VisitorBindingModel { Login = login, Password = password, Email = email, Name = name});
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
