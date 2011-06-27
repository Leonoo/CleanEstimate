using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanEstimate.Daten
{
    public class Leistung
    {
        public string Etage { get; set; }
        public string Bezeichnung { get; set; }
        public Decimal Anzahl { get; set; }
        public Decimal RichtLeistung { get; set; }
        public Decimal Haeufigkeit { get; set; }
        public Decimal AnzahlMonatlich { get; set; }
        public Decimal Preis { get; set; }
        public Decimal FixPreis { get; set; }
        public bool Fix { get; set; }
    }
}
