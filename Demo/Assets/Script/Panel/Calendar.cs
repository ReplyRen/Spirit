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

    Image season;
    Image seasonText;

    Image weather;
    Image weatherText;

    Text temperature;
    Text humidity;

    private void Start()
    {
        
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
        }
    }
}
