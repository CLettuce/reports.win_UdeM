using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using reports.win.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reports.win.Module.General
{
    public class EsUsuarioOperador : ICustomFunctionOperator
    {
        public string Name => "EsUsuarioOperador";

        public object Evaluate(params object[] operands)
        {
            return ((ApplicationUser)(SecuritySystem.CurrentUser)).Tipousuarios == ApplicationUser.EnumTipoUsuarios.USUARIOOPERADOR;
        }

        public Type ResultType(params Type[] operands)
        {
            return typeof(bool);
        }
    }
}
