using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEstimate.Report.Daten
{
    public class Leistung
    {
        public string Etage { get; set; }
        public string Bezeichnung { get; set; }
        public string Art { get; set; }
        public string Methode { get; set; }
        public string Maenge { get; set; }
        public Decimal Anzahl { get; set; }
        public Decimal RichtLeistung { get; set; }
        public string Haeufigkeit { get; set; }
        public decimal HaeufigkeitFaktor { get; set; }
        public Decimal AnzahlMonatlich { get; set; }
        public Decimal ZeitTaeglich { get; set; }
        public Decimal ZeitMonatlich { get; set; }
        public Decimal Preis { get; set; }
    }
}
