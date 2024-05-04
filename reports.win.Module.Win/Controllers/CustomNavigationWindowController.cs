using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using reports.win.Module.BusinessObjects;
using reports.win.Module.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reports.win.Module.Win.Controllers
{
    
    public partial class CustomNavigationWindowController : WindowController
    {
        
        public CustomNavigationWindowController()
        {
            InitializeComponent();

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            DBActualAction.Caption = Principal.DB_Actual == SolicitudesLogonParameters.EnumConexionDB.Produccion ? "Producción" : "Desarrollo";
        }
        protected override void OnDeactivated()
        {

            base.OnDeactivated();
        }
    }
}
