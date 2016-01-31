using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CleanEstimate.Behavior
{
    public static class SelectAllTextOnFocusBehavior
    {
        // Using a DependencyProperty as the backing store for SelectAllOnFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectAllOnFocusProperty =
            DependencyProperty.RegisterAttached("SelectAllOnFocus", typeof(bool), typeof(SelectAllTextOnFocusBehavior), new PropertyMetadata(SelectAllOnFocusChanged));

        public static bool GetSelectAllOnFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllOnFocusProperty);
        }

        public static void SetSelectAllOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllOnFocusProperty, value);
        }

        private static void SelectAllOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as TextBox;
            if (element != null)
            {
                element.GotKeyboardFocus += ElementGotKeyboardFocus;
                element.GotMouseCapture += ElementGotMouseCapture;
            }
        }

        private static void ElementGotKeyboardFocus(object sender,
            System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            Action del = () =>
            {
                var element = sender as TextBox;
                element.SelectAll();
            };

            App.Current.Dispatcher.BeginInvoke(del, System.Windows.Threading.DispatcherPriority.ApplicationIdle);  
        }

        private static void ElementGotMouseCapture(object sender,
            System.Windows.Input.MouseEventArgs e)
        {
            var element = sender as TextBox;
            element.SelectAll();
        }
    }
}
