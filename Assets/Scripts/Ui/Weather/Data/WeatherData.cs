using System;

namespace Ui.Weather.Data
{
    // These structs are used to deserialize the GraphQL weather API response
    [Serializable]
    public struct WeatherQueryResp
    {
        public CityData data;
    }
    [Serializable]
    public struct CityData
    {
        public WeatherDataOfCity getCityByName;
    }
    [Serializable]
    public struct WeatherDataOfCity
    {
        public string name;
        public WeatherData weather;
    }
    [Serializable]
    public struct WeatherData
    {
        public WeatherSummary summary;
        public long timestamp;
    }
    [Serializable]
    public struct WeatherSummary
    {
        public string title;
        public string description;
        public string icon;
    }
}
