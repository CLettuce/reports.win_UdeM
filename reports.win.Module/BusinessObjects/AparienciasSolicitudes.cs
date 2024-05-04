using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace reports.win.Module.BusinessObjects
{
    [DefaultClassOptions]

    public class AparienciasSolicitudes : BaseObject, IAppearanceRuleProperties
    {
        public AparienciasSolicitudes(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            AppearanceItemType = "ViewItem";
            Context = "Any";
            Priority = 0;
            EstadoApariencia = true;
        }

        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(ImplementedTypeConverter<ISupportCustomAppearance>))]
        [RuleRequiredField]
        [XafDisplayName("Tipo de Objeto")]
        public Type DeclaringType { get => declaringType; set => SetPropertyValue(nameof(DeclaringType), ref declaringType, value); }

        [Category("Behavior")]
        [CriteriaOptions("DeclaringType")]
        [EditorAlias(EditorAliases.CriteriaPropertyEditor)]
        [Size(1000)]
        public string Criteria { get => criteria; set => SetPropertyValue(nameof(Criteria), ref criteria, value); }

        [Category("Behavior")]
        [TypeConverter(typeof(AppearanceContextTypeConverter))]
        [RuleRequiredField]
        [ToolTip("Any / ListView / DetailView")]
        public string Context { get => context; set => SetPropertyValue(nameof(Context), ref context, value); }

        public int Priority { get => priority; set => SetPropertyValue(nameof(Priority), ref priority, value); }

        public FontStyle? FontStyle { get => fontStyle; set => SetPropertyValue(nameof(FontStyle), ref fontStyle, value); }

        [ValueConverter(typeof(ColorValueConverter))]
        public Color? FontColor { get => fontColor; set => SetPropertyValue(nameof(FontColor), ref fontColor, value); }

        [ValueConverter(typeof(ColorValueConverter))]
        public Color? BackColor { get => backColor; set => SetPropertyValue(nameof(BackColor), ref backColor, value); }

        public ViewItemVisibility? Visibility { get => visibility; set => SetPropertyValue(nameof(Visibility), ref visibility, value); }

        public bool? Enabled { get => enabled; set => SetPropertyValue(nameof(Enabled), ref enabled, value); }

        [Category("Behavior")]
        [DataSourceProperty("MethodNames")]
        [Description("Specifies the name of the business class method used to determine whether the Conditional Appearance rule is currently active.")]
        public string Method { get => method; set => SetPropertyValue(nameof(Method), ref method, value); }

        string usuarioExclusion;

        string descripcion;
        bool estadoApariencia;
        bool? enabled;
        ViewItemVisibility? visibility;
        Color? backColor;
        Color? fontColor;
        FontStyle? fontStyle;
        int priority;
        Type declaringType;
        string context;
        string method;
        string criteria;
        string appearanceItemType;
        string targetItems;

        [Category("Data")]
        [ToolTip("Campos a afectar *=Todos / *;Campo=Todos menos Campo / Campo=Aplicado solo a campo")]
        [RuleRequiredField]
        [Size(1000)]
        public string TargetItems { get => targetItems; set => SetPropertyValue(nameof(TargetItems), ref targetItems, value); }

        [Category("Behavior")]
        [ToolTip("ViewItem / Action")]
        [TypeConverter(typeof(AppearanceItemTypeConverter))]
        [RuleRequiredField]
        public string AppearanceItemType { get => appearanceItemType; set => SetPropertyValue(nameof(AppearanceItemType), ref appearanceItemType, value); }

        public bool EstadoApariencia { get => estadoApariencia; set => SetPropertyValue(nameof(EstadoApariencia), ref estadoApariencia, value); }

        [Size(1000)]
        public string Descripcion { get => descripcion; set => SetPropertyValue(nameof(Descripcion), ref descripcion, value); }

        [Size(1000)]
        public string UsuarioExclusion { get => usuarioExclusion; set => SetPropertyValue(nameof(UsuarioExclusion), ref usuarioExclusion, value); }
    }
}