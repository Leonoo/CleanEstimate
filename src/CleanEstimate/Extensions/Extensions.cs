using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CleanEstimate
{
    //public static class MyWpfExtensions
    //{
    //    public static System.Windows.Forms.IWin32Window GetIWin32Window(this System.Windows.Media.Visual visual)
    //    {
    //        var source = System.Windows.PresentationSource.FromVisual(visual) as System.Windows.Interop.HwndSource;
    //        System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
    //        return win;
    //    }

    //    private class OldWindow : System.Windows.Forms.IWin32Window
    //    {
    //        private readonly System.IntPtr _handle;
    //        public OldWindow(System.IntPtr handle)
    //        {
    //            _handle = handle;
    //        }

    //        #region IWin32Window Members
    //        System.IntPtr System.Windows.Forms.IWin32Window.Handle
    //        {
    //            get { return _handle; }
    //        }
    //        #endregion
    //    }
    //}

    public static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var comparer = new Comparer<T>(comparison);

            List<T> sorted = collection.OrderBy(x => x, comparer).ToList();

            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }

        private class Comparer<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;

            public Comparer(Comparison<T> comparison)
            {
                this.comparison = comparison;
            }

            #region IComparer<T> Members

            public int Compare(T x, T y)
            {
                return comparison.Invoke(x, y);
            }

            #endregion
        }
    }
}
