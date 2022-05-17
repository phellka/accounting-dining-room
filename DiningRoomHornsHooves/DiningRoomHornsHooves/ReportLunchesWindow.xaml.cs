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
using Microsoft.Win32;
using DiningRoomContracts.BusinessLogicsContracts;
using DiningRoomContracts.BindingModels;
using Unity;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Логика взаимодействия для ReportLunchesWindow.xaml
    /// </summary>
    public partial class ReportLunchesWindow : Window
    {
        private readonly IReportLogic reportLogic;
        public ReportLunchesWindow(IReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            DataGridTextColumn textColumnDate = new DataGridTextColumn();
            textColumnDate.Header = "Дата создания обеда";
            textColumnDate.Binding = new Binding("dateCreate");
            DataGrid.Columns.Add(textColumnDate);
            DataGridTextColumn textColumnWeight = new DataGridTextColumn();
            textColumnWeight.Header = "Вес обеда";
            textColumnWeight.Binding = new Binding("weight");
            DataGrid.Columns.Add(textColumnWeight);
            DataGridTextColumn textColumnPrice = new DataGridTextColumn();
            textColumnPrice.Header = "Цена обеда";
            textColumnPrice.Binding = new Binding("price");
            DataGrid.Columns.Add(textColumnPrice);
            DataGridTextColumn textColumnCutleryName = new DataGridTextColumn();
            textColumnCutleryName.Header = "Название прибора";
            textColumnCutleryName.Binding = new Binding("cutleryName");
            DataGrid.Columns.Add(textColumnCutleryName);
            DataGridTextColumn textColumnCutleryCount = new DataGridTextColumn();
            textColumnCutleryCount.Header = "Количество приборов";
            textColumnCutleryCount.Binding = new Binding("cutleryCount");
            DataGrid.Columns.Add(textColumnCutleryCount);
            DataGridTextColumn textColumnCookName = new DataGridTextColumn();
            textColumnCookName.Header = "Имя повара";
            textColumnCookName.Binding = new Binding("cookName");
            DataGrid.Columns.Add(textColumnCookName);
        }
        private class itemLucnh
        {
            public string dateCreate { get; set; }
            public string weight { get; set; }
            public string price { get; set; }
            public string cutleryName { get; set; }
            public string cutleryCount { get; set; }
            public string cookName { get; set; }
        }
        private void SendMessageClick(object sender, RoutedEventArgs e)
        {
            if (DatePickerAfter.SelectedDate == null || DatePickerBefore.SelectedDate == null ||
                DatePickerAfter.SelectedDate >= DatePickerBefore.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка");
                return;
            }
            SendMailWindow sendMailWindow = App.Container.Resolve<SendMailWindow>();
            sendMailWindow.DateAfter = DatePickerAfter.SelectedDate.Value;
            sendMailWindow.DateBefore = DatePickerBefore.SelectedDate.Value;
            sendMailWindow.ShowDialog();
        }
        private void ShowClick(object sender, RoutedEventArgs e)
        {
            if (DatePickerAfter.SelectedDate == null || DatePickerBefore.SelectedDate == null ||
                DatePickerAfter.SelectedDate >= DatePickerBefore.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка");
                return;
            }
            try
            {
                var dict = reportLogic.GetLunchesPCView(new ReportBindingModel() { 
                    DateAfter = DatePickerAfter.SelectedDate,
                    DateBefore = DatePickerBefore.SelectedDate,
                    VisitorLogin = AuthorizationWindow.AutorizedVisitor
                });
                if (dict != null)
                {
                    DataGrid.Items.Clear();
                    foreach (var elem in dict)
                    {
                        DataGrid.Items.Add(new itemLucnh() { 
                            dateCreate = elem.DateCreate.ToShortDateString(),
                            weight = elem.Weight.ToString(),
                            price = elem.Price.ToString()
                        });
                        for (int i = 0; i < Math.Max(elem.Cooks.Count, elem.Cutleries.Count); ++i)
                        {
                            itemLucnh newItem = new itemLucnh();
                            if (i < elem.Cooks.Count)
                            {
                                newItem.cookName = elem.Cooks[i].Name;
                            }
                            if (i < elem.Cutleries.Count)
                            {
                                newItem.cutleryName = elem.Cutleries[i].Name;
                                newItem.cutleryCount = elem.Cutleries[i].Count.ToString();
                            }
                            DataGrid.Items.Add(newItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
