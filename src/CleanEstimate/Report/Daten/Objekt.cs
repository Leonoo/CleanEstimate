using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEstimate.Report.Daten
{
    public class Objekt
    {
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public Decimal Stundenverrechnungssatz { get; set; }
        public Decimal AverageHoursDaily { get; set; }
    }
}
