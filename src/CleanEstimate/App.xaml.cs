using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace CleanEstimate
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(
                    System.Globalization.CultureInfo.CurrentCulture.Name)));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();

            // Create the ViewModel to which 
            // the main window binds.
            var viewModel = new ViewModel.MainWindowViewModel();

            // When the ViewModel asks to be closed, 
            // close the window.
            EventHandler handler = null;
            handler = (sender, e2) => 
            {
                viewModel.RequestClose -= handler;
                window.Close();
            };

            viewModel.RequestClose += handler; 

            // Allow all controls in the window to 
            // bind to the ViewModel by setting the 
            // DataContext, which propagates down 
            // the element tree.
            window.DataContext = viewModel;

            window.Show();
        }
    }
}
