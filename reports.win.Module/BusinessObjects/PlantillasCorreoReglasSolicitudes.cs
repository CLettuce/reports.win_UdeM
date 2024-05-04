using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
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
using System.Linq;
using System.Text;

namespace reports.win.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Glyph_Mail")]
    [XafDisplayName("Reglas de Notificación por Correo")]
    [OptimisticLocking(OptimisticLockingBehavior.NoLocking)]

    public class PlantillasCorreoReglasSolicitudes : BaseObject
    {
        public PlantillasCorreoReglasSolicitudes(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NotificacionEnviada = false;

        }
        public enum EnumPlantillaCorreoReglasEstado
        {
            Activo = 1,
            Inactivo = 0
        }

        bool notificacionEnviada;
        NotificacionesCorreos correoElectronico;
        object campoObservarInicial;
        string correosCCO;
        string correosCC;
        string correosAdicionalesPara;
        StringObject campoCorreos;
        EnumPlantillaCorreoReglasEstado estado;
        PlantillaCorreoSolicitudes plantilla;
        StringObject campoObservar;
        string nombre;
        Type declaringType;
        string criteria;


        [Size(500)]
        [RuleRequiredField]
        public string Nombre { get => nombre; set => SetPropertyValue(nameof(Nombre), ref nombre, value); }

        [Category("Comportamiento")]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(ImplementedTypeConverter<ISupportCustomNotifications>))]
        [RuleRequiredField]
        [XafDisplayName("Tipo de Objeto")]
        public Type DeclaringType { get => declaringType; set => SetPropertyValue(nameof(DeclaringType), ref declaringType, value); }

        [Category("Comportamiento")]
        [CriteriaOptions("DeclaringType")]
        [EditorAlias(EditorAliases.CriteriaPropertyEditor)]
        [RuleRequiredField]
        [Size(SizeAttribute.Unlimited)]
        public string Criteria { get => criteria; set => SetPropertyValue(nameof(Criteria), ref criteria, value); }

        [ValueConverter(typeof(StringObjectToStringConverter))]
        [DataSourceProperty(nameof(PropiedadesDisponiblesTipo))]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public StringObject CampoObservar { get => campoObservar; set => SetPropertyValue(nameof(CampoObservar), ref campoObservar, value); }

        [Browsable(false)]
        public IList<StringObject> PropiedadesDisponiblesTipoCorreos => DeclaringType.GetPropertiesStringObject<string>();

        [Browsable(false)]
        public IList<StringObject> PropiedadesDisponiblesTipo => DeclaringType.GetPropertiesStringObject();

        [RuleRequiredField]
        [DataSourceCriteria("DataType == '@This.DeclaringType'")]
        public PlantillaCorreoSolicitudes Plantilla { get => plantilla; set => SetPropertyValue(nameof(Plantilla), ref plantilla, value); }


        public EnumPlantillaCorreoReglasEstado Estado { get => estado; set => SetPropertyValue(nameof(Estado), ref estado, value); }

        [ValueConverter(typeof(StringObjectToStringConverter))]
        [DataSourceProperty(nameof(PropiedadesDisponiblesTipoCorreos))]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public StringObject CampoCorreos { get => campoCorreos; set => SetPropertyValue(nameof(CampoCorreos), ref campoCorreos, value); }

        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Correos Adicionales")]
        public string CorreosAdicionalesPara { get => correosAdicionalesPara; set => SetPropertyValue(nameof(CorreosAdicionalesPara), ref correosAdicionalesPara, value); }

        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Correos CC")]
        public string CorreosCC { get => correosCC; set => SetPropertyValue(nameof(CorreosCC), ref correosCC, value); }

        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Correos CCO")]
        public string CorreosCCO { get => correosCCO; set => SetPropertyValue(nameof(CorreosCCO), ref correosCCO, value); }

        public object CampoObservarInicial;

        public bool NotificacionEnviada;


        [RuleRequiredField]
        public NotificacionesCorreos CorreoElectronico { get => correoElectronico; set => SetPropertyValue(nameof(CorreoElectronico), ref correoElectronico, value); }

    }
}