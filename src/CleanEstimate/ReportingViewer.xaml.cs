using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CleanEstimate
{
    /// <summary>
    /// Interaktionslogik für ReportingViewer.xaml
    /// </summary>
    public partial class ReportingViewer : Window
    {
        private bool _isReportViewerLoaded;
        private LocalReport _Report;

        public ReportingViewer(LocalReport report)
        {
            InitializeComponent();
            _Report = report;
        }

        private void Load()
        {
            if (!_isReportViewerLoaded)
            {
                foreach (var item in _Report.DataSources)
                {
                    _reportViewer.LocalReport.DataSources.Add(item);
                }

                _reportViewer.LocalReport.ReportEmbeddedResource = _Report.ReportEmbeddedResource;

                _reportViewer.RefreshReport();
                _reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                _isReportViewerLoaded = true;
            }
        }

        private void _reportViewer_Load(object sender, EventArgs e)
        {
            Load();
        }
    }
}
