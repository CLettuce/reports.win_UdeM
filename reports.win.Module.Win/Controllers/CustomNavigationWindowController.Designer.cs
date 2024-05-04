namespace reports.win.Module.Win.Controllers
{
    partial class CustomNavigationWindowController
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
            this.DBActualAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // DBActualAction
            // 
            this.DBActualAction.Caption = "DBActual Action";
            this.DBActualAction.Category = "AccionesPrincipal";
            this.DBActualAction.ConfirmationMessage = null;
            this.DBActualAction.Id = "DBActualAction";
            this.DBActualAction.ImageName = "Actions_Database";
            this.DBActualAction.ToolTip = null;
            // 
            // CustomNavigationWindowController
            // 
            this.Actions.Add(this.DBActualAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction DBActualAction;
    }
}
