using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            CleanEstimate.Daten.Objekt objekt = new CleanEstimate.Daten.Objekt();
            objekt.Arbeistage = 6m;
            objekt.Stundenverrechnungssatz = 15.69m;

            CleanEstimate.Daten.Leistung leistung = new CleanEstimate.Daten.Leistung();
            leistung.Etage = "EG";
            leistung.Bezeichnung = "Eins";
            objekt.Leistungen.Add(leistung);

            leistung = new CleanEstimate.Daten.Leistung();
            leistung.Etage = "OG1";
            leistung.Bezeichnung = "Zwei";
            objekt.Leistungen.Add(leistung);

            using (FileStream fs = new FileStream(@"C:\Test\TestClean.cexml", FileMode.Create, FileAccess.Write))
            {
                objekt.Save(fs);
            }
        }
    }
}
