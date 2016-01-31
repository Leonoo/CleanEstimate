using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    public class Haeufigkeit : SettingRankValue
    {
        private int _ID;
        [XmlAttribute()]
        public int ID
        {
            get { return _ID; }
            set { Set(() => ID, ref _ID, value); }
        }

        private string _ReportName;
        [XmlAttribute()]
        public string ReportName
        {
            get { return _ReportName; }
            set { Set(() => ReportName, ref _ReportName, value); }
        }

        private Decimal _Faktor;
        [XmlAttribute()]
        public Decimal Faktor
        {
            get { return _Faktor; }
            set { Set(() => Faktor, ref _Faktor, value); }
        }

        public static Haeufigkeit GetHaeufigkeit(IEnumerable<Haeufigkeit> listWithItems, int id)
        {
            var item = listWithItems.FirstOrDefault(x => x.ID.Equals(id));

            return item;
        }

        public static Haeufigkeit GetDefaultHaeufigkeit(IEnumerable<Haeufigkeit> listWithItems)
        {
            var item = listWithItems.FirstOrDefault(x => x.Default);

            if (item == null)
            {
                item = listWithItems.FirstOrDefault();
            }

            return item;
        }
    }
}
