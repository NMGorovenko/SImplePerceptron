using System.Globalization;
using CsvHelper;
using DataAccess.Dtos;

namespace DataAccess.DataSource;

/// <summary>
/// Data source.
/// </summary>
public class DataSource
{
    /// <summary>
    /// Get records from csv.
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<VoiceDto> GetRecordsFromCsv(string pathToFile)
    {
        var reader = new StreamReader(pathToFile);
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<VoiceMapParser>();
        var records = csv.GetRecordsAsync<VoiceDto>();

        return records;
    }
}