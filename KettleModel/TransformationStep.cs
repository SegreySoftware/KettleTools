using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KettleModel
{
    public class TransformationStep : KettleEntry
    {
         public Transformation Transformation { get; private set; }

         public TransformationStep(Transformation transformation, XElement element)
             : base(transformation, element)
         {
             Transformation = transformation;
         }

    }
}
