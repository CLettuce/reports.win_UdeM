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
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using reports.win.Module.BusinessObjects;
using reports.win.Module.BusinessObjects.Interfaces;
using reports.win.Module.General;

namespace reports.win.Module.Controllers
{
    public partial class PlantillaCorreoReglasController : ViewController<DetailView>
    {

        public PlantillaCorreoReglasController()
        {
            InitializeComponent();
            TargetObjectType = typeof(ISupportCustomNotifications);
        }

        BaseObject CurrentObject;
        List<PlantillasCorreoReglasSolicitudes> ListaReglas;
        Type CurrentType;
        protected override void OnActivated()
        {
            base.OnActivated();
            CurrentObject = (BaseObject)View.CurrentObject;
            CurrentType = CurrentObject.GetType();

            //var plantillas = ObjectSpace.GetObjects<TRAMITEPlantillasCorreo>().FirstOrDefault();

            //            var c = plantillas.GetMemberValue("Template");

            //          plantillas.Reload();

            ListaReglas = ObjectSpace.GetObjectsQuery<PlantillasCorreoReglasSolicitudes>()
                .Where(l => l.DeclaringType == CurrentObject.GetType() && l.Estado == PlantillasCorreoReglasSolicitudes.EnumPlantillaCorreoReglasEstado.Activo)
                .ToList();

            ActualizarValoresIniciales();

            ObjectSpace.ObjectSaved += ObjectSpace_ObjectSaved;
        }
        private void ObjectSpace_ObjectSaved(object sender, ObjectManipulatingEventArgs e)
        {
            // Evitamos duplicidad de envios
            bool GuardandoObjectoPrincipal = e.Object.GetType() == CurrentType;

            if (!GuardandoObjectoPrincipal) return;

            foreach (PlantillasCorreoReglasSolicitudes item in ListaReglas.Where(l => l.CampoObservarInicial != l.CampoObservar))
            {
                if ((bool)CurrentObject.Evaluate(item.Criteria))
                {
                    string strListaCorreosParaCampo;
                    List<string> ListaCorreosPara = new List<string>();
                    string strListaCorreosPara = "";
                    List<string> ListaCorreosCC = new List<string>();
                    string strListaCorreosCC = "";
                    List<string> ListaCorreosCCO = new List<string>();
                    string strListaCorreosCCO = "";

                    try
                    {
                        strListaCorreosParaCampo = (string)CurrentObject.GetType().GetProperty(item.CampoCorreos.Name).GetValue(CurrentObject);
                        ListaCorreosPara.AddRange(CrearListaCorreos(strListaCorreosParaCampo));
                    }
                    catch (Exception) { }

                    ListaCorreosPara.AddRange(CrearListaCorreos(item.CorreosAdicionalesPara));
                    strListaCorreosPara = string.Join(";", ListaCorreosPara);


                    ListaCorreosCC.AddRange(CrearListaCorreos(item.CorreosCC));
                    strListaCorreosCC = string.Join(";", ListaCorreosCC);



                    ListaCorreosCCO.AddRange(CrearListaCorreos(item.CorreosCCO));
                    strListaCorreosCCO = string.Join(";", ListaCorreosCCO);
                    // Enviamos la plantilla al motor de Mail Merge
                    item.Plantilla.ObjetoCombinar = CurrentObject;
                    var resultado = item.Plantilla.ObjetoResultado;

                    try
                    {
                        if (!string.IsNullOrEmpty(strListaCorreosPara.CleanText().Replace(" ", ""))) Principal.EnviarCorreo(item.CorreoElectronico, strListaCorreosPara, item.Plantilla.Asunto, resultado, strListaCorreosCC, strListaCorreosCCO);

                        item.Plantilla.Reload();
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            ActualizarValoresIniciales();
        }

        private List<string> CrearListaCorreos(string strCorreos)
        {
            List<string> ListaResult = new List<string>();
            List<string> ListaTemp = new List<string>();

            if (strCorreos is null) return ListaResult;

            strCorreos = strCorreos.CleanText().Replace(" ", "");
            ListaTemp = strCorreos.Split(';').Where(o => !string.IsNullOrEmpty(o) && o.IsValidEmail()).ToList();

            if (ListaTemp.Count > 0) ListaResult.AddRange(ListaTemp);

            return ListaResult;
        }

        private void ActualizarValoresIniciales()
        {
            foreach (PlantillasCorreoReglasSolicitudes item in ListaReglas.Where(l => l.CampoObservar != null))
            {
                item.CampoObservarInicial = CurrentObject.GetType().GetProperty(item.CampoObservar.Name).GetValue(CurrentObject);
                item.NotificacionEnviada = false;
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

        }
        protected override void OnDeactivated()
        {
            ObjectSpace.ObjectSaved -= ObjectSpace_ObjectSaved;
            base.OnDeactivated();
        }
    }
}
