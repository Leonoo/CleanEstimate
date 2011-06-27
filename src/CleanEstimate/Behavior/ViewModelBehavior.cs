using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using CleanEstimate.Interface;

namespace CleanEstimate.Behavior
{
    /// <summary>
    /// The <see cref="ViewModelBehavior"/> attachable behavior class is designed to help map
    /// the load and the unload of a view model to the load and unload of the 
    /// <see cref="FrameworkElement"/> that an <see cref="IViewModel"/> has been data bound to.  
    /// When the the <see cref="FrameworkElement"/> is Loaded or Unloaded the 
    /// <see cref="IViewModel"/> will have its Load and Unload called.  Load and Unload will also
    /// be called if a <see cref="IViewModel"/> is connected or disconnected from the DataContext
    /// after the UI component has already loaded.
    /// </summary>
    public static class ViewModelBehavior
    {
        #region LoadUnload Behavior
        
        /// <summary>
        /// The LoadUnloadProperty is for hooking up load and unload functionality from the UI to
        /// the <see cref="IViewModel"/> that is bound to it.
        /// </summary>
        public static readonly DependencyProperty LoadUnloadProperty =
            DependencyProperty.RegisterAttached("LoadUnload", typeof(Boolean),
            typeof(ViewModelBehavior),
            new FrameworkPropertyMetadata(false,
                new PropertyChangedCallback(OnLoadUnloadChanged)));

        public static void SetLoadUnload(FrameworkElement element, Boolean value)
        {
            element.SetValue(LoadUnloadProperty, value);
        }

        public static Boolean GetLoadUnload(FrameworkElement element)
        {
            return (Boolean)element.GetValue(LoadUnloadProperty);
        }

        public static void OnLoadUnloadChanged(DependencyObject obj, 
            DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = obj as FrameworkElement;

            if (element == null)
                throw new InvalidOperationException();

            element.DataContextChanged += (sender, e) =>
            {
                if (!element.IsLoaded)
                    return;

                if (e.OldValue is IViewModel)
                    ((IViewModel)e.OldValue).Unload(element);

                if (e.NewValue is IViewModel)
                    ((IViewModel)e.NewValue).Load(element);
            };

            bool elementLoaded = false;

            element.Loaded += (sender, e) =>
            {
                if (elementLoaded)
                    return;

                elementLoaded = true;

                IViewModel viewModel =
                    element.GetValue(FrameworkElement.DataContextProperty) as IViewModel;

                if (viewModel != null)
                    viewModel.Load(element);
            };

            element.Unloaded += (sender, e) =>
            {
                IViewModel viewModel =
                    element.GetValue(FrameworkElement.DataContextProperty) as IViewModel;

                elementLoaded = false;

                if (viewModel != null)
                    viewModel.Unload(element);
            };

            Window window = element as Window;
            if (window != null)
            {
                window.Closing += (sender, e) =>
                    {
                        IMainWindowViewModel viewModel = window.GetValue(FrameworkElement.DataContextProperty) as IMainWindowViewModel;
                        if (viewModel != null)
                        {
                            viewModel.Closing(sender, e);
                        }
                    };

                window.Closed += (sender, e) =>
                    {
                        IMainWindowViewModel viewModel = window.GetValue(FrameworkElement.DataContextProperty) as IMainWindowViewModel;
                        if (viewModel != null)
                        {
                            viewModel.Closed(sender, e);
                        }
                    };
            }
        }

        #endregion
    }
}
