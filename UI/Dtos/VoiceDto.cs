using System.Collections;
using Domain;
using Domain.Interfaces;

namespace UI.Dtos;

public class VoiceDto : IInputData
{
    public double Meanfreq { get; set; }
    public double Sd { get; set; }
    public double Median { get; set; }
    public double Q25 { get; set; }
    public double Q75 { get; set; }
    public double IQR { get; set; }
    public double Skew { get; set; }
    public double Kurt { get; set; }
    public double Spent { get; set; }
    public double Sfm { get; set; }
    public double Mode { get; set; }
    public double Centroid { get; set; }
    public double Meanfun { get; set; }
    public double Minfun { get; set; }
    public double Maxfun { get; set; }
    public double Meandom { get; set; }
    public double Mindom { get; set; }
    public double Maxdom { get; set; }
    public double Dfrange { get; set; }
    public double Modindx { get; set; }
    public string Label { get; set; }
    
    
    public int GetAnswer()
    {
        if (Label == "male")
        {
            return 1;
        }
        
        return -1;
    }

    public Sensor[] GetSensors()
    {
        var properties = GetType().GetProperties();
        
        var result = properties
            .Where(prop => prop.PropertyType == typeof(double))
            .Select(prop => new Sensor(prop.Name, (double)prop.GetValue(this)))
            .ToArray();

        return result;
    }
}