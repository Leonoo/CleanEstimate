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
        private List<Haeufigkeit> _Haeufigkeiten = new List<Haeufigkeit>();
        private List<SettingRankValue> _Etage = new List<SettingRankValue>();
        private List<SettingValue> _Bezeichnung = new List<SettingValue>();
        private List<SettingValue> _Arten = new List<SettingValue>();
        private List<SettingValue> _Methoden = new List<SettingValue>();
        private List<SettingValue> _Einheit = new List<SettingValue>();

        public List<Haeufigkeit> Haeufigkeiten { get { return _Haeufigkeiten; } set { _Haeufigkeiten = value; } }
        public List<SettingRankValue> Etage { get { return _Etage; } set { _Etage = value; } }
        public List<SettingValue> Bezeichnung { get { return _Bezeichnung; } set { _Bezeichnung = value; } }
        public List<SettingValue> Arten { get { return _Arten; } set { _Arten = value; } }
        public List<SettingValue> Methoden { get { return _Methoden; } set { _Methoden = value; } }
        public List<SettingValue> Einheit { get { return _Einheit; } set { _Einheit = value; } }

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
                Etage = tempSettings.Etage;
                Bezeichnung = tempSettings.Bezeichnung;
                Arten = tempSettings.Arten;
                Methoden = tempSettings.Methoden;
                Einheit = tempSettings.Einheit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
