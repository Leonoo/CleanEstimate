using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    public class Haeufigkeit
    {
        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public Decimal Faktor { get; set; }
    }
}
