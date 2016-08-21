using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Import
{
    class Program
    {
        private static CleanEstimate.Daten.Settings _Settings = new CleanEstimate.Daten.Settings();

        public static CleanEstimate.Daten.Settings Settings
        {
            get { return _Settings; }
            set { _Settings = value; }
        }


        static void Main(string[] args)
        {
            LoadSettings();
            List<dynamic> firmList = new List<dynamic>();
            List<dynamic> objList = new List<dynamic>();
            List<dynamic> kalkList = new List<dynamic>();

            Dictionary<string, CleanEstimate.Daten.Haeufigkeit> haeufigDict = new Dictionary<string, CleanEstimate.Daten.Haeufigkeit>();
            haeufigDict.Add("2x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 5));
            haeufigDict.Add("5x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 1));
            haeufigDict.Add("0", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 0));
            haeufigDict.Add("3x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 3));
            haeufigDict.Add("6x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 7));
            haeufigDict.Add("1x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 6));
            haeufigDict.Add("1x Mo.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 9));
            haeufigDict.Add("2.5x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 4));
            haeufigDict.Add("4x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 2));
            haeufigDict.Add("Alle 12 W.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 14));

            haeufigDict.Add("2x Mo.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 20));
            haeufigDict.Add("3x Mo.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 21));
            haeufigDict.Add("4x Mo.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 22));

            haeufigDict.Add("Alle 3 Mon.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 16));
            haeufigDict.Add("Alle 6 Mon.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 18));
            haeufigDict.Add("1 pro Jahr.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 19));
            haeufigDict.Add("Alle 2 Mon.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 15));
            haeufigDict.Add("Alle 2 W.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 10));
            haeufigDict.Add("7x Wö.", CleanEstimate.Daten.Haeufigkeit.GetHaeufigkeit(Settings.Haeufigkeiten, 8));

            MySqlConnection con = new MySqlConnection("Server=10.1.1.10;Database=Kalk;Uid=kalku;Pwd=starwars;");
            con.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = con;

            com.CommandText = "SELECT id, firma, stra, plz, ort, beschreibung FROM kalk.firmtabelle WHERE firma is not NULL;";

            using (var reader = com.ExecuteReader())
            {
                while(reader.Read())
                {
                    firmList.Add(new {id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Strasse = reader.GetString(2),
                        PLZ = reader.GetString(3),
                        Ort = reader.GetString(4),
                        Beschreibung = reader.GetString(5)
                    });
                }
            }

            com.CommandText = "SELECT idObj, object, beschreibung, SVS, arbeitstage, id FROM kalk.objekte WHERE object is not NULL;";
            using (var reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    objList.Add(new { firmId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Beschreibung = reader.GetString(2),
                        Stundenverrechnungssatz = reader.GetDecimal(3),
                        Arbeistage = reader.GetDecimal(4),
                        id = reader.GetInt32(5)
                    });
                }
            }

            com.CommandText = "SELECT id, idKalk, etage, bezeichner, belaege, methode, einheit, anzahl, richtleistung, haeufigkeit, mietpreis FROM kalk.kalkulation WHERE etage is not NULL;";
            using (var reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    kalkList.Add(new
                    {
                        id = reader.GetInt32(0),
                        objid = reader.GetInt32(1),

                        etage = reader.GetString(2),
                        bezeichner = reader.GetString(3),
                        belaege = reader.GetString(4),
                        methode = reader.GetString(5),
                        einheit = reader.GetString(6),
                        anzahl = reader.GetDecimal(7),
                        richtleistung = reader.IsDBNull(8) ? 0m : reader.GetDecimal(8),
                        haeufigkeit = reader.GetString(9),
                        mietpreis = reader.GetDecimal(10),
                    });
                }
            }

            foreach (var firma in firmList)
            {
                var tempFirma = new CleanEstimate.Daten.Firma();
                tempFirma.Haeufigkeiten = Settings.Haeufigkeiten;

                tempFirma.Name = firma.Name;
                tempFirma.Strasse = firma.Strasse;
                tempFirma.PLZ = firma.PLZ;
                tempFirma.Ort = firma.Ort;
                tempFirma.Beschreibung = firma.Beschreibung;

                foreach (var objekt in objList.Where(x => x.firmId == firma.id))
                {
                    var tempObjekt = new CleanEstimate.Daten.Objekt();

                    tempObjekt.Name = objekt.Name;
                    tempObjekt.Beschreibung = objekt.Beschreibung;
                    tempObjekt.Arbeistage = objekt.Arbeistage;
                    tempObjekt.Stundenverrechnungssatz = objekt.Stundenverrechnungssatz;

                    tempFirma.Objekte.Add(tempObjekt);

                    foreach (var kalkulation in kalkList.Where(x => x.objid == objekt.id))
                    {
                        var tempKalkulation = new CleanEstimate.Daten.Leistung();

                        tempKalkulation.Etage = (kalkulation.etage as string).Replace(" DG.", "DG").Replace(" DG", "DG");
                        tempKalkulation.Bezeichnung = kalkulation.bezeichner;
                        tempKalkulation.Art = kalkulation.belaege;
                        tempKalkulation.Methode = kalkulation.methode;
                        tempKalkulation.HaeufigkeitID = haeufigDict[kalkulation.haeufigkeit].ID;
                        tempKalkulation.Maenge = kalkulation.einheit;
                        tempKalkulation.Anzahl = kalkulation.anzahl;
                        tempKalkulation.RichtLeistung = kalkulation.richtleistung;
                        tempKalkulation.FixPreis = kalkulation.mietpreis;
                        tempKalkulation.Fix = kalkulation.mietpreis > 0;

                        tempObjekt.Leistungen.Add(tempKalkulation);
                    }
                }

                string path = tempFirma.Name.Trim();

                while(System.IO.File.Exists(System.IO.Path.Combine("Files", path + ".cexml")))
                {
                    path += "_";
                }

                path += ".cexml";

                path = System.IO.Path.Combine("Files", path);

                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    tempFirma.Save(fs);
                }
            }
        }

        private static void LoadSettings()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "CleanEstimate");
            string filePath = Path.Combine(directoryPath, "CleanEstimate.xml");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    Settings.Load(fs);
                }
            }
        }
    }
}
