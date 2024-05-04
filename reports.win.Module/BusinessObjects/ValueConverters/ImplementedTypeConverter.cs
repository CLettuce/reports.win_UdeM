using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace reports.win.Module.BusinessObjects.ValueConverters
{
    public class ImplementedTypeConverter<T> : LocalizedClassInfoTypeConverter
    {
        public override List<Type> GetSourceCollection(ITypeDescriptorContext context) => (from t in Assembly.GetExecutingAssembly().GetTypes()
                                                                                           where t.IsClass && t.Namespace == "reports.win.Module.BusinessObjects" && typeof(T).IsAssignableFrom(t)
                                                                                           select t).ToList();
    }
}
