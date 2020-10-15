using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum Season { NONE, SPRING, SUMMER, AUTUM, WINTER };
    public enum Weather { NONE, SUNNY, HOTSUN, RAIN, SNOW };

    public Season currentSeason;
    public Weather currentWeather;

    public GameObject rain;
    public GameObject snow;

    [Header("Time Setting")]
    public float seasonTime;
    public float springTime;
    public float summerTime;
    public float autumTime;
    public float winterTime;

    [Header("Light Settings")]
    public Light sunLight;

    private float defaultLightIntensity;
    public float summerLightIntensity;
    public float autumLightIntensity;
    public float winterLightIntensity;

    private Color defaultLightColor;
    public Color summerColor;
    public Color autumColor;
    public Color winterColor;

    private void Start()
    {
        currentSeason = Season.SPRING;
        currentWeather = Weather.SUNNY;

        seasonTime = springTime;

        defaultLightColor = sunLight.color;
        defaultLightIntensity = sunLight.intensity;
    }

    public void ChangeSeason(Season seasonType)
    {
        if(seasonType != currentSeason)
        {
            switch(seasonType)
            {
                case Season.SPRING:
                    currentSeason = Season.SPRING;
                    break;
                case Season.SUMMER:
                    currentSeason = Season.SUMMER;
                    break;
                case Season.AUTUM:
                    currentSeason = Season.AUTUM;
                    break;
                case Season.WINTER:
                    currentSeason = Season.WINTER;
                    break;
            }
        }
    }

    public void ChangeWeather(Weather weatherType)
    {
        if (weatherType != currentWeather)
        {
            switch (weatherType)
            {
                case Weather.SUNNY:
                    currentWeather = Weather.SUNNY;
                    
                    break;
                case Weather.HOTSUN:
                    currentWeather = Weather.HOTSUN;
                    break;
                case Weather.RAIN:
                    currentWeather = Weather.RAIN;
                    
                    break;
                case Weather.SNOW:
                    currentWeather = Weather.SNOW;
                    
                    break;
            }
        }
    }

    private void Update()
    {
        seasonTime -= Time.deltaTime;

        if(currentSeason == Season.SPRING)
        {
            ChangeWeather(Weather.SUNNY);

            LerpSunIntensity(sunLight, defaultLightIntensity);
            LerpLightColor(sunLight, defaultLightColor);

            if(seasonTime <= 0f)
            {
                ChangeSeason(Season.SUMMER);
                seasonTime = summerTime;
            }
        }

        if(currentSeason == Season.SUMMER)
        {
            ChangeWeather(Weather.HOTSUN);

            LerpSunIntensity(sunLight, summerLightIntensity);
            LerpLightColor(sunLight, summerColor);

            if (seasonTime <= 0f)
            {
                ChangeSeason(Season.AUTUM);
                seasonTime = autumTime;
            }
        }

        if (currentSeason == Season.AUTUM)
        {
            ChangeWeather(Weather.RAIN);

            LerpSunIntensity(sunLight, autumLightIntensity);
            LerpLightColor(sunLight, autumColor);

            if (seasonTime <= 0f)
            {
                ChangeSeason(Season.WINTER);
                seasonTime = winterTime;
            }
        }

        if (currentSeason == Season.WINTER)
        {
            ChangeWeather(Weather.SNOW);

            LerpSunIntensity(sunLight, winterLightIntensity);
            LerpLightColor(sunLight, winterColor);

            if (seasonTime <= 0f)
            {
                ChangeSeason(Season.SPRING);
                seasonTime = springTime;
            }
        }
    }

    private void LerpLightColor(Light light, Color c)
    {
        light.color = Color.Lerp(light.color, c, 0.2f * Time.deltaTime);
    }

    private void LerpSunIntensity(Light light, float intensity)
    {
        light.intensity = Mathf.Lerp(light.intensity, intensity, 0.2f * Time.deltaTime);
    }
}