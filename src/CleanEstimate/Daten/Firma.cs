using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    public class Firma
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Strasse { get; set; }
        [XmlAttribute]
        public string PLZ { get; set; }
        [XmlAttribute]
        public string Ort { get; set; }
        [XmlAttribute]
        public string Beschreibung { get; set; }

        private List<Objekt> _Objekte = new List<Objekt>();
        public List<Objekt> Objekte { get { return _Objekte; } set { _Objekte = value; } }

        private List<Haeufigkeit> m_Haeufigkeiten = new List<Haeufigkeit>();
        public List<Haeufigkeit> Haeufigkeiten { get { return m_Haeufigkeiten; } set { m_Haeufigkeiten = value; } }

        public void Save(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            try
            {
                XmlSerializer seri = new XmlSerializer(typeof(Firma));
                seri.Serialize(stream, this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(Stream stream)
        {
            Firma tempObjekt = null;

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            try
            {
                XmlSerializer seri = new XmlSerializer(typeof(Firma));
                tempObjekt = seri.Deserialize(stream) as Firma;

                Name = tempObjekt.Name;
                Strasse = tempObjekt.Strasse;
                PLZ = tempObjekt.PLZ;
                Ort = tempObjekt.Ort;
                Beschreibung = tempObjekt.Beschreibung;

                Haeufigkeiten = tempObjekt.Haeufigkeiten;
                Objekte = tempObjekt.Objekte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
