using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Controls;

namespace CleanEstimate.Behavior
{
    public static class DataGridMouseDoubleClickBehavior
    {
        /// <summary>
        /// Hooks up a weak event against the source Selectors MouseDoubleClick
        /// if the Selector has asked for the HandleDoubleClick to be handled
        ///�
        /// If the source Selector has expressed an interest in not having its
        /// MouseDoubleClick handled the internal reference
        /// </summary>
        private static void OnHandleDoubleClickCommandChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = d as DataGrid;
            if (dataGrid != null)
            {
                dataGrid.MouseDoubleClick -= OnMouseDoubleClick;
                if (e.NewValue != null)
                {
                    dataGrid.MouseDoubleClick += OnMouseDoubleClick;
                }
            }
        }

        /// <summary>
        /// TheCommandToRun : The actual ICommand to run
        /// </summary>
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand",
                typeof(ICommand),
                typeof(DataGridMouseDoubleClickBehavior),
                new FrameworkPropertyMetadata((ICommand)null,
                    new PropertyChangedCallback(OnHandleDoubleClickCommandChanged)));

        /// <summary>
        /// Gets the TheCommandToRun property. �
        /// </summary>
        public static ICommand GetDoubleClickCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(DoubleClickCommandProperty);
        }

        /// <summary>
        /// Sets the TheCommandToRun property. �
        /// </summary>
        public static void SetDoubleClickCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(DoubleClickCommandProperty, value);
        }

        #region Handle the event

        /// <summary>
        /// Invoke the command we tagged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;

            DependencyObject originalSender = e.OriginalSource as DependencyObject;
            if (dataGrid == null || originalSender == null)
                return;

            IInputElement element = e.MouseDevice.DirectlyOver;
            if (element != null && element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent is DataGridCell)
                {
                    ICommand command = (ICommand)(sender as DependencyObject).GetValue(DoubleClickCommandProperty);

                    if (command != null)
                    {
                        if (command.CanExecute(null))
                            command.Execute(dataGrid.SelectedItem);
                    }
                }
            }
        }
        #endregion
    }
}
