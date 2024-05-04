using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace reports.win.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("NumeroIdentificacion")]
    public class PersonaIdentificaciones : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public PersonaIdentificaciones(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Activo = true;

        }

        Persona persona;
        bool activo;
        string numeroIdentificacion;
        TipoIdentificaciones tipo;

        public TipoIdentificaciones Tipo { get => tipo; set => SetPropertyValue(nameof(Tipo), ref tipo, value); }

        [Size(50)]
        public string NumeroIdentificacion { get => numeroIdentificacion; set => SetPropertyValue(nameof(NumeroIdentificacion), ref numeroIdentificacion, value); }

        public bool Activo { get => activo; set => SetPropertyValue(nameof(Activo), ref activo, value); }


        [Association("Persona-Identificaciones")]
        public Persona Persona { get => persona; set => SetPropertyValue(nameof(Persona), ref persona, value); }
    }
}