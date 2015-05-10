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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace KettleModel
{
    public class Folder
    {
        public const char FolderSeparator = '/';

        public Folder(Folder parent, string name)
        {
            Name = name;
            Parent = parent;
            Repository = parent == null ? (Repository) this : parent.Repository;
            Reset();
        }

        protected virtual void Reset()
        {
            Folders = new List<Folder>();
            Jobs = new List<Job>();
            Transformations = new List<Transformation>();
        }

        public string Name { get; private set; }
        public Folder Parent { get; private set; }
        public Repository Repository { get; private set; }

        public List<Folder> Folders { get; private set; }
        public List<Job> Jobs { get; private set; } 
        public List<Transformation> Transformations { get; private set; }

        public string RepositoryPath
        {
            get
            {
                if (Parent == null) return FolderSeparator.ToString();
                if (Parent.Parent == null) return Name;
                return Parent.RepositoryPath + FolderSeparator + Name;
            }
        }

        public string FilesystemPath
        {
            get { return Parent == null ? Repository.Path :  Path.Combine(Repository.Path, RepositoryPath.Replace(FolderSeparator, Path.DirectorySeparatorChar)); }
        }

        public void Scan()
        {
            ScanChildren();
            ScanJobs();
            ScanTransformations();
        }

        private void ScanChildren()
        {
            foreach (var dir in Directory.GetDirectories(FilesystemPath))
            {
                var name = Path.GetFileName(dir);
                if (name.StartsWith(".")) {continue;}
                var folder = new Folder(this, name);
                Folders.Add(folder);
                Repository.AllFolders.Add(folder.RepositoryPath, folder);
                folder.Scan();
            }
        }

        private void ScanJobs()
        {
            foreach (var path in Directory.GetFiles(FilesystemPath, "*." + KettleItem.JobFileExtension))
            {
                if (!path.EndsWith("#"))
                {
                    var name = Path.GetFileName(path);
                    var job = new Job(this, path);
                    Jobs.Add(job);
                    Repository.AllJobs.Add(job.Key, job);
                }
            }
        }

        private void ScanTransformations()
        {
            foreach (var path in Directory.GetFiles(FilesystemPath, "*." + KettleItem.TransformationFileExtension))
            {
                if (!path.EndsWith("#"))
                {
                    var name = Path.GetFileName(path);
                    var transformation = new Transformation(this, path);
                    Transformations.Add(transformation);
                    Repository.AllTransformations.Add(transformation.Key, transformation);
                }
            }
        }
    }
}
