using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WineProgress : MonoBehaviour
{
    [SerializeField]
    Bazzar bazzar;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Text name;
    [SerializeField]
    Text wineScore;
    [SerializeField]
    UIObject UIObject;
    [SerializeField]
    Match match;
    float score = 0;
    float maxScore = 0;
    int b = 0, c = 0;
    int inn = 0;
    string s1=null, s2=null;

    void Number()
    {
        int a = (int)(score / maxScore * 100);
        if (b < a / 10)
        {
            b = a / 10;
            switch (b)
            {
                case 0:
                case 1:
                    s1 = "拾";
                    break;
                case 2:
                    s1 = "贰";
                    break;
                case 3:
                    s1 = "叁";
                    break;
                case 4:
                    s1 = "肆";
                    break;
                case 5:
                    s1 = "伍";
                    break;
                case 6:
                    s1 = "陆";
                    break;
                case 7:
                    s1 = "柒";
                    break;
                case 8:
                    s1 = "捌";
                    break;
                case 9:
                    s1 = "玖";
                    break;
            }
        }
        c = a % 10;
        switch (c)
        {
            case 0:
                s2 = null;
                break;
            case 1:
                s2 = "壹";
                break;
            case 2:
                s2 = "贰";
                break;
            case 3:
                s2 = "叁";
                break;
            case 4:
                s2 = "肆";
                break;
            case 5:
                s2 = "伍";
                break;
            case 6:
                s2 = "陆";
                break;
            case 7:
                s2 = "柒";
                break;
            case 8:
                s2 = "捌";
                break;
            case 9:
                s2 = "玖";
                break;
        }
        if (a != 0)
        {
            if (b > 1) wineScore.text = s1 + "拾" + s2;
            else wineScore.text = s1 + s2;
        }
    }
    public void AddScore(float s)
    {
        maxScore = bazzar.max;
        score = s;
    }
    public void SetStatus()
    {
        //match.wines.Add(bazzar.winesP[0]);
        //name.text = match.wines[UIObject.index].name;
        maxScore = bazzar.max;
        wineScore.text = "零";
    }
    public void Close()
    {
        score = 0;
        slider.value = 0;
        isOver = false;
    }
    void Start()
    {
        slider.value = 0;
    }
    float t = 0.1f;
    public bool isOver = false;
    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        if(t<=0&&slider.value< score / maxScore)
        {
            slider.value += score / maxScore * 0.2f;
            t = 0.1f;
        }
        if(slider.value>= score / maxScore &&!isOver)
        {
            Number();
            isOver = true;
        }
    }
}
