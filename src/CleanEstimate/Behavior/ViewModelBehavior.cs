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
                // If the DataContext is being updated from another thread; like if IsAsync is true
                // on a binding statement, then we could potentially unload the element and unhook
                // the handler before this event is called because it is dispatched.  So, we always
                // check to see if the element IsLoaded, if it isn't then we better ignore the 
                // event and not re-hook the event handler for Invalid.
                //
                // We also want to ignore calling Unload if the element was never loaded, because
                // that would mean it will be called a second time when it finally does load.
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
                // Check to see if the element has already called Loaded, because some WPF 
                // components can actually call Loaded twice.
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
        }

        #endregion
    }
}
