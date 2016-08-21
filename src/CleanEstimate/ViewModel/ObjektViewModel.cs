using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CleanEstimate.ViewModel
{
    public class ObjektObservableObject : ObservableObject
    {
        private ObservableCollection<LeistungObservableObject> _Leistungen = new ObservableCollection<LeistungObservableObject>();
        private Decimal _Arbeistage = 5m;
        private Decimal _Stundenverrechnungssatz = 15.00m;

        private ListCollectionView _LeistungenView;
        public ListCollectionView LeistungenView
        {
            get
            {
                if (_LeistungenView == null)
                {
                    _LeistungenView = CollectionViewSource.GetDefaultView(_Leistungen) as ListCollectionView;
                    _LeistungenView.CustomSort = new Comparer.LeistungObservableObjectComparer();
                }

                return _LeistungenView;
            }
        }

        public ObservableCollection<LeistungObservableObject> Leistungen { get { return _Leistungen; } private set { _Leistungen = value; } }
        public Decimal Arbeistage { get { return _Arbeistage; } set { _Arbeistage = value; SVSOderArbeistageGeaendert(); } }
        public Decimal Stundenverrechnungssatz { get { return _Stundenverrechnungssatz; } set { _Stundenverrechnungssatz = value; SVSOderArbeistageGeaendert(); } }

        private decimal _Stunden;
        public decimal Stunden { get { return _Stunden; } set { Set(() => Stunden, ref _Stunden, value); } }

        private decimal _GesamtPreis;
        public decimal GesamtPreis { get { return _GesamtPreis; } set { Set(() => GesamtPreis, ref _GesamtPreis, value); } }

        private decimal _GesamtPreisJahr;
        public decimal GesamtPreisJahr { get { return _GesamtPreisJahr; } set { Set(() => GesamtPreisJahr, ref _GesamtPreisJahr, value); } }

        private string _Name = null;
        private string _Beschreibung = null;
        public string Name { get { return _Name; } set { Set(() => Name, ref _Name, value); } }
        public string Beschreibung { get { return _Beschreibung; } set { Set(() => Beschreibung, ref _Beschreibung, value); } }

        private Daten.Settings _Settings;
        public Daten.Settings Settings
        {
            get { return _Settings; }
            set { _Settings = value; }
        }

        private FirmaObservableObject _Firma;
        public FirmaObservableObject Firma
        {
            get { return _Firma; }
            set { _Firma = value; }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { Set(() => IsSelected, ref _IsSelected, value); }
        }

        public ObjektObservableObject()
        { }

        public ObjektObservableObject(Daten.Settings settings, FirmaObservableObject firma)
        {
            _Settings = settings;
            _Firma = firma;
        }

        public void AddLeistung(LeistungObservableObject viewModel)
        {
            Leistungen.Add(viewModel);
            LeistungenView.MoveCurrentTo(viewModel);
            viewModel.NewCalculation += CalculateOverview;
            CalculateOverview();
            viewModel.IsSelected = true;
        }

        public void Load(Daten.Objekt objekt)
        {
            Arbeistage = objekt.Arbeistage;
            Name = objekt.Name;
            Beschreibung = objekt.Beschreibung;
            Stundenverrechnungssatz = objekt.Stundenverrechnungssatz;

            foreach (Daten.Leistung item in objekt.Leistungen)
            {
                LeistungObservableObject viewModel = new LeistungObservableObject(this);
                viewModel.Load(item);
                Leistungen.Add(viewModel);
                viewModel.Calculate();
                viewModel.NewCalculation += CalculateOverview;
                CalculateOverview();
            }

            LeistungenView.Refresh();
        }

        public void Save(Daten.Objekt objekt)
        {
            objekt.Arbeistage = Arbeistage;
            objekt.Name = Name;
            objekt.Beschreibung = Beschreibung;
            objekt.Stundenverrechnungssatz = Stundenverrechnungssatz;

            foreach (LeistungObservableObject item in Leistungen)
            {
                Daten.Leistung leistung = new Daten.Leistung();
                item.Save(leistung);
                objekt.Leistungen.Add(leistung);
            }
        }

        public void SVSOderArbeistageGeaendert()
        {
            foreach (var item in Leistungen)
            {
                item.NewCalculation -= CalculateOverview;
                item.Calculate();
                item.NewCalculation += CalculateOverview;
            }

            CalculateOverview();
        }

        public void CalculateOverview()
        {
            if (Arbeistage > 0)
            {
                Stunden = Leistungen.Sum(x => x.ZeitMonatlich) / (365.2425m / 7m * Arbeistage / 12);
            }

            GesamtPreis = Leistungen.Sum(x => x.Preis);
            GesamtPreisJahr = GesamtPreis * 12;
            LeistungenView.Refresh();
        }
    }
}
