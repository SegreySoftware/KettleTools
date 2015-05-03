using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KettleModel
{
    public interface ItemRef
    {
        KettleItem Item { get; }
        string RefName { get ; }
        string Directory { get; }
        string RefKey { get; }

        KettleItem RefItem { get; }
        void UpdateReference();
    }
}
