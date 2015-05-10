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
