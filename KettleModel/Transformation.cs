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
