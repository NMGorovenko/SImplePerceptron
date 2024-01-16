using CsvHelper.Configuration;

namespace DataAccess.Dtos;

public class VoiceMapParser : ClassMap<VoiceDto>
{
    public VoiceMapParser()
    {
        Map(m => m.Meanfreq).Name("meanfreq");
        Map(m => m.Sd).Name("sd");
        Map(m => m.Median).Name("median");
        Map(m => m.Q25).Name("Q25");
        Map(m => m.Q75).Name("Q75");
        Map(m => m.IQR).Name("IQR");
        Map(m => m.Skew).Name("skew");
        Map(m => m.Kurt).Name("kurt");
        Map(m => m.Spent).Name("sp.ent");
        Map(m => m.Sfm).Name("sfm");
        Map(m => m.Mode).Name("mode");
        Map(m => m.Centroid).Name("centroid");
        Map(m => m.Meanfun).Name("meanfun");
        Map(m => m.Minfun).Name("minfun");
        Map(m => m.Maxfun).Name("maxfun");
        Map(m => m.Meandom).Name("meandom");
        Map(m => m.Mindom).Name("mindom");
        Map(m => m.Maxdom).Name("maxdom");
        Map(m => m.Dfrange).Name("dfrange");
        Map(m => m.Modindx).Name("modindx");
        Map(m => m.Label).Name("label");
    }
}