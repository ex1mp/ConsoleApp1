using CsvHelper;
using CsvHelper.Configuration;
using Cyrillic.Convert;
using System.Globalization;

namespace ConsoleApp1
{
    public class CsvParcer
    {
        public string GetManagerSurname(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            return fileName.Substring(0, fileName.Length - 13);
        }
        public List<CsvDataContainer> GetDataFromCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ";"
            };
            using var csv = new CsvReader(reader, csvConfig);

            var records = csv.GetRecords<CsvDataContainer>();
            return records.ToList();
        }

        public List<Custom> ProcessData(List<CsvDataContainer> data)
        {var conversion = new Conversion();
            var dt = new List<Custom>();

            var i =0;
                foreach (var item in data)
            {
                var d =dt.FirstOrDefault(x=>x.Code == item.Code && x.Date.Trim() == item.Date.Trim() && x.Kasa.Trim() == item.Kasa.Trim());
                if (d != null)
                {
                    d.Name.Add(conversion.RussianCyrillicToLatin(item.Name).Trim());
                    
                }
                else
                {
                    dt.Add(new Custom()
                    {
                        Code = item.Code,
                        Date = item.Date,   
                        Kasa = item.Kasa,
                        Name = new List<string> { conversion.RussianCyrillicToLatin(item.Name).Trim() }
                    });
                }
                Console.WriteLine(i);
                i++;
            }
            
            
            var groupedData = data.GroupBy(x => x.Code);
            var result = new List<CsvDataContainerNew>();

            return dt;

        }

        public List<CsvDataContainer> ProcessDataNew(List<CsvDataContainer> data)
        {
            var conversion = new Conversion();

            foreach (var item in data)
            {
                item.Name = conversion.RussianCyrillicToLatin(item.Name).Trim().Replace(" ", "_");
            }

            return data;
        }
    }
}