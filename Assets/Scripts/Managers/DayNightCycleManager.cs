using System;
using UnityEngine;

namespace Managers
{
    public class DayNightCycleManager : MonoBehaviour
    {
        [SerializeField] private Light _directionalLight;
        [SerializeField] private Gradient _lightColor;
        [SerializeField] private Gradient _fogColor;
        [SerializeField] private Gradient _ambientColor;

        public void SetTimeOfDay(DateTime localTime)
        {
            var timeOfDayInMinutes = localTime.Hour * 60+ localTime.Minute;
            var timeOfDayPercentage = timeOfDayInMinutes / (24 * 60f);
            _directionalLight.color = _lightColor.Evaluate(timeOfDayPercentage);
            RenderSettings.ambientLight = _ambientColor.Evaluate(timeOfDayPercentage);
            RenderSettings.fogColor = _fogColor.Evaluate(timeOfDayPercentage);
            _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timeOfDayPercentage * 360)-90, 160,0));
        }
    }
}
