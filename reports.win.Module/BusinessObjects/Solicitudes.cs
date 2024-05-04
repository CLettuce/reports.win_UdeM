using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using reports.win.Module.BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace reports.win.Module.BusinessObjects
{
    [DefaultClassOptions]

    public class Solicitudes : BaseObject, ISupportCustomAppearance, ISupportCustomNotifications, ISupportSecuencia
    {

        public Solicitudes(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            FechaRegistro = DateTime.Now;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        Persona persona;
        DateTime fechaRegistro;


        public DateTime FechaRegistro { get => fechaRegistro; set => SetPropertyValue(nameof(FechaRegistro), ref fechaRegistro, value); }

        private string notasCorreo;
        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInReports(false)]
        [VisibleInLookupListView(false)]
        public string NotasCorreo { get => notasCorreo; set => SetPropertyValue(nameof(NotasCorreo), ref notasCorreo, value); }

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string StrCorreosContacto => Persona.Usuario.StrCorreosContacto;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        //[Delayed]

        public string NombreCompletoPersona => Persona is null ? "N/A" : Persona.NombreCompleto;

        [Association("Persona-Solicitudes")]
        public Persona Persona { get => persona; set => SetPropertyValue(nameof(Persona), ref persona, value); }

        [Association("Solicitudes-SolicitudesAuditoria")]
        public XPCollection<Solicitudes_Auditoria> SolicitudesAuditoria { get { return GetCollection<Solicitudes_Auditoria>(nameof(SolicitudesAuditoria)); } }

    }
}