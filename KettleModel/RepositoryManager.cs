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
