using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace CleanEstimate.Interface
{
    /// <summary>
    /// The <see cref="IViewModel"/> interface is the base class of view models.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Load should be called when the UI component the <see cref="IViewModel"/> has been
        /// data bound to has been loaded.  For this to work correctly, you should be using the
        /// <see cref="ViewModelBehavior"/> attachable behavior class.
        /// </summary>
        /// <param name="element">The element that is data-bound to this 
        /// <see cref="IViewModel"/>.</param>
        void Load(FrameworkElement element);

        /// <summary>
        /// Unload should be called when the UI component the <see cref="IViewModel"/> has been
        /// data bound to has been unloaded.  For this to work correctly, you should be using the
        /// <see cref="ViewModelBehavior"/> attachable behavior class.
        /// </summary>
        /// <param name="element">The element that is data-bound to this 
        /// <see cref="IViewModel"/>.</param>
        void Unload(FrameworkElement element);
    }
}
