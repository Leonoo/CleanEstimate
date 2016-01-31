using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CleanEstimate.ViewModel
{
    public class FirmaObservableObject : ObservableObject
    {
        private Daten.Firma _Firma;
        public Daten.Firma Firma
        {
            get { return _Firma; }
        }

        public string Name { get { return _Firma.Name; } set { _Firma.Name = value; RaisePropertyChanged(() => Name); } }
        public string Strasse { get { return _Firma.Strasse; } set { _Firma.Strasse = value; RaisePropertyChanged(() => Strasse); } }
        public string PLZ { get { return _Firma.PLZ; } set { _Firma.PLZ = value; RaisePropertyChanged(() => PLZ); } }
        public string Ort { get { return _Firma.Ort; } set { _Firma.Ort = value; RaisePropertyChanged(() => Ort); } }
        public string Beschreibung { get { return _Firma.Beschreibung; } set { _Firma.Beschreibung = value; RaisePropertyChanged(() => Beschreibung); } }

        private ListCollectionView _ObjekteView;
        public ListCollectionView ObjekteView
        {
            get
            {
                if (_ObjekteView == null)
                {
                    _ObjekteView = CollectionViewSource.GetDefaultView(_Objekte) as ListCollectionView;
                    _ObjekteView.CustomSort = new Comparer.ObjektObservableObjectComparer();
                }

                return _ObjekteView;
            }
        }

        private ObservableCollection<ObjektObservableObject> _Objekte = new ObservableCollection<ObjektObservableObject>();
        public ObservableCollection<ObjektObservableObject> Objekte { get { return _Objekte; } set { _Objekte = value; } }

        private ObservableCollection<Daten.Haeufigkeit> _Haeufigkeiten = new ObservableCollection<Daten.Haeufigkeit>();
        public ObservableCollection<Daten.Haeufigkeit> Haeufigkeiten { get { return _Haeufigkeiten; } set { _Haeufigkeiten = value; } }

        public FirmaObservableObject(Daten.Firma firma)
        {
            _Firma = firma;

            firma.Haeufigkeiten.ForEach(x => Haeufigkeiten.Add(x));

            firma.Objekte.ForEach(x =>
            {
                var tempObjekt = new ObjektObservableObject(MainWindowViewModel.Settings, this);
                tempObjekt.Load(x); 
                Objekte.Add(tempObjekt);
            });
        }

        public void Save()
        {
            Firma.Name = Name;
            Firma.Strasse = Strasse;
            Firma.PLZ = PLZ;
            Firma.Ort = Ort;
            Firma.Beschreibung = Beschreibung;

            Firma.Objekte.Clear();
            foreach (var item in Objekte)
            {
                var tempObjekt = new Daten.Objekt();
                item.Save(tempObjekt);
                Firma.Objekte.Add(tempObjekt);
            }

            Firma.Haeufigkeiten.Clear();
            foreach (var item in Haeufigkeiten)
            {
                Firma.Haeufigkeiten.Add(item);
            }
        }
    }
}
