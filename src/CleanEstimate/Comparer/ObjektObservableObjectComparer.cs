using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEstimate.Comparer
{
    public class ObjektObservableObjectComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var xViewModel = x as ViewModel.ObjektObservableObject;
            var yViewModel = y as ViewModel.ObjektObservableObject;

            return Compare(xViewModel, yViewModel);
        }

        public int Compare(ViewModel.ObjektObservableObject x, ViewModel.ObjektObservableObject y)
        {
            var temp = string.Compare(x.Name, y.Name);

            if (temp == 0)
            {
                temp = string.Compare(x.Beschreibung, y.Beschreibung);
            }

            return temp;
        }
    }
}
