using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;
using DiningRoomContracts.BusinessLogicsContracts;
using DiningRoomContracts.StoragesContracts;
using DiningRoomDatabaseImplement.Implements;
using DiningRoomBusinessLogic.BusinessLogics;
using DiningRoomBusinessLogic.OfficePackage;
using DiningRoomBusinessLogic.OfficePackage.Implements;

namespace DiningRoomHornsHooves
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AuthorizationWindow authorizationWindow = Container.Resolve<AuthorizationWindow>();
            authorizationWindow.ShowDialog();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ICookStorage, CookStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICutleryStorage, CutleryStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILunchStorage, LunchStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductStorage, ProductStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerStorage, WorkerStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICookLogic, CookLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICutleryLogic, CutleryLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILunchLogic, LunchLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductLogic, ProductLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerLogic, WorkerLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
