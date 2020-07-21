using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvImporter
{
    class Program
    {
        static void Main(string[] args)
        {
			var content = File.ReadAllText("..\\..\\..\\kusovkymagic.csv");

			// konfigurace cteni CSV
			var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				IncludePrivateMembers = false,
				Delimiter = ";",
				HasHeaderRecord = true,
				Escape = '"',
			};
			configuration.RegisterClassMap<KusovkaMapper>();
			configuration.BadDataFound = x =>
			{
				Console.Write($"- bad data: '{x.RawRecord}'");
			};
			configuration.MissingFieldFound = (headerNames, index, x) =>
			{
				Console.Write($"- field at index '{index}' was not found in: '{x.RawRecord}'.");
			};

			using (var textReader = new StringReader(content))
			using (var stringReader = new EscapeToDoubleQuoteReader(textReader))
			using (var parser = new CsvReader(stringReader, configuration))
			{
				var res = parser.GetRecords<ImportKusovka>().ToList();

				Console.Write($"READ: {res.Count} records of '{typeof(ImportKusovka).Name}'");
			}
		}
	}
}
