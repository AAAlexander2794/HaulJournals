using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HaulJournalsDAL.Concrete;


namespace HaulJournalsImport.Concrete
{
    /// <summary>
    /// Одноразовый клас для ввода заранее заданных координат в БД из файла формата .kml
    /// </summary>
    public class SquareCoordsImporter
    {
        public void Run()
        {
            var context = DbConnectionManager.CreateConnection(ConnectionTypes.Remote);

            var doc = XDocument.Load("../../../HaulJournalsImport/Files/квадраты.kml");
            var ns = XNamespace.Get("http://earth.google.com/kml/2.2");

            var coordinates = //doc.Element(ns + "kml").Elements("Document");
            doc.Element(ns + "kml")?.Element(ns + "Document")?.Element(ns + "Folder")?.Elements(ns + "Placemark")
            .Select(x => new PlaceMark()
                {
                    CoordinatesString = x.Element(ns + "Point")?.Element(ns + "coordinates")?.Value,
                    Name = x.Element(ns + "name")?.Value
                })
            .ToList();

            //Создаем репозиторий
            var repos = new RepositoryDatabase(ConnectionTypes.Remote);
            repos.LoadDirectories();

            var file = File.Create("C:/squaresStandart.txt");
            var writeStream = new StreamWriter(file, Encoding.GetEncoding(1251));

            foreach (var placemark in coordinates)
            {
                var square = repos.Squares.FirstOrDefault(s => s.Name == placemark.Name.Trim());
                if (square != null)
                {
                    square.TemplateCoordsN = placemark.CoordsN;
                    square.TemplateCoordsE = placemark.CoordsE;
                    writeStream.WriteLine($"{{\"{placemark.Name}\", \"{placemark.CoordsN}\", \"{placemark.CoordsE}\"}},");
                }
            }
            writeStream.Close();
            file.Close();
            ////Сохраняем изменения в репозитории
            //repos.SaveChanges();
            
        }

        

        public class PlaceMark
        {
            private string _coordinatesString;
            public string CoordinatesString
            {
                get=>_coordinatesString;
                set
                {
                    _coordinatesString = value;
                    ParseCoordinates();
                }
            }

            public string Name { get; set; }

            public decimal CoordsN { get; set; }

            public decimal CoordsE { get; set; }

            private void ParseCoordinates()
            {
                var splitedString = CoordinatesString.Split(',');
                CoordsN = decimal.Parse(splitedString[1]);
                CoordsE = decimal.Parse(splitedString[0]);
            }
        }

    }

    
}
