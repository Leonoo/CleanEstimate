using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    public class Leistung
    {
        [XmlAttribute]
        public string Etage { get; set; }
        [XmlAttribute]
        public string Bezeichnung { get; set; }
        [XmlAttribute]
        public string Art { get; set; }
        [XmlAttribute]
        public string Methode { get; set; }
        [XmlAttribute]
        public string Maenge { get; set; }
        [XmlAttribute]
        public Decimal Anzahl { get; set; }
        [XmlAttribute]
        public Decimal RichtLeistung { get; set; }
        [XmlAttribute]
        public int HaeufigkeitID { get; set; }
        [XmlAttribute]
        public Decimal FixPreis { get; set; }
        [XmlAttribute]
        public bool Fix { get; set; }
    }
}
