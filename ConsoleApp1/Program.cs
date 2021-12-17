// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

Console.WriteLine("Start");

var parcer = new CsvParcer();

var data = parcer.GetDataFromCsv("C:/Users/Mikalay/Desktop/dat.csv");

var records= parcer.ProcessData(data);
var recordsNew = parcer.ProcessDataNew(data);

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    // Don't write the header again.
    HasHeaderRecord = false,
};
using (var stream = File.Open("C:/Users/Mikalay/Desktop/dat1.csv", FileMode.Append))
using (var writer = new StreamWriter(stream))
using (var csv = new CsvWriter(writer, config))
{
    csv.WriteRecords(records);
}

Console.WriteLine("erewddgdfg");
