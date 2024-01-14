using System.Globalization;
using CsvHelper;

namespace UI.DataSource;

/// <summary>
/// Data source.
/// </summary>
public class DataSource 
{
    /// <summary>
    /// Get records from csv.
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<VoiceDto> GetRecordsFromCsv(string pathToFile)
    {
        using var reader = new StreamReader(pathToFile);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<VoiceMapParser>();
        var records = csv.GetRecords<VoiceDto>();

        return records.ToList();
    }
}