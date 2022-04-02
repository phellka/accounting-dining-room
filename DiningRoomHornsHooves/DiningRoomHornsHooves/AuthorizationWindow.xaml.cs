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

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        WorkerLogic workerLogic = new WorkerLogic(new WorkerStorage());
        public AuthorizationWindow()
        {
            InitializeComponent();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }
        private void AutorizedClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;
            if (login == "" || password == "")
            {
                MessageBox.Show("Для авторизации необходимо ввести логин и пароль");
                return;
            }
            if (workerLogic.Login(new WorkerBindingModel { Login = login, Password = password}))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не существует или пароль введен не верно");
                return;
            }
        }
    }
}
