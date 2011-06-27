using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    [XmlRoot("CleanEstimate_Settings")]
    public class Settings
    {
        private List<Haeufigkeit> m_Haeufigkeiten = new List<Haeufigkeit>();
        private List<string> m_Arten = new List<string>();
        private List<string> m_Methoden = new List<string>();


        public List<Haeufigkeit> Haeufigkeiten { get { return m_Haeufigkeiten; } set { m_Haeufigkeiten = value; } }
        public List<string> Arten { get { return m_Arten; } set { m_Arten = value; } }
        public List<string> Methoden { get { return m_Methoden; } set { m_Methoden = value; } }

        public void Save(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            try
            {
                XmlSerializer seri = new XmlSerializer(typeof(Settings));
                seri.Serialize(stream, this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(Stream stream)
        {
            Settings tempSettings = null;

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            try
            {
                XmlSerializer seri = new XmlSerializer(typeof(Settings));
                tempSettings = seri.Deserialize(stream) as Settings;

                Haeufigkeiten = tempSettings.Haeufigkeiten;
                Arten = tempSettings.Arten;
                Methoden = tempSettings.Methoden;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
