using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp;
using MailBee.Mime;
using MailBee.SmtpMail;
using reports.win.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace reports.win.Module.General
{
    public static class Principal
    {
        public static ApplicationUser TipoUsuario;
        public static SolicitudesLogonParameters.EnumConexionDB DB_Actual;
        public static void MostrarMensaje(XafApplication Application, string Mensaje, InformationType TipoMensaje)
        {
            MessageOptions options = new MessageOptions();
            options.Type = TipoMensaje;
            options.Win.Caption = "Solicitudes UdeM";
            options.Win.Type = WinMessageType.Flyout;
            options.Message = Mensaje;

            options.Duration = 2000;
            options.Web.Position = InformationPosition.Right;

            Application.ShowViewStrategy.ShowMessage(options);
        }

        public static string CleanText(this string value)
        {
            value = value.Replace("\t", "").Replace("\r", "").Replace("\n", "").Trim();

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            value = regex.Replace(value, " ");

            return value;
        }
        //NotificacionesCorreos Sender
        public static void EnviarCorreo(NotificacionesCorreos Sender, string Destinatario, string Asunto, string Mensaje, string DestCC = "", string DestCCO = "")
        {
            MailBee.Global.LicenseKey = "MN120-43CE31BA89419EEADC5EB0B72C4E-A8FE";

            SmtpServer server = new SmtpServer(Sender.Servidor, Sender.CorreoElectronico, Sender.Password);
            server.SslMode = Sender.ModoSSL.HasValue ? Sender.ModoSSL.Value : MailBee.Security.SslStartupMode.Manual; // Manejo de valores nulos
            server.SslProtocol = Sender.ProtocoloSeguridad.HasValue ? Sender.ProtocoloSeguridad.Value : MailBee.Security.SecurityProtocol.Auto; // Manejo de valores nulos
            server.Port = Sender.Puerto.HasValue ? Sender.Puerto.Value : 0; // Manejo de valores nulos

            Smtp mailer = new Smtp();
            mailer.SmtpServers.Add(server);
            mailer.Message.From = new EmailAddress("carlos.rodlopez@gmail.com", "Carlos Lopez");
            mailer.Message.To.AsString = Destinatario;
            mailer.Message.Cc.AsString = DestCC;
            mailer.Message.Bcc.AsString = DestCCO;

            mailer.Message.Subject = Asunto;
            mailer.Message.BodyHtmlText = Mensaje;

            mailer.Send();
        }


        public static IList<StringObject> GetPropertiesStringObject(this Type DeclaringType)
        {
            return DeclaringType.GetPropertiesStringObject<object>();
        }
        public static IList<StringObject> GetPropertiesStringObject<T>(this Type DeclaringType)
        {
            List<StringObject> result = new List<StringObject>();
            DeclaringType?.GetProperties().Where(o => o.PropertyType.IsSubclassOf(typeof(T)) || o.PropertyType == typeof(T)).ToList().ForEach(c => {
                result.Add(new StringObject(c.Name));
            });
            return result;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static bool IsValidEmail(this string correo)
        {
            try
            {
                MailAddress m = new MailAddress(correo);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
