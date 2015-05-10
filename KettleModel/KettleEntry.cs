//
// Segrey Software licenses this file to you under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with 
// the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
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
