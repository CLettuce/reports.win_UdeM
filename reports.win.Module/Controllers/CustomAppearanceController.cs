using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using reports.win.Module.BusinessObjects.Interfaces;
using reports.win.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reports.win.Module.Controllers
{
    public partial class CustomAppearanceController : ViewController
    {
        AppearanceController targetController;
        List<AparienciasSolicitudes> CachedAppearances;
        public CustomAppearanceController()
        {
            TargetObjectType = typeof(ISupportCustomAppearance);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Type CurrentType = View.ObjectTypeInfo.Type;
            string CurrentViewContext = View.GetType().Name;

            CachedAppearances = ObjectSpace.GetObjectsQuery<AparienciasSolicitudes>()
                .Where(a => a.EstadoApariencia == true && a.DeclaringType == CurrentType && (a.Context == "Any" || a.Context == CurrentViewContext))
                .ToList();

            if (CachedAppearances.Count > 0)
            {
                targetController = Frame.GetController<AppearanceController>();
                if (targetController != null)
                {
                    targetController.CollectAppearanceRules += TargetController_CollectAppearanceRules;
                }
            }
        }

        private void TargetController_CollectAppearanceRules(object sender, CollectAppearanceRulesEventArgs e)
        {
            e.AppearanceRules.AddRange(CachedAppearances);
            targetController.Refresh();
        }

        protected override void OnDeactivated()
        {
            CachedAppearances.Clear();
            if (targetController != null)
            {
                targetController.CollectAppearanceRules -= TargetController_CollectAppearanceRules;
            }
            base.OnDeactivated();
        }
    }
}
