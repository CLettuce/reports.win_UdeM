using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MailBee.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace reports.win.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("CorreoElectronico")]
    public class NotificacionesCorreos : BaseObject
    {
        public NotificacionesCorreos(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
        LinkedResource imagen;
        string password;
        string correoElectronicoMostrar;
        int? puerto;
        SecurityProtocol? protocoloSeguridad;
        SslStartupMode? modoSSL;
        string servidor;
        string correoElectronico;

        [Size(100)]
        [RuleRequiredField]
        public string CorreoElectronico { get => correoElectronico; set => SetPropertyValue(nameof(CorreoElectronico), ref correoElectronico, value); }

        [Size(100)]
        [RuleRequiredField]
        public string CorreoElectronicoMostrar { get => correoElectronicoMostrar; set => SetPropertyValue(nameof(CorreoElectronicoMostrar), ref correoElectronicoMostrar, value); }

        [Size(50)]
        [RuleRequiredField]
        public string Servidor { get => servidor; set => SetPropertyValue(nameof(Servidor), ref servidor, value); }


        [RuleRequiredField]
        public SslStartupMode? ModoSSL { get => modoSSL; set => SetPropertyValue(nameof(ModoSSL), ref modoSSL, value); }

        [RuleRequiredField]
        public SecurityProtocol? ProtocoloSeguridad { get => protocoloSeguridad; set => SetPropertyValue(nameof(ProtocoloSeguridad), ref protocoloSeguridad, value); }

        [RuleRequiredField]
        public int? Puerto { get => puerto; set => SetPropertyValue(nameof(Puerto), ref puerto, value); }


        [Size(100)]
        [RuleRequiredField]
        public string Password { get => password; set => SetPropertyValue(nameof(Password), ref password, value); }


        public LinkedResource Imagenes { get => imagen; set => SetPropertyValue(nameof(imagen), ref imagen, value); }
    }
}