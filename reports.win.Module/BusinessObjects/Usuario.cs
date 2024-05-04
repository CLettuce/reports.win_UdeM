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
    [DefaultProperty("Username")]
    public class Usuario : BaseObject
    {
        public Usuario(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            FechaRegistro = DateTime.Now;
            Estado = EstadoUsuario.Activo;
        }


        EstadoUsuario estado;
        string passwordAgain;
        string username;
        string password;
        DateTime fechaRegistro;
        string correoElectronico;

        public DateTime FechaRegistro { get => fechaRegistro; set => SetPropertyValue(nameof(FechaRegistro), ref fechaRegistro, value); }

        public enum EstadoUsuario
        {
            Activo = 1,
            Inactivo = 0
        }

        [Size(50)]
        public string Username { get => username; set => SetPropertyValue(nameof(Username), ref username, value); }

        [NonPersistent]
        public bool UsuarioExiste { get; set; }

        [Size(100)]
        public string Password { get => password; set => SetPropertyValue(nameof(Password), ref password, value); }

        [NonPersistent]
        [Size(100)]
        public string PasswordAgain { get => passwordAgain; set => SetPropertyValue(nameof(PasswordAgain), ref passwordAgain, value); }

        public EstadoUsuario Estado { get => estado; set => SetPropertyValue(nameof(Estado), ref estado, value); }

        [Size(100)]
        public string CorreoElectronico { get => correoElectronico; set => SetPropertyValue(nameof(CorreoElectronico), ref correoElectronico, value); }
        [Association("Usuario-Persona")]
        public XPCollection<Persona> Persona => GetCollection<Persona>(nameof(Persona));

        List<string> ListaCorreos = new List<string>();
        public string StrCorreosContacto
        {
            get
            {
                ListaCorreos.AddRange(Persona.Where(c => c.Usuario.Estado == EstadoUsuario.Activo).Select(c => CorreoElectronico.Trim()).ToList());
                return ListaCorreos.Count > 0 ? string.Join(";", ListaCorreos) + ";" : "";
            }
        }
    }
}