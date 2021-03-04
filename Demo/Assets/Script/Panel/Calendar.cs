using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{

    Sprite spring;
    Sprite summer;
    Sprite fall;
    Sprite winter;

    Sprite springText;
    Sprite summerText;
    Sprite fallText;
    Sprite winterText;

    Sprite sunny;
    Sprite rain;
    Sprite cloudy;

    Sprite sunnyText;
    Sprite rainText;
    Sprite cloudyText;

    public Image season;
    public Image seasonText;

    public Image weather;
    public Image weatherText;

    public Text temperature;
    public Text humidity;

    private void Start()
    {
        spring = StaticMethod.LoadSprite("Sprite/日历/spring");
        summer = StaticMethod.LoadSprite("Sprite/日历/summer");
        fall = StaticMethod.LoadSprite("Sprite/日历/fall");
        winter = StaticMethod.LoadSprite("Sprite/日历/winter");

        springText = StaticMethod.LoadSprite("Sprite/日历/springText");
        summerText = StaticMethod.LoadSprite("Sprite/日历/summerText");
        fallText = StaticMethod.LoadSprite("Sprite/日历/fallText");
        winterText = StaticMethod.LoadSprite("Sprite/日历/winterText");

        sunny = StaticMethod.LoadSprite("Sprite/日历/sunny");
        rain = StaticMethod.LoadSprite("Sprite/日历/rain");
        cloudy = StaticMethod.LoadSprite("Sprite/日历/cloudy");

        sunnyText = StaticMethod.LoadSprite("Sprite/日历/sunnyText");
        rainText = StaticMethod.LoadSprite("Sprite/日历/rainText");
        cloudyText = StaticMethod.LoadSprite("Sprite/日历/cloudyText");

        Set(Season.冬, Weather.阴, 20, 20);
    }
    public void Set(Season season,Weather weather,float temperature, float humidity)
    {
        Sprite seasonSpr;
        Sprite seasonText;
        switch (season)
        {
            case Season.春:
                seasonSpr = spring;
                seasonText = springText;
                break;
            case Season.夏:
                seasonSpr = summer;
                seasonText = summerText;
                break;
            case Season.秋:
                seasonSpr = fall;
                seasonText = fallText;
                break;
            case Season.冬:
                seasonSpr = winter;
                seasonText = winterText;
                break;
            default:
                seasonSpr = null;
                seasonText = null;
                break;
        }
        this.season.sprite = seasonSpr;
        this.seasonText.sprite = seasonText;

        Sprite weatherSpr;
        Sprite weatherText;
        switch (weather)
        {
            case Weather.晴:
                weatherSpr = sunny;
                weatherText = sunnyText;
                break;
            case Weather.阴:
                weatherSpr = cloudy;
                weatherText = cloudyText;
                break;
            case Weather.雨:
                weatherSpr = rain;
                weatherText = rainText;
                break;
            default:
                weatherSpr = null;
                weatherText = null;
                break;
        }
        this.weather.sprite = weatherSpr;
        this.weatherText.sprite = weatherText;

        this.temperature.text = temperature.ToString();
        this.humidity.text = humidity.ToString();
    }
}
