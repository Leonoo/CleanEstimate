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

        public List<Leistung> Leistungen
        {
            get
            {
                return m_Leistungen;
            }
            set
            {
                m_Leistungen = value;
            }
        }

        public Decimal Arbeistage
        {
            get
            {
                return m_Arbeistage;
            }
            set
            {
                m_Arbeistage = value;
            }
        }

        public Decimal Stundenverrechnungssatz
        {
            get
            {
                return m_Stundenverrechnungssatz;
            }
            set
            {
                m_Stundenverrechnungssatz = value;
            }
        }

        public void Save(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            try
            {
                XmlSerializer seri = new XmlSerializer(typeof(Objekt));
                seri.Serialize(stream, this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(Stream stream)
        {
            Objekt tempObjekt = null;

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            try
            {
                XmlSerializer seri = new XmlSerializer(typeof(Objekt));
                tempObjekt = seri.Deserialize(stream) as Objekt;

                Leistungen = tempObjekt.Leistungen;
                Arbeistage = tempObjekt.Arbeistage;
                Stundenverrechnungssatz = tempObjekt.Stundenverrechnungssatz;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
