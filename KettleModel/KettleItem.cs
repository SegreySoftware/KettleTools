using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KettleModel
{
    public abstract class KettleItem
    {
        public static Dictionary<string, Type> EntryTypes = new Dictionary<string, Type>
        {
            {"JOB", typeof(JobJobEntry)},
            {"TRANS", typeof(TransJobEntry)}
        };

        public const string JobFileExtension = "kjb";
        public const string TransformationFileExtension = "ktr";
        public const string ConnectionFileExtension = "kdb";
        
        public abstract string FileExtension { get; }
        public abstract string TypeName { get; }

        public string Name
        {
            get { return GetValue(InfoElement, "name"); }
            set { InfoElement.SetElementValue("name", value); }
        }

        public string Directory
        {
            get { return GetValue(InfoElement, "directory"); }
            set
            {
                if (value != Directory)
                {
                    InfoElement.SetElementValue("directory", value);
                }
            }
        }
        public string UniqueName {get { return Directory + Folder.FolderSeparator + Name; }}
        public string Url { get { return Repository.Url + "/" + TypeName + "/" + Key; }}
        public string Path { get { return System.IO.Path.Combine(Directory, Name + "." + FileExtension); }}
        public string FilesystemPath {get
        {
            //return System.IO.Path.Combine(Parent.FilesystemPath, Name + "." + FileExtension);
            return Repository.Resolve(Path);
        }}

        public string Key
        {
            get { return MakeKey(Directory, Name); }
        }

        public static string MakeKey(string directory, string name)
        {
            return (directory + name).Replace("/", "").Replace("\\", "").Replace(" ", "");
        }

        public string Name2 { get { return GetValue(InfoElement, "name"); } }
        public string Name3 { get { return GetValue(InfoElement, "name"); } }

        public Folder Parent { get; private set; }
        public Repository Repository { get { return Parent.Repository; }}
        public string Filename { get; private set; }
        public bool LoadError { get; private set; }
        public string ErrorMessage { get; private set; }

        public bool Unreferenced
        {
            get { return ReferncedByItems.Count == 0; }
        }

        public List<KettleItem> ReferencedItems
        {
            get
            {
                return
                    Entries.OfType<ItemRef>()
                        .Where(e => e.RefItem != null)
                        .Select(e => e.RefItem)
                        .Distinct()
                        .OrderBy(j => j.Name)
                        .ToList();
            }
        }

        public List<KettleItem> ReferncedByItems
        {
            get
            {
                return
                    Repository.AllJobs.Values.SelectMany(j => j.Entries)
                        .OfType<ItemRef>()
                        .Where(entry => entry.RefItem == this)
                        .Select(entry => entry.Item)
                        .Distinct()
                        .OrderBy(j => j.Name)
                        .ToList();
            }
        }



        public List<KettleEntry> Entries { get; set; }

        public KettleItem(Folder parent, string filename)
        {
            Parent = parent;
            Filename = filename;
            try
            {
                LoadError = false;
                Document = XDocument.Load(Filename);
                Load();
            }
            catch (Exception e)
            {
                LoadError = true;
                ErrorMessage = e.ToString();
            }
        }

        public void Move(string directory, string name)
        {
            var newPath = System.IO.Path.Combine(directory, name + "." + FileExtension);
            if (Path == newPath)
            {
                return;
            }
            newPath = Repository.Resolve(newPath);
            var dir = Repository.Resolve(directory);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            // First move the file. If this fails, we still have a consistent repository state.
            File.Move(FilesystemPath, newPath);

            // Now update the content of the file
            Directory = directory;
            Name = name;
            Save();

            // Refactor items referencing this item.
            foreach (var item in ReferncedByItems)
            {
                item.UpdateReference(this);
            }
        }

        public void Save()
        {
            Document.Save(Repository.Resolve(FilesystemPath));
        }

        protected void UpdateReference(KettleItem referencedItem)
        {
            foreach (var entry in Entries.OfType<ItemRef>().Where(e => e.RefItem == referencedItem))
            {
                entry.UpdateReference();
            }
        }

        protected abstract List<XElement> EntryElements { get; }

        protected abstract Type DefaultEntryType { get; }

        protected void Load()
        {
            LoadEntries();
        }


        protected void LoadEntries()
        {
            Entries = new List<KettleEntry>();
            foreach (var element in EntryElements)
            {
                CreateEntry(element);
            }
        }

        private void CreateEntry(XElement element)
        {
            Type type = null;
            string typeName = GetValue(element, "type");
            if (!EntryTypes.TryGetValue(typeName, out type))
            {
                type = DefaultEntryType;
            }
            var constructor = type.GetConstructor(new[] { this.GetType(), typeof(XElement) });
            var entry = (KettleEntry)constructor.Invoke(new object[] { this, element });
            Entries.Add(entry);
        }

        protected string GetValue(XElement parent, string name)
        {
            if (parent == null) return null;
            var element = parent.Element(name);
            if (element == null) return null;
            return element.Value;
        }

        protected XDocument Document { get; set; }
        protected XElement RootElement { get { return Document.Root; } }
        protected virtual XElement InfoElement { get { return RootElement; } }
    }
}
