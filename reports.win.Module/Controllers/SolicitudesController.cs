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
using reports.win.Module.BusinessObjects.NonPersistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reports.win.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Solicitudes_ReportesController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public Solicitudes_ReportesController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void SolicitarNotas(ref CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = e.Application.CreateObjectSpace(typeof(clsParameterNotas));
            clsParameterNotas currentObject = objectSpace.CreateObject<clsParameterNotas>();
            DetailView detailView = e.Application.CreateDetailView(objectSpace, currentObject);
            detailView.ViewEditMode = ViewEditMode.Edit;

            e.View = detailView;
        }

        private void ActionSolicitudEnRevision_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Solicitudes_Reportes CurrentObject = (Solicitudes_Reportes)View.CurrentObject;
            ObjectSpace.CommitChanges();

            var auditoria = ObjectSpace.CreateObject<Solicitudes_Auditoria>();
            auditoria.EstadoAnterior = CurrentObject.Estado.Value;
            CurrentObject.Estado = Solicitudes_Reportes.EstadoSolicitud.EnRevision;

            auditoria.EstadoNuevo = CurrentObject.Estado.Value;
            auditoria.Nota = ((clsParameterNotas)e.PopupWindowViewCurrentObject).Notas;

            CurrentObject.SolicitudesAuditoria.Add(auditoria);

            ObjectSpace.CommitChanges();
        }

        private void ActionSolicitudEnRevision_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            SolicitarNotas(ref e);
        }

        private void ActionSolicitudRealizado_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Solicitudes_Reportes CurrentObject = (Solicitudes_Reportes)View.CurrentObject;
            ObjectSpace.CommitChanges();

            var auditoria = ObjectSpace.CreateObject<Solicitudes_Auditoria>();
            auditoria.EstadoAnterior = CurrentObject.Estado.Value;
            CurrentObject.Estado = Solicitudes_Reportes.EstadoSolicitud.Atendido;
            auditoria.EstadoNuevo = CurrentObject.Estado.Value;
            auditoria.Nota = ((clsParameterNotas)e.PopupWindowViewCurrentObject).Notas;
            CurrentObject.SolicitudesAuditoria.Add(auditoria);
            ObjectSpace.CommitChanges();
        }

        private void ActionSolicitudRealizado_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            SolicitarNotas(ref e);
        }

        private void ActionSolicitudRechazado_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Solicitudes_Reportes CurrentObject = (Solicitudes_Reportes)View.CurrentObject;
            ObjectSpace.CommitChanges();

            var auditoria = ObjectSpace.CreateObject<Solicitudes_Auditoria>();
            auditoria.EstadoAnterior = CurrentObject.Estado.Value;
            CurrentObject.Estado = Solicitudes_Reportes.EstadoSolicitud.Rechazado;
            auditoria.EstadoNuevo = CurrentObject.Estado.Value;
            auditoria.Nota = ((clsParameterNotas)e.PopupWindowViewCurrentObject).Notas;
            CurrentObject.NotasCorreo = ((clsParameterNotas)e.PopupWindowViewCurrentObject).Notas;
            CurrentObject.SolicitudesAuditoria.Add(auditoria);
            ObjectSpace.CommitChanges();
            //ObjectSpace.Refresh();
        }

        private void ActionSolicitudRechazado_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            SolicitarNotas(ref e);
        }
    }
}
