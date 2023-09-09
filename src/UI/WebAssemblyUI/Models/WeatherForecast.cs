using System.Text.Json.Serialization;

namespace WebAssemblyUI.Models;

public class WeatherForecast
{
    [JsonPropertyName("daily")]
    public DailyWeather DailyWeather { get; set; }

    //public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class DailyWeather
{
    [JsonPropertyName("time")]
    public List<DateTime> Dates { get; set; }
    [JsonPropertyName("temperature_2m_min")]
    public List<double> MinTemperatures { get; set; }
    [JsonPropertyName("temperature_2m_max")]
    public List<double> MaxTemperatures { get; set; }
    [JsonPropertyName("temperature_2m_mean")]
    public List<double> MeanTemperatures { get; set; }

    [JsonPropertyName("rain_sum")]
    public List<double> RainSum { get; set; }
    [JsonPropertyName("snowfall_sum")]
    public List<double> SnowfallSum { get; set; }
    [JsonPropertyName("precipitation_hours")]
    public List<double> PrecipitationHours { get; set; }
    [JsonPropertyName("precipitation_probability_mean")]
    public List<int?> PrecipitationProbabilityMean { get; set; }
}

class DataItem
{
    public DateTime Date { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double MeanTemperature { get; set; }

    public double RainSum { get; set; }
    public double SnowfallSum { get; set; }
    public double PrecipitationHours { get; set; }
    public int? PrecipitationProbabilityMean { get; set; }
}
