using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    public class SettingRankValue : SettingValue
    {
        private int _Rank;
        [XmlAttribute()]
        public int Rank
        {
            get { return _Rank; }
            set { Set(() => Rank, ref _Rank, value); }
        }

        public static int Compare(SettingRankValue x, SettingRankValue y)
        {
            int temp = 0;

            if (x == null && y == null)
            {
                temp = 0;
            }
            else if (x == null && y != null)
            {
                temp = -1;
            }
            else if (x != null && y == null)
            {
                temp = 1;
            }
            else
            {
                temp = x.Rank.CompareTo(y.Rank);
            }

            return temp;
        }

        public static SettingRankValue GetSettingRankValue(IEnumerable<SettingRankValue> listWithItems, string name,int notFoundRank = 0)
        {
            bool tempBool = false;
            return GetSettingRankValue(listWithItems, name, ref tempBool, notFoundRank);
        }

        public static SettingRankValue GetSettingRankValue(IEnumerable<SettingRankValue> listWithItems, string name, ref bool isNew, int notFoundRank = 0)
        {
            var item = listWithItems.FirstOrDefault(x => x.Name.Equals(name));

            if (item == null)
            {
                item = new SettingRankValue() { Rank = notFoundRank, Name = name };
                isNew = true;
            }

            return item;
        }

        public static SettingRankValue GetDefaultSettingRankValue(IEnumerable<SettingRankValue> listWithItems)
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
