using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEstimate.Comparer
{
    public class LeistungObservableObjectComparer : IComparer<ViewModel.LeistungObservableObject>, IComparer
    {
        public int Compare(object x, object y)
        {
            var xViewModel = x as ViewModel.LeistungObservableObject;
            var yViewModel = y as ViewModel.LeistungObservableObject;

            return Compare(xViewModel, yViewModel);
        }

        public int Compare(ViewModel.LeistungObservableObject x, ViewModel.LeistungObservableObject y)
        {
            var temp = Daten.SettingRankValue.Compare(x.Etage, y.Etage);

            if (temp == 0)
            {
                temp = Daten.SettingValue.Compare(x.Bezeichnung, y.Bezeichnung);
                if (temp == 0)
                {
                    temp = Daten.SettingValue.Compare(x.Art, y.Art);
                    if (temp == 0)
                    {
                        temp = Daten.SettingValue.Compare(x.Methode, y.Methode);
                        if (temp == 0)
                        {
                            temp = x.Anzahl.CompareTo(y.Anzahl);
                            if (temp == 0)
                            {
                                temp = x.RichtLeistung.CompareTo(y.RichtLeistung);
                                if (temp == 0)
                                {
                                    temp = Daten.Haeufigkeit.Compare(x.Haeufigkeit, y.Haeufigkeit);
                                }
                            }
                        }
                    }
                }
            }

            return temp;
        }
    }
}
