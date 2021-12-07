using System;
using GraphQlClient.Core;
using GraphQlClient.EventCallbacks;
using Managers;
using TMPro;
using Ui.Weather.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Ui.Weather
{
    public class WeatherPresenter : MonoBehaviour
    {
        [SerializeField] private DayNightCycleManager _dayNightCycleManager;

        [SerializeField] private TMP_InputField LocationInput;
        [SerializeField] private Button ResetLocationButton;
        [SerializeField] private Slider TimeOfDaySlider;
        [SerializeField] private TextMeshProUGUI TimeLabel;
        [SerializeField] private TextMeshProUGUI WeatherConditionLabel;
        [SerializeField] private GraphApi _graphApi;
        
        [SerializeField] private RectTransform Content;
        private DateTime _localTime;

        private void Awake()
        {
            ResetLocationButton.onClick.AddListener(ResetLocation);
            LocationInput.onSubmit.AddListener(RequestLocationInfo);
            TimeOfDaySlider.onValueChanged.AddListener(ChangeTimeOfDay);

            ResetLocation();
        }

        private void ChangeTimeOfDay(float timeOfDay)
        {
            var hourOfDay = Mathf.RoundToInt(timeOfDay);
            _localTime = new DateTime(_localTime.Year, _localTime.Month, _localTime.Day, hourOfDay, _localTime.Minute, _localTime.Second);
            TimeLabel.text = _localTime.ToShortTimeString();
            _dayNightCycleManager.SetTimeOfDay(_localTime);
        }

        private async void RequestLocationInfo(string location)
        {
            GraphApi.Query query = _graphApi.GetQueryByName("GetCityByName", GraphApi.Query.Type.Query);
            query.SetArgs(new{name = location});
            UnityWebRequest request = await _graphApi.Post(query);

            var text = HttpHandler.FormatJson(request.downloadHandler.text);
            var response = JsonUtility.FromJson<WeatherQueryResp>(text);

            SetTimeOfDay(response.data.getCityByName);
            WeatherConditionLabel.text = response.data.getCityByName.weather.summary.title;
        }

        private void SetTimeOfDay(WeatherDataOfCity weatherData)
        {
            var epoch = new DateTime(1970,1,1,0,0,0,0);
            var gmtTime = epoch.AddSeconds(weatherData.weather.timestamp);
            _localTime = ConvertGMTForCityToLocalTime(weatherData.name, gmtTime);

            TimeOfDaySlider.SetValueWithoutNotify(_localTime.Hour);
            TimeLabel.text = _localTime.ToShortTimeString();
            _dayNightCycleManager.SetTimeOfDay(_localTime);
        }

        private void ResetLocation()
        {
            RequestLocationInfo("Hamburg");
        }

        public void Show(bool show)
        {
            Content.gameObject.SetActive(show);
        }

        private DateTime ConvertGMTForCityToLocalTime(string cityName, DateTime gmtTime)
        {
            if (cityName.Equals("Hamburg") || cityName.Equals("Berlin"))
            {
                return gmtTime.AddHours(1);
            }
            if (cityName.Equals("London"))
            {
                return gmtTime;
            }
            if (cityName.Equals("Moscow"))
            {
                return gmtTime.AddHours(3);
            }
            if (cityName.Equals("New York"))
            {
                return gmtTime.AddHours(-5);
            }
            if (cityName.Equals("Sydney"))
            {
                return gmtTime.AddHours(11);
            }
            if (cityName.Equals("Hong Kong"))
            {
                return gmtTime.AddHours(8);
            }
            Debug.LogError($"Unsupported location: {cityName}");
            return gmtTime;
        }
    }
}
