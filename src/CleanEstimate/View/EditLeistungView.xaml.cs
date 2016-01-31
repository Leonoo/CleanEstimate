using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CleanEstimate.View
{
    /// <summary>
    /// Interaktionslogik für LeistungView.xaml
    /// </summary>
    public partial class EditLeistungView : Window
    {
        public EditLeistungView()
        {
            InitializeComponent();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
