using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEstimate.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        private string _DisplayName;

        public string DisplayName
        {
            get { return _DisplayName; }
            set { Set(() => DisplayName, ref _DisplayName, value); }
        }

    }
}
