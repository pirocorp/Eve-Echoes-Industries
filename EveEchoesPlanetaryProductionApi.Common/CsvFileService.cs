namespace EveEchoesPlanetaryProductionApi.Common
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class CsvFileService
    {
        public static async IAsyncEnumerable<string> ReadCsvDataLineByLineAsync(string filePath)
        {
            await using var fileStream = new FileStream(filePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);

            var line = await streamReader.ReadLineAsync(); // First line of CSV don't hold data

            while (line != null)
            {
                line = await streamReader.ReadLineAsync();
                yield return line;
            }
        }
    }
}
