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
