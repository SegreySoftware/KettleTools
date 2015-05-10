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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using KettleModel.Properties;

namespace KettleModel
{
    public class RepositoryManager
    {
        private static RepositoryManager _instance;
        public static RepositoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RepositoryManager();
                }
                return _instance;
            }
        }

        private Dictionary<string, Repository> Repositories { get; set; }
        private Dictionary<string,string> RepositoryPaths { get; set; }

        public RepositoryManager()
        {
            Repositories = new Dictionary<string, Repository>();
            RepositoryPaths = new Dictionary<string, string>();
            foreach (var str in Settings.Repositories)
            {
                var x = str.Split('|');
                RepositoryPaths.Add(x[0], x[1]);
            }
        }

        public Repository Get(string name)
        {
            Repository rep = null;
            if (!Repositories.TryGetValue(name, out rep))
            {
                rep = new Repository(name);
                rep.Load(RepositoryPaths[name]);
                Repositories.Add(name, rep);
            }
            return rep;
        }

        public List<string> GetNames()
        {
            return RepositoryPaths.Keys.OrderBy(k => k).ToList();
        } 

        private static Settings Settings
        {
            get { return Properties.Settings.Default; }
        }
    }
}
