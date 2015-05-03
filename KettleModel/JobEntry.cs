using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KettleModel
{
    public class JobEntry : KettleEntry
    {

        public Job Job { get; private set; }

        public JobEntry(Job job, XElement element) : base(job, element)
        {
            Job = job;
        }

 
    }
}
