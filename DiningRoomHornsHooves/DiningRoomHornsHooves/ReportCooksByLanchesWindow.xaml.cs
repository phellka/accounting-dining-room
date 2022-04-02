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
using DiningRoomContracts.BusinessLogicsContracts;
using DiningRoomContracts.StoragesContracts;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;
using Microsoft.Win32;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для ReportCooksByLanchesWindow.xaml
    /// </summary>
    public partial class ReportCooksByLanchesWindow : Window
    {
        private readonly ILunchStorage lunchStorage;
        private readonly IReportLogic reportLogic;
        public ReportCooksByLanchesWindow(ILunchStorage lunchStorage, IReportLogic reportLogic)
        {
            InitializeComponent();
            this.lunchStorage = lunchStorage;
            this.reportLogic = reportLogic;
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LoadData()
        {
            var list = lunchStorage.GetFullList();
            if (list != null)
            {
                LunchesListBox.ItemsSource = list;
                LunchesListBox.SelectedItem = null;
            }
        }
        private void ReportCooksByLanchesWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void ExcelClick(object sender, RoutedEventArgs e)
        {
            if (LunchesListBox.SelectedItem != null)
            {
                var list = LunchesListBox.SelectedItems.Cast<LunchViewModel>().ToList();
                var dialog = new SaveFileDialog();
                dialog.Filter = "xlsx|*.xlsx";
                if (dialog.ShowDialog() == true) {
                    reportLogic.saveCooksToExcel(new ReportBindingModel() { FileName = dialog.FileName, lunches = list});
                }
                MessageBox.Show("Файл успешно сохранен");
            }
            else
            {
                MessageBox.Show("Выберите обеды", "Ошибка");
            }
        }
        private void WordClick(object sender, RoutedEventArgs e)
        {
            if (LunchesListBox.SelectedItem != null)
            {
                var list = LunchesListBox.SelectedItems.Cast<LunchViewModel>().ToList();
                var dialog = new SaveFileDialog();
                dialog.Filter = "docx|*.docx";
                if (dialog.ShowDialog() == true)
                {
                    reportLogic.saveCooksToWord(new ReportBindingModel() { FileName = dialog.FileName, lunches = list });
                }
                MessageBox.Show("Файл успешно сохранен");
            }
            else
            {
                MessageBox.Show("Выберите обеды", "Ошибка");
            }
        }
    }
}
