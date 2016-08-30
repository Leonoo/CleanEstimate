using CleanEstimate.Daten;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanEstimate.ViewModel
{
    public class LeistungObservableObject : ObservableObject
    {
        public event Action NewCalculation;

        private SettingRankValue _Etage = null;
        private SettingValue _Bezeichnung = null;
        private SettingValue _Art = null;
        private SettingValue _Methode = null;
        private SettingValue _Maenge = null;
        private Haeufigkeit _Haeufigkeit = null;

        private Decimal _Anzahl = 0;
        private Decimal _RichtLeistung = 0;

        private Decimal _AnzahlMonatlich = 0;
        private Decimal _Preis = 0;
        private Decimal _FixPreis = 0;
        private bool _Fix = false;
        private ObjektObservableObject _Objekt = null;

        public SettingRankValue Etage
        {
            get { return _Etage; }
            set { Set(() => Etage, ref _Etage, value); }
        }

        public SettingValue Bezeichnung
        {
            get { return _Bezeichnung; }
            set { Set(() => Bezeichnung, ref _Bezeichnung, value); }
        }

        public SettingValue Art
        {
            get { return _Art; }
            set { Set(() => Art, ref _Art, value); }
        }
        public SettingValue Methode
        {
            get { return _Methode; }
            set { Set(() => Methode, ref _Methode, value); }
        }
        public SettingValue Maenge
        {
            get { return _Maenge; }
            set { Set(() => Maenge, ref _Maenge, value); }
        }
        public Haeufigkeit Haeufigkeit
        {
            get { return _Haeufigkeit; }
            set { Set(() => Haeufigkeit, ref _Haeufigkeit, value); }
        }
        public Decimal Anzahl
        {
            get { return _Anzahl; }
            set { Set(() => Anzahl, ref _Anzahl, value); }
        }

        public Decimal RichtLeistung
        {
            get { return _RichtLeistung; }
            set { Set(() => RichtLeistung, ref _RichtLeistung, value); RaisePropertyChanged(nameof(RichtLeistungMitFaktor)); }
        }

        public Decimal RichtLeistungMitFaktor
        {
            get { return _RichtLeistung * Objekt.RichtleistungsFaktor; }
        }

        public Decimal AnzahlMonatlich
        {
            get { return _AnzahlMonatlich; }
            set { Set(() => AnzahlMonatlich, ref _AnzahlMonatlich, value); }
        }

        public Decimal Preis
        {
            get { return _Preis; }
            set { Set(() => Preis, ref _Preis, value); }
        }

        public Decimal FixPreis
        {
            get { return _FixPreis; }
            set { Set(() => FixPreis, ref _FixPreis, value); }
        }

        public bool Fix
        {
            get { return _Fix; }
            set { Set(() => Fix, ref _Fix, value); }
        }

        private Decimal _ZeitTaegelich;
        public Decimal ZeitTaeglich
        {
            get { return _ZeitTaegelich; }
            set { Set(() => ZeitTaeglich, ref _ZeitTaegelich, value); }
        }

        private Decimal _ZeitMonatlich;
        public Decimal ZeitMonatlich
        {
            get { return _ZeitMonatlich; }
            set { Set(() => ZeitMonatlich, ref _ZeitMonatlich, value); }
        }

        private bool _EigeneBezeichnung;
        public bool EigeneBezeichnung
        {
            get { return _EigeneBezeichnung; }
            set { Set(() => EigeneBezeichnung, ref _EigeneBezeichnung, value); }
        }

        private string _EigeneBezeichnungString;
        public string EigeneBezeichnungString
        {
            get { return _EigeneBezeichnungString; }
            set { Set(() => EigeneBezeichnungString, ref _EigeneBezeichnungString, value); }
        }

        private bool _EigeneEtage;
        public bool EigeneEtage
        {
            get { return _EigeneEtage; }
            set { Set(() => EigeneEtage, ref _EigeneEtage, value); }
        }

        private string _EigeneEtageString;
        public string EigeneEtageString
        {
            get { return _EigeneEtageString; }
            set { Set(() => EigeneEtageString, ref _EigeneEtageString, value); }
        }

        private bool _EigeneArt;
        public bool EigeneArt
        {
            get { return _EigeneArt; }
            set { Set(() => EigeneArt, ref _EigeneArt, value); }
        }

        private string _EigeneArtString;
        public string EigeneArtString
        {
            get { return _EigeneArtString; }
            set { Set(() => EigeneArtString, ref _EigeneArtString, value); }
        }

        private bool _EigeneMethode;
        public bool EigeneMethode
        {
            get { return _EigeneMethode; }
            set { Set(() => EigeneMethode, ref _EigeneMethode, value); }
        }

        private string _EigeneMethodeString;
        public string EigeneMethodeString
        {
            get { return _EigeneMethodeString; }
            set { Set(() => EigeneMethodeString, ref _EigeneMethodeString, value); }
        }

        public ObjektObservableObject Objekt { get { return _Objekt; } set { _Objekt = value; } }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { Set(() => IsSelected, ref _IsSelected, value); }
        }

        public LeistungObservableObject()
        { }

        public LeistungObservableObject(ObjektObservableObject objekt)
        {
            Objekt = objekt;
        }

        public void Load(Daten.Leistung leistung)
        {
            Etage = SettingRankValue.GetSettingRankValue(Objekt.Settings.Etage, leistung.Etage, ref _EigeneEtage);
            if (EigeneEtage) EigeneEtageString = leistung.Etage;

            Bezeichnung = SettingValue.GetSettingValue(Objekt.Settings.Bezeichnung, leistung.Bezeichnung, ref _EigeneBezeichnung);
            if (EigeneBezeichnung) EigeneBezeichnungString = leistung.Bezeichnung;

            Art = SettingValue.GetSettingValue(Objekt.Settings.Arten, leistung.Art, ref _EigeneArt);
            if (EigeneArt) EigeneArtString = leistung.Art;

            Methode = SettingValue.GetSettingValue(Objekt.Settings.Methoden, leistung.Methode, ref _EigeneMethode);
            if (EigeneMethode) EigeneMethodeString = leistung.Methode;

            Maenge = SettingValue.GetSettingValue(Objekt.Settings.Einheit, leistung.Maenge);
            Anzahl = leistung.Anzahl;
            RichtLeistung = leistung.RichtLeistung;
            FixPreis = leistung.FixPreis;
            Fix = leistung.Fix;

            Haeufigkeit = Haeufigkeit.GetHaeufigkeit(Objekt.Firma.Haeufigkeiten, leistung.HaeufigkeitID);
        }

        public void Load(LeistungObservableObject leistung)
        {
            bool newSetting = false;

            if (leistung.EigeneEtage)
            {
                Etage = SettingRankValue.GetSettingRankValue(Objekt.Settings.Etage, leistung.EigeneEtageString, ref newSetting);

                if (newSetting)
                {
                    EigeneEtageString = leistung.EigeneEtageString;
                    EigeneEtage = true;
                }
                else
                {
                    EigeneEtageString = null;
                    EigeneEtage = false;
                }
            }
            else
            {
                Etage = leistung.Etage;
            }

            if (leistung.EigeneBezeichnung)
            {
                Bezeichnung = SettingValue.GetSettingValue(Objekt.Settings.Bezeichnung, leistung.EigeneBezeichnungString, ref newSetting);

                if (newSetting)
                {
                    EigeneBezeichnungString = leistung.EigeneBezeichnungString;
                    EigeneBezeichnung = true;
                }
                else
                {
                    EigeneBezeichnungString = null;
                    EigeneBezeichnung = false;
                }
            }
            else
            {
                Bezeichnung = leistung.Bezeichnung;
            }

            if (leistung.EigeneArt)
            {
                Art = SettingValue.GetSettingValue(Objekt.Settings.Arten, leistung.EigeneArtString, ref newSetting);

                if (newSetting)
                {
                    EigeneArtString = leistung.EigeneArtString;
                    EigeneArt = true;
                }
                else
                {
                    EigeneArtString = null;
                    EigeneArt = false;
                }
            }
            else
            {
                Art = leistung.Art;
            }

            if (leistung.EigeneMethode)
            {
                Methode = SettingValue.GetSettingValue(Objekt.Settings.Methoden, leistung.EigeneMethodeString, ref newSetting);

                if (newSetting)
                {
                    EigeneMethodeString = leistung.EigeneMethodeString;
                    EigeneMethode = true;
                }
                else
                {
                    EigeneMethodeString = null;
                    EigeneMethode = false;
                }
            }
            else
            {
                Methode = leistung.Methode;
            }

            Maenge = leistung.Maenge;
            Anzahl = leistung.Anzahl;
            RichtLeistung = leistung.RichtLeistung;
            Haeufigkeit = leistung.Haeufigkeit;
            AnzahlMonatlich = leistung.AnzahlMonatlich;
            Preis = leistung.Preis;
            FixPreis = leistung.FixPreis;
            Fix = leistung.Fix;
        }

        public void Save(Daten.Leistung leistung)
        {
            leistung.Etage = Etage.Name;
            leistung.Bezeichnung = Bezeichnung.Name;
            leistung.Art = Art.Name;
            leistung.Methode = Methode.Name;
            leistung.Maenge = Maenge.Name;
            leistung.Anzahl = Anzahl;
            leistung.RichtLeistung = RichtLeistung;
            if (Haeufigkeit != null)
            {
                leistung.HaeufigkeitID = Haeufigkeit.ID;
            }
            else
            {
                leistung.HaeufigkeitID = 0;
            }

            leistung.FixPreis = FixPreis;
            leistung.Fix = Fix;
        }

        public void Calculate()
        {
            if (!Fix)
            {
                if (Haeufigkeit != null && Haeufigkeit.Faktor > 0 && Anzahl > 0)
                {
                    AnzahlMonatlich = Anzahl * Haeufigkeit.Faktor;

                    if (RichtLeistung != 0)
                    {
                        ZeitMonatlich = AnzahlMonatlich / RichtLeistungMitFaktor;
                        ZeitTaeglich = ZeitMonatlich / Haeufigkeit.Faktor;

                        Preis = decimal.Round(ZeitMonatlich * Objekt.Stundenverrechnungssatz, 2);
                    }
                    else
                    {
                        ZeitMonatlich = 0;
                        ZeitTaeglich = 0;
                        Preis = 0;
                    }
                }
                else
                {
                    AnzahlMonatlich = 0;
                    ZeitMonatlich = 0;
                    ZeitTaeglich = 0;
                    Preis = 0;
                }
            }
            else
            {
                Art = null;
                Methode = null;
                Haeufigkeit = null;
                AnzahlMonatlich = 0;
                ZeitMonatlich = 0;
                ZeitTaeglich = 0;

                Preis = FixPreis;
            }

            if (NewCalculation != null)
            {
                NewCalculation();
                IsSelected = true;
            }
        }
    }
}
