using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization.Configuration;

namespace KettleModel
{
    public class Job : KettleItem
    {

        public Job(Folder parent, string filename) : base(parent, filename) {}

        public override string FileExtension { get { return JobFileExtension; } }

        public override string TypeName { get { return "job"; } }

        protected override List<XElement> EntryElements {get { return RootElement.Element("entries").Elements("entry").ToList(); }} 

        protected override Type DefaultEntryType { get { return typeof (JobEntry); }}
   
    }
}
