using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reports.win.Module.Controllers
{
    public class ShowDetailViewFromListViewController : ViewController<ListView>, IModelExtender
    {
        public const string EnabledKeyShowDetailView = "ShowDetailViewFromListViewController";
        protected override void OnActivated()
        {
            base.OnActivated();
            ListViewProcessCurrentObjectController targetController = Frame.GetController<ListViewProcessCurrentObjectController>();
            if (targetController != null)
            {
                targetController.ProcessCurrentObjectAction.Enabled[EnabledKeyShowDetailView] = ((IModelShowDetailView)View.Model).ShowDetailView;
            }
        }
        public void ExtendModelInterfaces(DevExpress.ExpressApp.Model.ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelViews, IModelDefaultShowDetailView>();
            extenders.Add<IModelListView, IModelShowDetailView>();
        }
    }
    public interface IModelDefaultShowDetailView : IModelNode
    {
        [DefaultValue(true)]
        bool DefaultShowDetailViewFromListView { get; set; }
    }
    public interface IModelShowDetailView : IModelNode
    {
        bool ShowDetailView { get; set; }
    }
    [DomainLogic(typeof(IModelShowDetailView))]
    public static class ModelShowDetailViewLogic
    {
        public static bool Get_ShowDetailView(IModelShowDetailView showDetailView)
        {
            IModelDefaultShowDetailView defaultShowDetailViewFromListView = showDetailView.Parent as IModelDefaultShowDetailView;
            if (defaultShowDetailViewFromListView != null)
            {
                return defaultShowDetailViewFromListView.DefaultShowDetailViewFromListView;
            }
            return true;
        }
    }
}
