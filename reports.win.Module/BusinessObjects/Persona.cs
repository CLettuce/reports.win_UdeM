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
    [DefaultProperty("NombreCompleto")]
    public class Persona : BaseObject
    {
        public Persona(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            FechaRegistro = DateTime.Now;
        }


        Usuario usuario;
        CatalogoSexo sexo;
        DateTime fechaRegistro;
        string apellido2;
        string apellido1;
        string nombre1;
        string nombre2;

        public string NombreCompleto => string.Join(" ", new string[] { Nombre1?.Trim(), Nombre2?.Trim(), Apellido1?.Trim(), Apellido2?.Trim() }).Trim();

        [Size(50)]
        public string Nombre1 { get => nombre1; set => SetPropertyValue(nameof(Nombre1), ref nombre1, value); }

        [Size(50)]
        public string Nombre2 { get => nombre2; set => SetPropertyValue(nameof(Nombre2), ref nombre2, value); }

        [Size(50)]
        public string Apellido1 { get => apellido1; set => SetPropertyValue(nameof(Apellido1), ref apellido1, value); }

        [Size(50)]
        public string Apellido2 { get => apellido2; set => SetPropertyValue(nameof(Apellido2), ref apellido2, value); }

        public DateTime FechaRegistro { get => fechaRegistro; set => SetPropertyValue(nameof(FechaRegistro), ref fechaRegistro, value); }

        public CatalogoSexo Sexo { get => sexo; set => SetPropertyValue(nameof(Sexo), ref sexo, value); }

        [Association("Usuario-Persona")]
        public Usuario Usuario { get => usuario; set => SetPropertyValue(nameof(Usuario), ref usuario, value); }

        [Association("Persona-Solicitudes")]
        public XPCollection<Solicitudes> Solicitude { get { return GetCollection<Solicitudes>(nameof(Solicitude)); } }

        [Association("Persona-Identificaciones")]
        public XPCollection<PersonaIdentificaciones> Identificaciones { get { return GetCollection<PersonaIdentificaciones>(nameof(Identificaciones)); } }

    }
}