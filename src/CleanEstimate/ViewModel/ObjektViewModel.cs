using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CleanEstimate.ViewModel
{
    public class ObjektViewModel : ViewModelBase
    {
        private ObservableCollection<LeistungViewModel> m_Leistungen = new ObservableCollection<LeistungViewModel>();
        private Decimal m_Arbeistage = 5m;
        private Decimal m_Stundenverrechnungssatz = 15.00m;

        public ObservableCollection<LeistungViewModel> Leistungen
        {
            get
            {
                return m_Leistungen;
            }
            set
            {
                m_Leistungen = value;
            }
        }

        public Decimal Arbeistage
        {
            get
            {
                return m_Arbeistage;
            }
            set
            {
                m_Arbeistage = value;
            }
        }

        public Decimal Stundenverrechnungssatz
        {
            get
            {
                return m_Stundenverrechnungssatz;
            }
            set
            {
                m_Stundenverrechnungssatz = value;
            }
        }

        public void AddLeistung(LeistungViewModel viewModel)
        {
            viewModel.Objekt = this;
            Leistungen.Add(viewModel);
        }

        public void Load(Daten.Objekt objekt)
        {
            Arbeistage = objekt.Arbeistage;
            Stundenverrechnungssatz = objekt.Stundenverrechnungssatz;

            foreach (Daten.Leistung item in objekt.Leistungen)
            {
                LeistungViewModel viewModel = new LeistungViewModel();
                viewModel.Load(item);
                AddLeistung(viewModel);
            }
        }

        public void Save(Daten.Objekt objekt)
        {
            objekt.Arbeistage = Arbeistage;
            objekt.Stundenverrechnungssatz = Stundenverrechnungssatz;

            foreach (LeistungViewModel item in Leistungen)
            {
                Daten.Leistung leistung = new Daten.Leistung();
                item.Save(leistung);
                objekt.Leistungen.Add(leistung);
            }
        }
    }
}
