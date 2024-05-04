using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reports.win.Module.Win.Controllers
{
    public class ShowCheckboxOnLisviewController : ViewController<ListView>, IModelExtender
    {
        GridListEditor gridListEditor;

        public const string EnabledKeyShowCheckboxView = "ShowCheckboxOnLisviewController";

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            gridListEditor = View.Editor as GridListEditor;
            if (gridListEditor != null)
            {
                gridListEditor.ControlDataSourceChanged += GridListEditor_ControlDataSourceChanged;
                if (((IModelshowCheckboxListview)View.Model).ShowCheckboxListview)
                {
                    gridListEditor.GridView.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
                    gridListEditor.GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                }
            }
        }

        private void GridListEditor_ControlDataSourceChanged(object sender, EventArgs e)
        {
            gridListEditor.GridView.ClearSelection();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelViews, IModelDefaultShowCheckboxListview>();
            extenders.Add<IModelListView, IModelshowCheckboxListview>();
        }

        public interface IModelDefaultShowCheckboxListview : IModelNode
        {
            [DefaultValue(true)]
            bool DefaultShowCheckBoxFromListView { get; set; }
        }

        public interface IModelshowCheckboxListview : IModelNode
        {
            bool ShowCheckboxListview { get; set; }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (gridListEditor != null)
            {
                gridListEditor.ControlDataSourceChanged -= GridListEditor_ControlDataSourceChanged;
                gridListEditor = null;
            }
        }
    }
}
