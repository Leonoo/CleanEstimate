using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CleanEstimate.Interface
{
    public interface IMainWindowViewModel
    {
        //void LeftViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e, int tabIndex);
        void Closed();
    }
}
