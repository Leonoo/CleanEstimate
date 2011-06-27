using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanEstimate.ViewModel
{
    public class LeistungViewModel : ViewModelBase
    {
        private string m_Etage = string.Empty;
        private string m_Bezeichnung = string.Empty;
        private Decimal m_Anzahl = 0;
        private Decimal m_RichtLeistung = 0;
        private Decimal m_Haeufigkeit = 0;
        private Decimal m_AnzahlMonatlich = 0;
        private Decimal m_Preis = 0;
        private Decimal m_FixPreis = 0;
        private bool m_Fix = false;
        private ObjektViewModel m_Objekt = null;
        
        public string Etage 
        {
            get
            {
                return m_Etage;
            }
            set
            {
                if (m_Etage != value)
                {
                    m_Etage = value;
                    OnPropertyChanged("Etage");
                }
            }
        }

        public string Bezeichnung
        {
            get
            {
                return m_Bezeichnung;
            }
            set
            {
                if (m_Bezeichnung != value)
                {
                    m_Bezeichnung = value;
                    OnPropertyChanged("Bezeichnung");
                }
            }
        }

        public Decimal Anzahl
        {
            get
            {
                return m_Anzahl;
            }
            set
            {
                if (m_Anzahl != value)
                {
                    m_Anzahl = value;
                    OnPropertyChanged("Anzahl");
                }
            }
        }

        public Decimal RichtLeistung
        {
            get
            {
                return m_RichtLeistung;
            }
            set
            {
                if (m_RichtLeistung != value)
                {
                    m_RichtLeistung = value;
                    OnPropertyChanged("RichtLeistung");
                }
            }
        }

        public Decimal Haeufigkeit
        {
            get
            {
                return m_Haeufigkeit;
            }
            set
            {
                if (m_Haeufigkeit != value)
                {
                    m_Haeufigkeit = value;
                    OnPropertyChanged("Haeufigkeit");
                }
            }
        }

        public Decimal AnzahlMonatlich
        {
            get
            {
                return m_AnzahlMonatlich;
            }
            set
            {
                if (m_AnzahlMonatlich != value)
                {
                    m_AnzahlMonatlich = value;
                    OnPropertyChanged("AnzahlMonatlich");
                }
            }
        }

        public Decimal Preis
        {
            get
            {
                return m_Preis;
            }
            set
            {
                if (m_Preis != value)
                {
                    m_Preis = value;
                    OnPropertyChanged("Preis");
                }
            }
        }

        public Decimal FixPreis
        {
            get
            {
                return m_FixPreis;
            }
            set
            {
                if (m_FixPreis != value)
                {
                    m_FixPreis = value;
                    OnPropertyChanged("FixPreis");
                }
            }
        }

        public bool Fix
        {
            get
            {
                return m_Fix;
            }
            set
            {
                if (m_Fix != value)
                {
                    m_Fix = value;
                    OnPropertyChanged("Fix");
                }
            }
        }


        public ObjektViewModel Objekt { get { return m_Objekt; } set { m_Objekt = value; } }

        public LeistungViewModel()
        {
            DisplayName = "Leistung";
        }

        public void Load(Daten.Leistung leistung)
        {
            Etage = leistung.Etage;
            Bezeichnung = leistung.Bezeichnung;
            Anzahl = leistung.Anzahl;
            RichtLeistung = leistung.RichtLeistung;
            Haeufigkeit = leistung.Haeufigkeit;
            AnzahlMonatlich = leistung.AnzahlMonatlich;
            Preis = leistung.Preis;
            FixPreis = leistung.FixPreis;
            Fix = leistung.Fix;
        }

        public void Load(LeistungViewModel leistung)
        {
            Etage = leistung.Etage;
            Bezeichnung = leistung.Bezeichnung;
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
            leistung.Etage = Etage;
            leistung.Bezeichnung = Bezeichnung;
            leistung.Anzahl = Anzahl;
            leistung.RichtLeistung = RichtLeistung;
            leistung.Haeufigkeit = Haeufigkeit;
            leistung.AnzahlMonatlich = AnzahlMonatlich;
            leistung.Preis = Preis;
            leistung.FixPreis = FixPreis;
            leistung.Fix = Fix;
        }

        public void Save(LeistungViewModel leistung)
        {
            leistung.Etage = Etage;
            leistung.Bezeichnung = Bezeichnung;
            leistung.Anzahl = Anzahl;
            leistung.RichtLeistung = RichtLeistung;
            leistung.Haeufigkeit = Haeufigkeit;
            leistung.AnzahlMonatlich = AnzahlMonatlich;
            leistung.Preis = Preis;
            leistung.FixPreis = FixPreis;
            leistung.Fix = Fix;
        }
    }
}
