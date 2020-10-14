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

    [Header("Time Setting")]
    public float seasonTime;
    public float springTime;
    public float summerTime;
    public float autumTime;
    public float winterTime;

    public int currentYear;

    private void Start()
    {
        this.currentSeason = Season.SPRING;
        this.currentWeather = Weather.SUNNY;
        this.currentYear = 1;

        this.seasonTime = this.springTime;
    }

    public void ChangeSeason(Season seasonType)
    {
        if(seasonType != this.currentSeason)
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
        if (weatherType != this.currentWeather)
        {
            switch (weatherType)
            {
                case Weather.SUNNY:
                    currentSeason = Season.SPRING;
                    break;
                case Weather.HOTSUN:
                    currentSeason = Season.SUMMER;
                    break;
                case Weather.RAIN:
                    currentSeason = Season.AUTUM;
                    break;
                case Weather.SNOW:
                    currentSeason = Season.WINTER;
                    break;
            }
        }
    }

    private void Update()
    {
        this.seasonTime -= Time.deltaTime;

        if(this.currentSeason == Season.SPRING)
        {
            ChangeWeather(Weather.SUNNY);

            if(this.seasonTime <= 0f)
            {
                ChangeSeason(Season.SUMMER);
                this.seasonTime = this.summerTime;
            }
        }

        if(this.currentSeason == Season.SUMMER)
        {
            ChangeWeather(Weather.HOTSUN);

            if (this.seasonTime <= 0f)
            {
                ChangeSeason(Season.AUTUM);
                this.seasonTime = this.autumTime;
            }
        }

        if (this.currentSeason == Season.AUTUM)
        {
            ChangeWeather(Weather.RAIN);

            if (this.seasonTime <= 0f)
            {
                ChangeSeason(Season.WINTER);
                this.seasonTime = this.winterTime;
            }
        }

        if (this.currentSeason == Season.WINTER)
        {
            ChangeWeather(Weather.SNOW);

            if (this.seasonTime <= 0f)
            {
                ChangeSeason(Season.SPRING);
                this.seasonTime = this.springTime;
            }
        }
    }
}