using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CleanEstimate.Daten
{
    public class SettingValue : ObservableObject
    {
        private string _Name;
        [XmlAttribute()]
        public string Name
        {
            get { return _Name; }
            set { Set(() => Name, ref _Name, value); }
        }

        private bool _Default;
        [XmlAttribute()]
        public bool Default
        {
            get { return _Default; }
            set { Set(() => Default, ref _Default, value); }
        }

        public static int Compare(SettingValue x, SettingValue y)
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
                temp = x.Name.CompareTo(y.Name);
            }

            return temp;
        }

        public static SettingValue GetSettingValue(IEnumerable<SettingValue> listWithItems, string name)
        {
            bool tempBool = false;
            return GetSettingValue(listWithItems, name, ref tempBool);
        }

        public static SettingValue GetSettingValue(IEnumerable<SettingValue> listWithItems, string name, ref bool isNew)
        {
            var item = listWithItems.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (item == null)
            {
                item = new SettingValue() { Name = name };
                isNew = true;
            }

            return item;
        }

        public static SettingValue GetDefaultSettingValue(IEnumerable<SettingValue> listWithItems)
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
