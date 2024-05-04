using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reports.win.Module.BusinessObjects.NonPersistent
{
    [DomainComponent]
    public class clsParameterNotas : NonPersistentBaseObject
    {
        private String notas;

        public String Notas
        {
            get { return notas; }
            set { SetPropertyValue(ref notas, value); }
        }

    }
}
