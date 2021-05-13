using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;


public class Emulator : MonoBehaviour
{
    [SerializeField]
    List<InputField> timeIn = new List<InputField>();
    [SerializeField]
    List<InputField> wineRatioIn = new List<InputField>();
    [SerializeField]
    List<InputField> judgeRatioIn = new List<InputField>();
    [SerializeField]
    List<InputField> HPrateIn = new List<InputField>();
    [SerializeField]
    List<InputField> cruise_XIn = new List<InputField>();
    [SerializeField]
    List<InputField> cruise_PIn = new List<InputField>();
    [SerializeField]
    List<InputField> HPratio_Full_MIn = new List<InputField>();
    [SerializeField]
    List<InputField> HPratio_Full_PIn = new List<InputField>();
    [SerializeField]
    List<InputField> HPratio_Lack_M_30In = new List<InputField>();
    [SerializeField]
    List<InputField> HPratio_Lack_P_30In = new List<InputField>();
    [SerializeField]
    List<InputField> wineData1In = new List<InputField>();
    [SerializeField]
    List<InputField> wineData2In = new List<InputField>();
    [SerializeField]
    List<Text> records = new List<Text>();


    //
    float timeLimit;
    int timeScale;

    float[] wineRatio = new float[12];

    float judgeRatio_P;//
    float judgeRatio_M;
    float judgeRatio_H;

    float HPrate1;
    float HPrate2;

    float cruise_X1;
    float cruise_P1;
    float cruise_X2;
    float cruise_P2;

    List<float> HPratio_Full_M = new List<float>();
    List<float> HPratio_Full_P = new List<float>();

    /*List<float> HPratio_Full_MM = new List<float>();
    List<float> HPratio_Full_PM = new List<float>();

    List<float> HPratio_Full_ML = new List<float>();
    List<float> HPratio_Full_PL = new List<float>();

    List<float> HPratio_Full_MB = new List<float>();*/

    List<float> HPratio_Lack_M_30 = new List<float>();
    List<float> HPratio_Lack_P_30 = new List<float>();

    /*List<float> HPratio_Lack_M_10 = new List<float>();
    List<float> HPratio_Lack_P_10 = new List<float>();

    List<float> HPratio_Lack_M_0 = new List<float>();
    List<float> HPratio_Lack_P_0 = new List<float>();*/


    public string name1;
    public float[] wine1 = new float[6];
    float wineRatio1;
    float HP1;
    float M1;
    float P1;
    List<float> mark1 = new List<float>();

    public string name2;
    public float[] wine2 = new float[6];
    float wineRatio2;
    float HP2;
    float M2;
    float P2;
    List<float> mark2 = new List<float>();

    int time;

    public void Set()
    {
        time = 0;
        name1 = wineData1In[0].text;
        name2 = wineData2In[0].text;
        for (int i=0;i<wineData1In.Count-1;i++)
        {
            wine1[i] = float.Parse(wineData1In[i + 1].text);
            wine2[i] = float.Parse(wineData2In[i + 1].text);
        }
        switch (name1)
        {
            case ("酱香型"):
                wineRatio1 = wineRatio[0];
                break;
            case ("浓香型"):
                wineRatio1 = wineRatio[1];
                break;
            case ("清香型"):
                wineRatio1 = wineRatio[2];
                break;
            case ("兼香型"):
                wineRatio1 = wineRatio[3];
                break;
            case ("米香型"):
                wineRatio1 = wineRatio[4];
                break;
            case ("凤香型"):
                wineRatio1 = wineRatio[5];
                break;
            case ("芝麻香型"):
                wineRatio1 = wineRatio[6];
                break;
            case ("豉香型"):
                wineRatio1 = wineRatio[7];
                break;
            case ("特香型"):
                wineRatio1 = wineRatio[8];
                break;
            case ("药香型"):
                wineRatio1 = wineRatio[9];
                break;
            case ("老白干香型"):
                wineRatio1 = wineRatio[10];
                break;
            case ("馥郁香型"):
                wineRatio1 = wineRatio[11];
                break;
        }
        switch (name2)
        {
            case ("酱香型"):
                wineRatio2 = wineRatio[0];
                break;
            case ("浓香型"):
                wineRatio2 = wineRatio[1];
                break;
            case ("清香型"):
                wineRatio2 = wineRatio[2];
                break;
            case ("兼香型"):
                wineRatio2 = wineRatio[3];
                break;
            case ("米香型"):
                wineRatio2 = wineRatio[4];
                break;
            case ("凤香型"):
                wineRatio2 = wineRatio[5];
                break;
            case ("芝麻香型"):
                wineRatio2 = wineRatio[6];
                break;
            case ("豉香型"):
                wineRatio2 = wineRatio[7];
                break;
            case ("特香型"):
                wineRatio2 = wineRatio[8];
                break;
            case ("药香型"):
                wineRatio2 = wineRatio[9];
                break;
            case ("老白干香型"):
                wineRatio2 = wineRatio[10];
                break;
            case ("馥郁香型"):
                wineRatio2 = wineRatio[11];
                break;
        }

        timeLimit = float.Parse(timeIn[0].text);
        timeScale = int.Parse(timeIn[1].text);

        for (int i=0;i<wineRatioIn.Count;i++)
        {
            wineRatio[i] = float.Parse(wineRatioIn[i].GetComponent<InputField>().text);
        }

        judgeRatio_P = float.Parse(judgeRatioIn[0].text);
        judgeRatio_M = float.Parse(judgeRatioIn[1].text);
        judgeRatio_H = float.Parse(judgeRatioIn[2].text);

        HP1 = wineRatio1 * wine1[1] + timeScale;
        HP2 = wineRatio2 * wine2[1] + timeScale;
        HPrate1 = (float)Math.Pow((float.Parse(HPrateIn[0].text) + float.Parse(HPrateIn[1].text) / (float.Parse(HPrateIn[2].text) * wine1[2])), float.Parse(HPrateIn[3].text));
        HPrate2 = (float)Math.Pow((float.Parse(HPrateIn[0].text) + float.Parse(HPrateIn[1].text) / (float.Parse(HPrateIn[2].text) * wine2[2])),float.Parse(HPrateIn[3].text));

        cruise_X1 = wine1[0] / judgeRatio_H * float.Parse(cruise_XIn[0].text);
        cruise_P1 = float.Parse(cruise_PIn[0].text) * float.Parse(cruise_PIn[1].text) * (float.Parse(cruise_PIn[3].text) - (float)Math.Pow(cruise_X1,float.Parse(cruise_PIn[2].text))) * cruise_X1;
        cruise_X2 = wine2[0] / judgeRatio_H * float.Parse(cruise_XIn[0].text);
        cruise_P2 = float.Parse(cruise_PIn[0].text) * float.Parse(cruise_PIn[1].text) * (float.Parse(cruise_PIn[3].text) - (float)Math.Pow(cruise_X2, float.Parse(cruise_PIn[2].text))) * cruise_X2;

        for(int i=0;i<HPratio_Full_MIn.Count;i++)
        {
            HPratio_Full_M.Add(float.Parse(HPratio_Full_MIn[i].text));
        }
        for(int i=0;i<HPratio_Full_PIn.Count;i++)
        {
            HPratio_Full_P.Add(float.Parse(HPratio_Full_PIn[i].text));
        }

        for(int i=0;i<HPratio_Lack_M_30In.Count;i++)
        {
            HPratio_Lack_M_30.Add(float.Parse(HPratio_Lack_M_30In[i].text));
        }
        for (int i = 0; i < HPratio_Lack_P_30In.Count; i++)
        {
            HPratio_Lack_P_30.Add(float.Parse(HPratio_Lack_P_30In[i].text));
        }
    }
    void Calculate()
    {
        float hp1 = HP1, hp2 = HP2;
        bool isdone = false;
        for (int i = 0; i < timeScale; i++)
        {
            if(i<10)
            {
                if (!isdone)
                {
                    Debug.Log(cruise_X1);
                    M1 = (float)Math.Pow(wineRatio1 * HPratio_Full_M[0] * wine1[5] * (HPratio_Full_M[1] + wine1[4] / HPratio_Full_M[2]), HPratio_Full_M[3]);
                    P1 = (HPratio_Full_P[0] + wine1[4] / HPratio_Full_P[1]) * cruise_P1 * HPratio_Full_P[2];
                    M2 = (float)Math.Pow(wineRatio2 * HPratio_Full_M[0] * wine2[5] * (HPratio_Full_M[1] + wine2[4] / HPratio_Full_M[2]), HPratio_Full_M[3]);
                    P2 = (HPratio_Full_P[0] + wine2[4] / HPratio_Full_P[1]) * cruise_P2 * HPratio_Full_P[2];
                    isdone = true;
                }
                var a1 = Guid.NewGuid().ToString();
                long sum1 = ChangePro(a1);
                var a2 = Guid.NewGuid().ToString();
                long sum2 = ChangePro(a2);
                if (sum1<=100*P1%100)
                    mark1.Add(M1);
                else
                    mark1.Add(0);
                if (sum2 <= 100 * P2%100)
                    mark2.Add(M2);
                else
                    mark2.Add(0);
            }
            else if(i<24)
            {
                if (isdone)
                {
                    Debug.Log(wine2[5]);
                    M1 = wineRatio1 * HPratio_Full_M[4] * wine1[5];
                    P1 = cruise_P1;
                    M2 = wineRatio2 * HPratio_Full_M[4] * wine2[5];
                    P2 = cruise_P2;
                    isdone = false;
                }
                var a1 = Guid.NewGuid().ToString();
                long sum1 = ChangePro(a1);
                var a2 = Guid.NewGuid().ToString();
                long sum2 = ChangePro(a2);
                if (sum1 <= 100 * P1%100)
                    mark1.Add(M1);
                else
                    mark1.Add(0);
                if (sum2 <= 100 * P2%100)
                    mark2.Add(M2);
                else
                    mark2.Add(0);
            }
            else
            {
                if (!isdone)
                {
                    M1 = wineRatio1 * HPratio_Full_M[5] * wine1[5] * (HPratio_Full_M[6] + wine1[3] / HPratio_Full_M[7]);
                    P1 = cruise_P1 * (HPratio_Full_P[3] + wine1[3] / HPratio_Full_P[4]);
                    M2 = wineRatio2 * HPratio_Full_M[5] * wine2[5] * (HPratio_Full_M[6] + wine2[3] / HPratio_Full_M[7]);
                    P2 = cruise_P2 * (HPratio_Full_P[3] + wine2[3] / HPratio_Full_P[4]);
                    isdone = true;
                }
                var a1 = Guid.NewGuid().ToString();
                long sum1 = ChangePro(a1);
                var a2 = Guid.NewGuid().ToString();
                long sum2 = ChangePro(a2);
                if (sum1 <= 100 * P1%100)
                    mark1.Add(M1);
                else
                    mark1.Add(0);
                if (sum2 <= 100 * P2%100)
                    mark2.Add(M2);
                else
                    mark2.Add(0);
            }
            hp1 -= HPrate1;
            hp2 -= HPrate2;
            if (hp1 <= 0.3 * HP1)
            {
                if(hp1 > HP1 * 0.1)
                {
                    M1 = M1 * HPratio_Lack_M_30[0] * (HPratio_Lack_M_30[1] + wine1[2] / HPratio_Lack_M_30[2]);
                    P1 = P1 * HPratio_Lack_P_30[0] * (HPratio_Lack_P_30[1] + wine1[2] / HPratio_Lack_P_30[2]);
                }
                else if(hp1 > 0)
                {
                    M1 = M1 * HPratio_Lack_M_30[3] * (HPratio_Lack_M_30[4] + wine1[2] / HPratio_Lack_M_30[5]);
                    P1 = P1 * HPratio_Lack_P_30[3] * (HPratio_Lack_P_30[4] + wine1[2] / HPratio_Lack_P_30[5]);
                }
                else if(hp1<=0)
                {
                    M1 = M1 * HPratio_Lack_M_30[6] * (HPratio_Lack_M_30[7] + wine1[2] / HPratio_Lack_M_30[8]);
                    P1 = P1 * HPratio_Lack_P_30[6] * (HPratio_Lack_P_30[7] + wine1[2] / HPratio_Lack_P_30[8]);
                }
            }
            if (hp2 <= 0.3 * HP2)
            {
                if (hp2 > HP2 * 0.1)
                {
                    M2 = M2 * HPratio_Lack_M_30[0] * (HPratio_Lack_M_30[1] + wine2[2] / HPratio_Lack_M_30[2]);
                    P2 = P2 * HPratio_Lack_P_30[0] * (HPratio_Lack_P_30[1] + wine2[2] / HPratio_Lack_P_30[2]);
                }
                else if (hp2 > 0)
                {
                    M2 = M2 * HPratio_Lack_M_30[3] * (HPratio_Lack_M_30[4] + wine2[2] / HPratio_Lack_M_30[5]);
                    P2 = P2 * HPratio_Lack_P_30[3] * (HPratio_Lack_P_30[4] + wine2[2] / HPratio_Lack_P_30[5]);
                }
                else if (hp2 <= 0)
                {
                    M2 = M2 * HPratio_Lack_M_30[6] * (HPratio_Lack_M_30[7] + wine2[2] / HPratio_Lack_M_30[8]);
                    P2 = P2 * HPratio_Lack_P_30[6] * (HPratio_Lack_P_30[7] + wine2[2] / HPratio_Lack_P_30[8]);
                }
            }
        }
        M1 = wineRatio1 * HPratio_Full_M[8] * (HPratio_Full_M[9] + wine1[3] / HPratio_Full_M[10]) * (HPratio_Full_M[11] + wine1[0] / HPratio_Full_M[12]) * (HPratio_Full_M[13] + wine1[4] / HPratio_Full_M[14]);
        M2 = wineRatio2 * HPratio_Full_M[8] * (HPratio_Full_M[9] + wine2[3] / HPratio_Full_M[10]) * (HPratio_Full_M[11] + wine2[0] / HPratio_Full_M[12]) * (HPratio_Full_M[13] + wine2[4] / HPratio_Full_M[14]);
        mark1.Add(M1);
        mark2.Add(M2);
    }
    Timer timer;
    public void Settle()
    {
        time = 0;
        mark1.Clear();
        mark2.Clear();
        records[0].text = null;
        records[1].text = null;
        records[0].text += "HP:" + HP1 + Environment.NewLine;
        records[0].text += "消耗速度:" + HPrate1 + Environment.NewLine;
        records[1].text += "HP:" + HP2 + Environment.NewLine;
        records[1].text += "消耗速度:" + HPrate2 + Environment.NewLine;
        Calculate();
        float t0 = timeLimit / timeScale;
        Debug.Log(t0);
        for(int i=0;i<timeScale+1;i++)
        {
            DownCount();
        }
        /*timer = new Timer(t0);
        timer.Elapsed += DownCount;
        timer.AutoReset = true;
        timer.Enabled = true;*/
    }
    void DownCount()
    {
        string a;
        a = "+" + mark1[time] + Environment.NewLine;
        records[0].text += a;
        a = "+" + mark2[time] + Environment.NewLine;
        records[1].text += a;
        time++;
        if(time==10)
        {
            float s1=0, s2=0;
            for(int i=0;i<10;i++)
            {
                s1 += mark1[i];
                s2 += mark2[i];
            }
            records[0].text += "前期总分:" + s1+Environment.NewLine; 
            records[1].text += "前期总分:" + s2+Environment.NewLine;
        }
        if(time== (int)(10 + (timeScale - 10) * 0.7))
        {
            float s1 = 0, s2 = 0;
            for (int i = 10; i < 10+(timeScale-10)*0.7; i++)
            {
                s1 += mark1[i];
                s2 += mark2[i];
            }
            records[0].text += "中期总分:" + s1+Environment.NewLine;
            records[1].text += "中期总分:" + s2+Environment.NewLine;
        }
        if(time==timeScale)
        {
            float s1 = 0, s2 = 0;
            for (int i = (int)(10 + (timeScale - 10) * 0.7); i < timeScale; i++)
            {
                s1 += mark1[i];
                s2 += mark2[i];
            }
            records[0].text += "后期总分:" + s1+Environment.NewLine;
            records[1].text += "后期总分;" + s2+Environment.NewLine;
        }
        if(time==timeScale+1)
        {
            float s1 = 0, s2 = 0;
            for (int i=0;i<timeScale+1;i++)
            {
                s1 += mark1[i];
                s2 += mark2[i];
            }
            records[0].text += "Bonus:" + mark1[time-1] + Environment.NewLine;
            records[1].text += "Bonus;" + mark2[time-1] + Environment.NewLine;
            records[0].text += "总分:" + s1 + Environment.NewLine;
            records[1].text += "总分;" + s2 + Environment.NewLine;
        }
        /*if(time==timeScale)
        {
            timer.Stop();
        }*/
    }

    long Change(long a)
    {
        long c = a, d, e = 0;
        int j = 1;
        while (c / 16 != 0)
        {
            d = c % 16;
            c = c / 16;
            e += d * j;
            j *= 10;
        }
        return e;
    }
    long ChangePro(string a)
    {
        try { a = a.Replace("a", "10"); } catch { }
        try { a = a.Replace("b", "11"); } catch { }
        try { a = a.Replace("c", "12"); } catch { }
        try { a = a.Replace("d", "13"); } catch { }
        try { a = a.Replace("e", "14"); } catch { }
        try { a = a.Replace("f", "15"); } catch { }
        string[] b = a.Split('-');
        long c1 = Change(long.Parse(b[0]));
        long c2 = Change(long.Parse(b[1]));
        long c3 = Change(long.Parse(b[2]));
        long c4 = Change(long.Parse(b[3]));
        b[4] = b[4].Remove(11);
        long c5 = Change(long.Parse(b[4]));
        return (c1 + c2 + c3 + c4 + c5)%100;
    }

    void Start()
    {
        Debug.Log(Math.Pow(5693.917, 0.5) + "000");
        Set();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
