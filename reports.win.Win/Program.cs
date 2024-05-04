using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraEditors;
using reports.win.Module.BusinessObjects;
using reports.win.Module.General;
using static reports.win.Module.BusinessObjects.SolicitudesLogonParameters;

namespace reports.win.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            DevExpress.ExpressApp.FrameworkSettings.DefaultSettingsCompatibilityMode = DevExpress.ExpressApp.FrameworkSettingsCompatibilityMode.Latest;
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            WindowsFormsSettings.LoadApplicationSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			DevExpress.Utils.ToolTipController.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            if(Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder) {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
            winWindowsFormsApplication winApplication = new winWindowsFormsApplication();
            winApplication.GetSecurityStrategy().RegisterXPOAdapterProviders();
            //if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
            //    winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //}
            winApplication.CreateCustomTemplate += delegate (object sender, CreateCustomTemplateEventArgs e)
            {

                if (e.Context == TemplateContext.ApplicationWindow)
                {
                    e.Template = new SolicitudesMainRibbonForm();
                }
                else if (e.Context == TemplateContext.View)
                {
                    e.Template = new SolicitudesDetailRibbonForm();
                }
            };

            winApplication.LoggingOn += WinApplication_LoggingOn;
            winApplication.LoggedOn += WinApplication_LoggedOn;
            CriteriaOperator.RegisterCustomFunction(new EsUsuarioOperador());
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            try {
                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.StopSplash();
                winApplication.HandleException(e);
            }
        }

        private static void WinApplication_LoggingOn(object sender, LogonEventArgs e)
        {
            var logon = (SolicitudesLogonParameters)e.LogonParameters;
            XafApplication application = (XafApplication)sender;

            switch (logon.ConexionDB)
            {
                case EnumConexionDB.Desarrollo:
                    application.ConnectionString = "XpoProvider=MSSqlServer;Data Source=localhost;User ID=sa;Password=lucas0286;Initial Catalog=ReportsUdeM;Persist Security Info=true";
                    break;
                case EnumConexionDB.Produccion:
                    application.ConnectionString = "XpoProvider=MSSqlServer;Data Source=localhost;User ID=sa;Password=lucas0286;Initial Catalog=ReportsUdeM;Persist Security Info=true";
                    break;
            }
        }

        private static void WinApplication_LoggedOn(object sender, LogonEventArgs e)
        {
            var logon = (SolicitudesLogonParameters)e.LogonParameters;
            Principal.DB_Actual = logon.ConexionDB;
        }
    }
}
