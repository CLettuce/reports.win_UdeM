using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
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

    public class Solicitudes_Auditoria : BaseObject
    {
        public Solicitudes_Auditoria(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Fecha = DateTime.Now;
            UsuarioAud = SecuritySystem.CurrentUserName;
        }


        Solicitudes solicitud;
        string usuarioAud;
        DateTime fecha;
        Solicitudes_Reportes.EstadoSolicitud estadoNuevo;
        Solicitudes_Reportes.EstadoSolicitud estadoAnterior;
        string nota;

        public DateTime Fecha { get => fecha; set => SetPropertyValue(nameof(Fecha), ref fecha, value); }

        [Size(50)]
        public string UsuarioAud { get => usuarioAud; set => SetPropertyValue(nameof(UsuarioAud), ref usuarioAud, value); }

        public Solicitudes_Reportes.EstadoSolicitud EstadoAnterior { get => estadoAnterior; set => SetPropertyValue(nameof(EstadoAnterior), ref estadoAnterior, value); }

        public Solicitudes_Reportes.EstadoSolicitud EstadoNuevo { get => estadoNuevo; set => SetPropertyValue(nameof(EstadoNuevo), ref estadoNuevo, value); }

        [Size(SizeAttribute.Unlimited)]
        public string Nota { get => nota; set => SetPropertyValue(nameof(Nota), ref nota, value); }


        [Association("Solicitudes-SolicitudesAuditoria")]
        public Solicitudes Solicitud { get => solicitud; set => SetPropertyValue(nameof(Solicitud), ref solicitud, value); }
    }
}