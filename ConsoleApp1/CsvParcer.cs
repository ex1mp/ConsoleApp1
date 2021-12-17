using CsvHelper;
using Cyrillic.Convert;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CsvDataContainer>();
            return records.ToList();
        }

        public List<CsvDataContainerNew> ProcessData(List<CsvDataContainer> data)
        {
            var conversion = new Conversion();
            var groupedData = data.GroupBy(x => x.Code);
            var result = new List<CsvDataContainerNew>();

            foreach (var group in groupedData)
            {
                var s = group.ToList();

                var model = new CsvDataContainerNew() { Code = group.Key, NameList = new List<string>() };

                foreach (var item in group.ToList())
                {
                    var translatedText = conversion.RussianCyrillicToLatin(item.Name).Trim().Replace(" ","_");
                    model.NameList.Add(translatedText);
                }

                result.Add(model);
            }

            Console.WriteLine();
            return result;
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