using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KettleModel
{
    public class KettleEntry
    {
        public KettleItem Item { get; private set; }
        public XElement Element { get; private set; }

        public string Name { get { return GetValue(Element, "name"); } }
        public string TypeName { get { return GetValue(Element, "type"); } }
        public SpecificationMethod SpecificationMethod { get { return GetValue(Element, "specification_method") == "rep_name" ? SpecificationMethod.Repository : SpecificationMethod.Other; } }


        public KettleEntry(KettleItem item, XElement element)
        {
            Item = item;
            Element = element;
        }

        public virtual void Load()
        {
            
        }


        public virtual void PostLoad()
        {}

        protected string GetValue(XElement parent, string name)
        {
            if (parent == null) return null;
            var element = parent.Element(name);
            if (element == null) return null;
            return element.Value;
        }

        protected long GetLongValue(XElement element, string name)
        {
            var str = GetValue(element, name);
            long value = 0;
            if (str != null)
            {
                Int64.TryParse(str, out value);
            }
            return value;
        }
    }
}
