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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KettleModel
{
    public class Repository : Folder
    {
        public Repository() : this("root")
        {
        }

        public Repository(string name) : base(null, name)
        {
            Reset();
        }

        protected override void Reset()
        {
            AllJobs = new Dictionary<string, Job>();
            AllTransformations = new Dictionary<string, Transformation>();
            AllFolders = new Dictionary<string, Folder>();
            base.Reset();
        }

        public string Path { get; private set; }
        public string Url {get { return "/repo/" + Name; }}
        public Dictionary<string, Job> AllJobs { get; private set; } 
        public Dictionary<string, Transformation> AllTransformations { get; private set; }
        public Dictionary<string, Folder> AllFolders { get; private set; }


        public string Resolve(string path)
        {
            return System.IO.Path.Combine(Path, path.Replace('/', System.IO.Path.DirectorySeparatorChar).TrimStart(new[] {System.IO.Path.DirectorySeparatorChar}));
        }

        public void Load(string path)
        {
            Path = path;
            Load();
        }

        public void Load()
        {
            AllFolders.Add(RepositoryPath, this);
            Scan();
            foreach (var job in AllJobs.Values)
            {
                foreach (var entry in job.Entries)
                {
                    entry.PostLoad();
                }
            }
        }

        public void Reload()
        {
            Reset();
            Load();
        }
    }
}
