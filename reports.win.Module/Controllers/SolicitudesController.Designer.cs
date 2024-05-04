namespace reports.win.Module.Controllers
{
    partial class Solicitudes_ReportesController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ActionSolicitudEnRevision = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ActionSolicitudRealizado = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ActionSolicitudRechazado = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // ActionSolicitudEnRevision
            // 
            this.ActionSolicitudEnRevision.AcceptButtonCaption = null;
            this.ActionSolicitudEnRevision.CancelButtonCaption = null;
            this.ActionSolicitudEnRevision.Caption = "Marcar en Revisión";
            this.ActionSolicitudEnRevision.ConfirmationMessage = "¿Está seguro que desea realizar esta acción?";
            this.ActionSolicitudEnRevision.Id = "ActionSolicitudEnRevision";
            this.ActionSolicitudEnRevision.ImageName = "Bo_Document";
            this.ActionSolicitudEnRevision.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.ActionSolicitudEnRevision.ToolTip = null;
            this.ActionSolicitudEnRevision.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.ActionSolicitudEnRevision.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ActionSolicitudEnRevision_CustomizePopupWindowParams);
            this.ActionSolicitudEnRevision.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ActionSolicitudEnRevision_Execute);
            // 
            // ActionSolicitudRealizado
            // 
            this.ActionSolicitudRealizado.AcceptButtonCaption = null;
            this.ActionSolicitudRealizado.CancelButtonCaption = null;
            this.ActionSolicitudRealizado.Caption = "Asistencia Atendida";
            this.ActionSolicitudRealizado.ConfirmationMessage = "¿Está seguro que desea realizar esta acción?";
            this.ActionSolicitudRealizado.Id = "ActionSolicitudRealizado";
            this.ActionSolicitudRealizado.ImageName = "Check";
            this.ActionSolicitudRealizado.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.ActionSolicitudRealizado.ToolTip = null;
            this.ActionSolicitudRealizado.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.ActionSolicitudRealizado.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ActionSolicitudRealizado_CustomizePopupWindowParams);
            this.ActionSolicitudRealizado.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ActionSolicitudRealizado_Execute);
            // 
            // ActionSolicitudRechazado
            // 
            this.ActionSolicitudRechazado.AcceptButtonCaption = null;
            this.ActionSolicitudRechazado.CancelButtonCaption = null;
            this.ActionSolicitudRechazado.Caption = "Rechazado";
            this.ActionSolicitudRechazado.ConfirmationMessage = "¿Está seguro que desea realizar esta acción?";
            this.ActionSolicitudRechazado.Id = "ActionSolicitudRechazado";
            this.ActionSolicitudRechazado.ImageName = "TrackingChanges_Reject";
            this.ActionSolicitudRechazado.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.ActionSolicitudRechazado.ToolTip = null;
            this.ActionSolicitudRechazado.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.ActionSolicitudRechazado.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ActionSolicitudRechazado_CustomizePopupWindowParams);
            this.ActionSolicitudRechazado.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ActionSolicitudRechazado_Execute);
            // 
            // Solicitudes_ReportesController
            // 
            this.Actions.Add(this.ActionSolicitudEnRevision);
            this.Actions.Add(this.ActionSolicitudRealizado);
            this.Actions.Add(this.ActionSolicitudRechazado);
            this.TargetObjectType = typeof(reports.win.Module.BusinessObjects.Solicitudes_Reportes);
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ActionSolicitudEnRevision;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ActionSolicitudRealizado;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ActionSolicitudRechazado;
    }
}
