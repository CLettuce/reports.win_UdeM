using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
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
    public class Solicitudes_Reportes : Solicitudes, ISupportCustomAppearance
    {
        public Solicitudes_Reportes(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public enum EstadoSolicitud
        {
            Creado = 0,
            [XafDisplayName("Publicado")]
            Publicado = 1,
            EnRevision = 2,
            Atendido = 3,
            Rechazado = 4
        }

        EstadoSolicitud? estado;
        CatalogoUbicacion ubicacionDePeticion;
        CatalogoProfesores profesor;
        CatalogoCarrera carrera;
        PersonaIdentificaciones identificacion;
        string descripcion;
        bool correcion;
        DateTime fechaCorrecion;

        [RuleRequiredField]
        [Appearance("AppSolicitudes_ReportesEstadoDisabled", Enabled = true)]
        public EstadoSolicitud? Estado { get => estado; set => SetPropertyValue(nameof(Estado), ref estado, value); }

        [Size(200)]
        public string Descripcion { get => descripcion; set => SetPropertyValue(nameof(Descripcion), ref descripcion, value); }

        public CatalogoCarrera Carrera { get => carrera; set => SetPropertyValue(nameof(Carrera), ref carrera, value); }

        public CatalogoProfesores Profesor { get => profesor; set => SetPropertyValue(nameof(Profesor), ref profesor, value); }

        public CatalogoUbicacion UbicacionDePeticion { get => ubicacionDePeticion; set => SetPropertyValue(nameof(UbicacionDePeticion), ref ubicacionDePeticion, value); }

        public PersonaIdentificaciones Identificacion { get => identificacion; set => SetPropertyValue(nameof(Identificacion), ref identificacion, value); }

        public bool CorrecionUsuario { get => correcion; set => SetPropertyValue(nameof(CorrecionUsuario), ref correcion, value); }
        public DateTime FechaCorrecion { get => fechaCorrecion; set => SetPropertyValue(nameof(FechaCorrecion), ref fechaCorrecion, value); }
    }
}