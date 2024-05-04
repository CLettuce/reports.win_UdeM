using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using reports.win.Module.BusinessObjects.Interfaces;
using reports.win.Module.BusinessObjects.ValueConverters;
using reports.win.Module.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace reports.win.Module.BusinessObjects
{
    [DefaultClassOptions]
    [OptimisticLocking(OptimisticLockingBehavior.NoLocking)]
    [XafDisplayName("Secuencias")]
    public class Secuencia : BaseObject, ISupportTypeProperties
    {
        public Secuencia(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Automatico = false;
            FechaCustom1 = DateTime.Now;
            FechaCustom2 = DateTime.Now;
        }

        Type objetoAplicar;
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(ImplementedTypeConverter<ISupportSecuencia>))]
        [RuleRequiredField]
        public Type ObjetoAplicar { get => objetoAplicar; set => SetPropertyValue(nameof(ObjetoAplicar), ref objetoAplicar, value); }

        StringObject propiedad;
        [RuleRequiredField]
        [ValueConverter(typeof(StringObjectToStringConverter))]
        [DataSourceProperty(nameof(PropiedadesDisponiblesTipo))]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public StringObject Propiedad { get => propiedad; set => SetPropertyValue(nameof(Propiedad), ref propiedad, value); }

        [Browsable(false)]
        public IList<StringObject> PropiedadesDisponiblesTipo => ObjetoAplicar.GetPropertiesStringObject();

        string nombre;
        [Size(50), RuleRequiredField, RuleUniqueValue]
        public string Nombre { get { return nombre; } set { SetPropertyValue("Nombre", ref nombre, value); } }

        bool bloquearCampo;
        public bool BloquearCampo { get => bloquearCampo; set => SetPropertyValue(nameof(BloquearCampo), ref bloquearCampo, value); }

        int actual;
        public int Actual { get { return actual; } set { SetPropertyValue("Actual", ref actual, value); } }

        string patron;
        [Size(100)]
        public string Patron { get { return patron; } set { SetPropertyValue("Patron", ref patron, value); } }

        string ultimaSecuencia;
        [Size(50)]
        public string UltimaSecuencia { get { return ultimaSecuencia; } set { SetPropertyValue("UltimaSecuencia", ref ultimaSecuencia, value); } }

        DateTime? ultimaActualizacion;
        public DateTime? UltimaActualizacion { get { return ultimaActualizacion; } set { SetPropertyValue("UltimaActualizacion", ref ultimaActualizacion, value); } }

        ReinicioEnum reinicio;
        public ReinicioEnum Reinicio { get { return reinicio; } set { SetPropertyValue("Reinicio", ref reinicio, value); } }

        int rellenoCeros;
        public int RellenoCeros { get { return rellenoCeros; } set { SetPropertyValue("RellenoCeros", ref rellenoCeros, value); } }

        bool automatico;
        public bool Automatico { get => automatico; set => SetPropertyValue(nameof(Automatico), ref automatico, value); }


        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInReports(false)]
        [VisibleInLookupListView(false)]
        [Size(100)]
        public string Codigo1 { get => codigo1; set => SetPropertyValue(nameof(Codigo1), ref codigo1, value); }
        string codigo1;

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInReports(false)]
        [VisibleInLookupListView(false)]
        [Size(100)]
        public string Codigo2 { get => codigo2; set => SetPropertyValue(nameof(Codigo2), ref codigo2, value); }
        string codigo2;

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInReports(false)]
        [VisibleInLookupListView(false)]
        public DateTime FechaCustom1 { get => fechaCustom1; set => SetPropertyValue(nameof(FechaCustom1), ref fechaCustom1, value); }
        DateTime fechaCustom1;

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInReports(false)]
        [VisibleInLookupListView(false)]
        public DateTime FechaCustom2 { get => fechaCustom2; set => SetPropertyValue(nameof(FechaCustom2), ref fechaCustom2, value); }
        DateTime fechaCustom2;

        [Size(50)]
        public string FormatoFechaCustom1 { get => formatoFechaCustom1; set => SetPropertyValue(nameof(FormatoFechaCustom1), ref formatoFechaCustom1, value); }
        string formatoFechaCustom1;

        [Size(50)]
        public string FormatoFechaCustom2 { get => formatoFechaCustom2; set => SetPropertyValue(nameof(FormatoFechaCustom2), ref formatoFechaCustom2, value); }
        string formatoFechaCustom2;

        public enum ReinicioEnum
        {
            SinReinicio = 0,
            ReinioPorAño = 1,
            ReinicioPorMes = 2,
            ReinicioPorDia = 3
        }

        public string GetSecuencia(DateTime? FechaLote = null)
        {
            DateTime CurrentDate = DateTime.Now;
            string NuevaSecuencia = "";

            if (FechaLote != null)
            {
                CurrentDate = FechaLote.Value;
            }

            if (UltimaActualizacion == null)
            {
                UltimaActualizacion = CurrentDate;
                //Actual = 0;
            }

            switch (Reinicio)
            {
                case ReinicioEnum.ReinicioPorDia:

                    if (CurrentDate.Day != UltimaActualizacion.Value.Day)
                    {
                        Actual = 0;
                    }
                    break;

                case ReinicioEnum.ReinicioPorMes:

                    if (CurrentDate.Month != UltimaActualizacion.Value.Month)
                    {
                        Actual = 0;
                    }
                    break;

                case ReinicioEnum.ReinioPorAño:

                    if (CurrentDate.Year != UltimaActualizacion.Value.Year)
                    {
                        Actual = 0;
                    }
                    break;
            }

            //Incrementamos el valor de la secuencia
            Actual++;

            NuevaSecuencia = Patron.Replace("{DAY}", CurrentDate.Day.ToString().PadLeft(2, '0'));
            NuevaSecuencia = NuevaSecuencia.Replace("{MONTH}", CurrentDate.Month.ToString().PadLeft(2, '0'));
            NuevaSecuencia = NuevaSecuencia.Replace("{YEAR}", CurrentDate.Year.ToString().PadLeft(4, '0'));
            NuevaSecuencia = NuevaSecuencia.Replace("{CURRENT}", Actual.ToString().PadLeft(RellenoCeros, '0'));
            NuevaSecuencia = NuevaSecuencia.Replace("{RANDOM}", Principal.RandomString(RellenoCeros));
            NuevaSecuencia = NuevaSecuencia.Replace("{CODIGO1}", Codigo1);
            NuevaSecuencia = NuevaSecuencia.Replace("{CODIGO2}", Codigo2);
            NuevaSecuencia = NuevaSecuencia.Replace("{FECHACUSTOM1}", FechaCustom1.ToString(FormatoFechaCustom1, CultureInfo.CreateSpecificCulture("es-NI")).ToUpper());
            NuevaSecuencia = NuevaSecuencia.Replace("{FECHACUSTOM2}", FechaCustom2.ToString(FormatoFechaCustom2, CultureInfo.CreateSpecificCulture("es-NI")).ToUpper());

            UltimaActualizacion = CurrentDate;
            UltimaSecuencia = NuevaSecuencia;

            Save();

            return NuevaSecuencia;
        }
    }
}