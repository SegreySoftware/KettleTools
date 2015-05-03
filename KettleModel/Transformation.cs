using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KettleModel
{
    public class Transformation : KettleItem
    {
        public Transformation(Folder parent, string filename) : base(parent, filename)
        {
        }

        public override string FileExtension
        {
            get { return TransformationFileExtension; }
        }

        public override string TypeName
        {
            get { return "trans"; }
        }

        protected override XElement InfoElement
        {
            get { return RootElement.Element("info"); }
        }

        protected override List<XElement> EntryElements { get { return RootElement.Elements("step").ToList(); } }

        protected override Type DefaultEntryType { get { return typeof(TransformationStep); } }
    }
}
