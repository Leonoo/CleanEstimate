using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Text;

namespace CleanEstimate.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, Interface.IMainWindowViewModel
    {
        #region Fields
        private MainWindow m_MainWindow = null;

        private RelayCommand m_CloseCommand;
        private RelayCommand m_NewCommand;
        private RelayCommand m_LoadCommand;
        private RelayCommand m_SaveCommand;
        private RelayCommand m_SaveAsCommand;
        private RelayCommand m_DataGridDoubleClickCommand;

        private string m_FilePath = string.Empty;
        private bool m_IsEdited = false;

        private ObservableCollection<LeistungViewModel> m_Leistungen = null;
        private ViewModel.ObjektViewModel m_Objekt = new ObjektViewModel();
        #endregion //Fields

        #region Constructor

        public MainWindowViewModel()
        {
            DisplayName = "CleanEstimate";
        }

        #endregion //Constructor

        public string FilePath { get { return m_FilePath; } 
            set
            { 
                m_FilePath = value;
                if (!String.IsNullOrEmpty(value))
                {
                    DisplayName = "CleanEstimate - " + Path.GetFileName(value);
                }
                else
                {
                    DisplayName = "CleanEstimate";
                    OnPropertyChanged("EditDisplayName");
                }
            } 
        }

        public bool IsEdited { get { return m_IsEdited; } set { m_IsEdited = value; OnPropertyChanged("EditDisplayName"); } }

        public string EditDisplayName
        {
            get
            {
                if (IsEdited)
                    return DisplayName + "*";

                return DisplayName;
            }
        }

        public ObservableCollection<LeistungViewModel> Leistungen
        {
            get
            {
                return m_Leistungen;
            }
            set
            {
                if (m_Leistungen != value)
                {
                    m_Leistungen = value;
                    OnPropertyChanged("Leistungen");
                }
            }
        }

        public ViewModel.ObjektViewModel Objekt { get { return m_Objekt; } set { m_Objekt = value; OnPropertyChanged("Objekt"); } }

        public ICommand CloseCommand
        {
            get
            {
                if (m_CloseCommand == null)
                    m_CloseCommand = new RelayCommand(parm => OnRequestClose());

                return m_CloseCommand;
            }
        }

        public ICommand NewCommand
        {
            get
            {
                if (m_NewCommand == null)
                    m_NewCommand = new RelayCommand(parm =>
                {
                    Objekt = new ObjektViewModel();
                    Leistungen = Objekt.Leistungen;
                    FilePath = null;
                    IsEdited = false;
                });

                return m_NewCommand;
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                if (m_LoadCommand == null)
                    m_LoadCommand = new RelayCommand(parm =>
                {
                    Load();
                    IsEdited = false;
                });

                return m_LoadCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (m_SaveCommand == null)
                    m_SaveCommand = new RelayCommand(parm =>
                {
                    Save();
                    IsEdited = false;
                });

                return m_SaveCommand;
            }
        }

        public ICommand SaveAsCommand
        {
            get
            {
                if (m_SaveAsCommand == null)
                    m_SaveAsCommand = new RelayCommand(parm =>
                {
                    SaveAs();
                    IsEdited = false;
                });

                return m_SaveAsCommand;
            }
        }
      
        public ICommand DataGridDoubleClickCommand
        {
            get
            {
                if (m_DataGridDoubleClickCommand == null)
                    m_DataGridDoubleClickCommand = new RelayCommand((item) =>
                {
                    if (item is LeistungViewModel)
                    {
                        LeistungViewModel viewModel = item as LeistungViewModel;
                        LeistungViewModel tempViewModel = new LeistungViewModel();
                        tempViewModel.Load(viewModel);

                        View.LeistungView view = new View.LeistungView();
                        view.DataContext = tempViewModel;

                        bool? result = view.ShowDialog();

                        if (result.HasValue && result.Value)
                        {
                            tempViewModel.Save(viewModel);
                            IsEdited = true;
                        }
                    }
                    else
                    {
                        LeistungViewModel viewModel = new LeistungViewModel();

                        View.LeistungView view = new View.LeistungView();
                        view.DataContext = viewModel;

                        bool? result = view.ShowDialog();

                        if (result.HasValue && result.Value)
                        {
                            Objekt.AddLeistung(viewModel);
                            IsEdited = true;
                        }
                    }
                });

                return m_DataGridDoubleClickCommand;
            }
        }

        #region RequestClose [event]
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion // RequestClose [event]

        private void Save()
        {
            if (String.IsNullOrEmpty(FilePath))
            {
                SaveAs();
            }
            else
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    Daten.Objekt tempObjekt = new Daten.Objekt();
                    Objekt.Save(tempObjekt);

                    tempObjekt.Save(fs);
                }
            }
        }

        private void SaveAs()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".cexml";
            dlg.Filter = "CleanEstimate Datei |*.cexml";

            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                FilePath = dlg.FileName;
                Save();
            }
        }

        private void Load()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".cexml";
            dlg.Filter = "CleanEstimate Datei |*.cexml";

            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        Daten.Objekt tempObjekt = new Daten.Objekt();
                        tempObjekt.Load(fs);

                        Objekt = new ObjektViewModel();
                        Objekt.Load(tempObjekt);

                        Leistungen = Objekt.Leistungen;

                        FilePath = dlg.FileName;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Die CleanEstimate Datei ist fehlerhaft.", "Fehler CleanEstimate Datei!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Closed()
        {
            m_MainWindow = null;
        }

        #region IViewModel Member

        override public void Load(FrameworkElement element)
        {
            m_MainWindow = element as MainWindow;

            Delegates.VoidDelegate del = new Delegates.VoidDelegate(() =>
            {
                Leistungen = Objekt.Leistungen;
            });

            Dispatcher.BeginInvoke(del, null);
        }

        override public void Unload(FrameworkElement element)
        {
            Closed();
        }

        #endregion

        #region IMainWindowViewModel Member
        public void Closed(object sender, EventArgs e)
        {
            Closed();
        }

        public void Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        #endregion
    }
}
