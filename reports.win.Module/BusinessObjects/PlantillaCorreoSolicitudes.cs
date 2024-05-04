using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace reports.win.Module.BusinessObjects
{
    [XafDisplayName("Plantillas de Correo")]
    [OptimisticLocking(OptimisticLockingBehavior.NoLocking)]
    public class PlantillaCorreoSolicitudes : RichTextMailMergeData
    {
        public PlantillaCorreoSolicitudes(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
        private object objetoCombinar;
        private string asunto;

        [Size(100)]
        public string Asunto { get => asunto; set => SetPropertyValue(nameof(Asunto), ref asunto, value); }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInReports(false)]
        [VisibleInLookupListView(false)]
        public object ObjetoCombinar { get => objetoCombinar; set => SetPropertyValue(nameof(ObjetoCombinar), ref objetoCombinar, value); }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInReports(false)]
        [VisibleInLookupListView(false)]
        public string ObjetoResultado
        {
            get
            {
                //  var Currentobject = this;
                if (ObjetoCombinar is null) return "";

                Stream streamResult = new MemoryStream();
                string result = "";

                List<object> ListaCombinar = new List<object>();
                ListaCombinar.Add(ObjetoCombinar);

                using (RichEditDocumentServer server = new RichEditDocumentServer())
                {
                    server.LoadDocument(Template);
                    server.Options.MailMerge.DataSource = ListaCombinar;

                    server.MailMerge(streamResult, DocumentFormat.Html);
                    streamResult.Position = 0;
                    StreamReader reader = new StreamReader(streamResult);
                    result = reader.ReadToEnd();
                }
                return result;
            }
        }
    }
}
