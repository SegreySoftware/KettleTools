using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KettleModel
{
    public class TransJobEntry : JobEntry, ItemRef
    {
        public TransJobEntry(Job job, XElement element)
            : base(job, element)
        {

        }

        public override void PostLoad()
        {
            base.PostLoad();
            Job.Parent.Repository.AllTransformations.TryGetValue(RefKey, out _refTrans);
        }

        public string RefName
        {
            get { return GetValue(Element, "transname"); }
            set { Element.SetElementValue("transname", value); }

        }

        public string Directory
        {
            get { return GetValue(Element, "directory"); }
            set { Element.SetElementValue("directory", value); }

        }
        public string RefKey { get { return KettleItem.MakeKey(Directory, RefName); } }

        public KettleItem RefItem { get { return _refTrans; } }
        public Transformation RefTrans { get { return _refTrans; } }

        public void UpdateReference()
        {
            RefName = RefItem.Name;
            Directory = RefItem.Directory;
            Job.Save();
        }

        private Transformation _refTrans;

    }
}
