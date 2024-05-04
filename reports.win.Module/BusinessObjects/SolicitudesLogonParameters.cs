using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reports.win.Module.BusinessObjects
{
    public class SolicitudesLogonParameters : AuthenticationStandardLogonParameters
    {
        public enum EnumConexionDB
        {
            [XafDisplayName("Producción")]
            Produccion,
            [XafDisplayName("Desarrollo")]
            Desarrollo
        }

        public EnumConexionDB ConexionDB { get; set; }
    }
}
