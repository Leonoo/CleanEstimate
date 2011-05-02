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

        private SqlConnection m_SQLConnection = null;
        private DataSet m_DataSet = new DataSet();

        private SqlDataAdapter m_sdaFirm = null;
        //private SqlDataAdapter m_sdaObj = null;

        private RelayCommand m_CloseCommand;
        #endregion //Fields

        #region Constructor

        public MainWindowViewModel()
        {
            DisplayName = "CleanEstimate";
        }

        #endregion //Constructor

        private object m_MyData = null;
        public object MyData { get { return m_MyData; } set { m_MyData = value; OnPropertyChanged("MyData"); } }

        public ICommand CloseCommand
        {
            get
            {
                if (m_CloseCommand == null)
                    m_CloseCommand = new RelayCommand(parm => this.OnRequestClose());

                return m_CloseCommand;
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

        private void DBConnect()
        {
            m_SQLConnection = new SqlConnection(@"Server=.\SQLEXPRESS;Trusted_Connection=true;");
            m_SQLConnection.Open();

            CreateDBNew();

            UseDB();

            CreateFirmTable();
        }

        public virtual bool DBExists()
        {
            /*bool exists = false;

            try
            {
                SqlCommand cmd = m_SQLConnection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(name) FROM sys.databases WHERE name = @DBName";
                cmd.Parameters.AddWithValue("@DBName", "Kalku");

                exists = (Convert.ToInt32(cmd.ExecuteScalar()) == 1);
            }
            catch (Exception)
            {

            }

            return exists;*/
            return false;
        }

        public void CreateDBNew()
        {
            string createDB = @"CREATE DATABASE Kalku";

            try
            {
                SqlCommand cmd = m_SQLConnection.CreateCommand();
                cmd.CommandText = createDB;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }

        public void UseDB()
        {
            string useDB = @"USE Kalku";

            try
            {
                SqlCommand cmd = m_SQLConnection.CreateCommand();
                cmd.CommandText = useDB;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }

        private void CreateFirmTable()
        {
            string sql = string.Format("IF NOT EXISTS"
               + "(SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[{0}]')"
               + "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)"
               + "CREATE TABLE {0} ("
               + "ID INTEGER not null, "
               + "FirmName VARCHAR(50) not null, "
               + "Straße VARCHAR(50) not null, "
               + "Ort VARCHAR(50) not null, "
               + "PRIMARY KEY (ID));", "Firm");

            try
            {
                SqlCommand cmd = m_SQLConnection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

        }

        private void ShowFirm()
        {
            m_sdaFirm = new SqlDataAdapter(@"select * from Firm", m_SQLConnection);
            new SqlCommandBuilder(m_sdaFirm);

            m_sdaFirm.Fill(m_DataSet, "Firm");

            /*var quary = from f in m_DataSet.Tables["Firm"].AsEnumerable()
                        select new Daten.FrimData
                            {
                                ID = f.Field<int>("ID"),
                                Name = f.Field<string>("FirmName"),
                                Straße = f.Field<string>("Straße"),
                                Ort = f.Field<string>("Ort")
                            };



            MyData = new ObservableCollection<Daten.FrimData>(quary);*/

            MyData = m_DataSet.Tables["Firm"].DefaultView;
        }

        #region IViewModel Member

        override public void Load(System.Windows.FrameworkElement element)
        {
            m_MainWindow = element as MainWindow;

            Delegates.VoidDelegate del = new Delegates.VoidDelegate(() =>
            {
                DBConnect();

                ShowFirm();
            });

            Dispatcher.BeginInvoke(del, null);
        }

        override public void Unload(System.Windows.FrameworkElement element)
        {
            Closed();
        }

        #endregion

        #region IMainWindowViewModel Member

        public void Closed()
        {
            m_MainWindow = null;
        }

        #endregion
    }
}
