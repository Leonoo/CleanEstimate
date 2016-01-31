using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    [XmlRoot("CleanEstimate")]
    public class Objekt
    {
        private List<Leistung> m_Leistungen = new List<Leistung>();

        private Decimal m_Arbeistage = 5m;
        private Decimal m_Stundenverrechnungssatz = 15.00m;

        public List<Leistung> Leistungen { get { return m_Leistungen; } set { m_Leistungen = value; } }
        [XmlAttribute]
        public Decimal Arbeistage { get { return m_Arbeistage; } set { m_Arbeistage = value; } }
        [XmlAttribute]
        public Decimal Stundenverrechnungssatz { get { return m_Stundenverrechnungssatz; } set { m_Stundenverrechnungssatz = value; } }

        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Beschreibung { get; set; }
    }
}
