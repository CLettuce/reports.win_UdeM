using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp;
using reports.win.Module.BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using reports.win.Module.BusinessObjects;

namespace reports.win.Module.Controllers
{
    public class SecuenciaController : ViewController<DetailView>
    {
        Secuencia seq;
        bool DesactivarOnSaving = false;
        private AppearanceController appearanceController;

        public string Codigo1;
        public string Codigo2;
        public DateTime FechaCustom1;
        public DateTime FechaCustom2;

        public SecuenciaController()
        {
            TargetObjectType = typeof(ISupportSecuencia);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Type ObjectoTipo;

            ObjectoTipo = View.CurrentObject.GetType();
            IQueryable<Secuencia> query = ObjectSpace.GetObjectsQuery<Secuencia>(true);
            seq = query.Where(o => o.ObjetoAplicar == ObjectoTipo && o.Automatico).FirstOrDefault();

            if (seq != null)
            {
                var obj = View.CurrentObject;
                var propertyInfo = obj.GetType().GetProperty(seq.Propiedad.Name);

                if (propertyInfo != null)
                {
                    AppearanceController appearanceController = Frame.GetController<AppearanceController>();
                    if (appearanceController != null)
                    {
                        appearanceController.CustomApplyAppearance += AppearanceController_CustomApplyAppearance;
                    }

                    if (ObjectSpace.IsNewObject(View.CurrentObject))
                    {
                        ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
                        ObjectSpace.ObjectSaved += ObjectSpace_ObjectSaved;
                        DesactivarOnSaving = true;
                    }
                }
            }
        }

        private void AppearanceController_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
        {
            if (e.AppearanceObject.Visibility == null || e.AppearanceObject.Visibility == ViewItemVisibility.Show)
            {
                if (View is DetailView)
                {
                    if (e.Item is PropertyEditor)
                    {
                        if (((PropertyEditor)e.Item).Id == seq.Propiedad.Name && seq.BloquearCampo)
                        {
                            e.AppearanceObject.Enabled = false;
                        }
                    }
                }
            }
        }

        private void ObjectSpace_ObjectSaved(object sender, ObjectManipulatingEventArgs e)
        {
            if (ObjectSpace.IsNewObject(View.CurrentObject))
                View.RefreshDataSource();
        }

        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            if (ObjectSpace.IsNewObject(View.CurrentObject))
            {
                var obj = View.CurrentObject;
                var propertyInfo = obj.GetType().GetProperty(seq.Propiedad.Name);
                string NuevaSecuencia = "";

                XPObjectSpace ObjectSpaceSecuencia = (XPObjectSpace)Application.CreateObjectSpace();
                try
                {
                    //Iniciar ObjectSpace de Proceso para la secuencia
                    Secuencia CurrentObjectSecuencia = ObjectSpaceSecuencia.GetObjectsQuery<Secuencia>(true).Where(s => s.ObjetoAplicar == View.CurrentObject.GetType()).FirstOrDefault();

                    //Generar y persistir la secuencia
                    CurrentObjectSecuencia.Codigo1 = Codigo1;
                    CurrentObjectSecuencia.Codigo2 = Codigo2;
                    CurrentObjectSecuencia.FechaCustom1 = FechaCustom1;
                    CurrentObjectSecuencia.FechaCustom2 = FechaCustom1;

                    NuevaSecuencia = CurrentObjectSecuencia.GetSecuencia();
                    ObjectSpaceSecuencia.CommitChanges();

                    //Asignar al campo la nueva secuencia
                    propertyInfo.SetValue(obj, NuevaSecuencia);
                }
                catch
                {
                    // Deshabilitar mensaje de Rollback
                    Frame.GetController<ModificationsController>()
                        .ModificationsHandlingMode = ModificationsHandlingMode.AutoRollback;

                    ObjectSpaceSecuencia.Rollback();

                    Frame.GetController<ModificationsController>()
                        .ModificationsHandlingMode = ModificationsHandlingMode.Confirmation;
                    //General.MostrarMensaje(this, string.Format("Error grave en al generar la secuencia: {0}", ex.Message), InformationType.Error);
                }
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

        }
        protected override void OnDeactivated()
        {
            if (DesactivarOnSaving)
            {
                ObjectSpace.ObjectSaving -= ObjectSpace_ObjectSaving;
                ObjectSpace.ObjectSaved -= ObjectSpace_ObjectSaved;
            }
            if (appearanceController != null)
            {
                appearanceController.CustomApplyAppearance -= AppearanceController_CustomApplyAppearance;
            }
            base.OnDeactivated();
        }
    }
}
