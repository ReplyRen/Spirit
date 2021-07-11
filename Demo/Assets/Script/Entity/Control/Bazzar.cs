using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class Bazzar : MonoBehaviour
{
    int date = 0;

    List<Judge> mainJudges = new List<Judge>();
    List<Judge> commonJudges = new List<Judge>();
    List<Judge> judges = new List<Judge>();
    List<Wine> winesE = new List<Wine>();
    List<Wine> winesP = new List<Wine>();
    List<Wine> wines = new List<Wine>();
    public List<Score> scores;
    public List<List<Score>> judgeScore;
    public List<List<List<Score>>> wineScore;

    float timeRange = 30;


    void DataDecode(string temp,int kind)
    {
        string[] ss = temp.Split('|');
        if(kind==1)
        {
            Judge judge = new Judge();
            judge.prefers = new List<int>();
            judge.name = ss[0];
            judge.exceptStrength = float.Parse(ss[2]);
            for(int i=3;i<ss.Length-1;i++)
            {
                int a = int.Parse(ss[i]);
                judge.prefers.Add(a); 
            }
            if (ss[1] == "1")
            {
                judge.isMain = true;
                mainJudges.Add(judge);
            }
            else
            {
                judge.isMain = false;
                commonJudges.Add(judge);
            }
        }
        if(kind==2)
        {
            BaseObject baseobj = new BaseObject();
            baseobj.evaluation = new Evaluation();
            baseobj.name = ss[0];
            baseobj.evaluation.intensity = float.Parse(ss[2]);
            baseobj.evaluation.rich = float.Parse(ss[3]);
            baseobj.evaluation.continuity = float.Parse(ss[4]);
            baseobj.evaluation.fineness = float.Parse(ss[5]);
            baseobj.evaluation.flavor = float.Parse(ss[6]);
            Wine w = new Wine();
            w.primaryScore = float.Parse(ss[1]);
            w.obj = baseobj;
            w.Calculate();
            winesE.Add(w);
        }
    }
    void ReadJudgesData()
    {
        TextAsset data = Resources.Load("Data/JudegspreferData") as TextAsset;
        string[] s1 = data.text.Split('\n');
        for (int i = 0; i < s1.Length - 1; i++)
        {
           // Debug.Log(s1[i]);
            DataDecode(s1[i], 1);
        }

    }
    void ReadWineData()
    {
        TextAsset data = Resources.Load("Data/WineData") as TextAsset;
        string[] s1 = data.text.Split('\n');
        for (int i = 0; i < s1.Length - 1; i++)
        {
            DataDecode(s1[i], 2);
        }
    }
    public void GetWine(Wine w)
    {
        wines.Add(w);
    }
    void CalculateScore(Wine wine,Judge judge)
    {
        wine.time = timeRange;
        wine.expectStr = judge.exceptStrength;
        wine.Calculate();
        float J_M, J_P;
        float P, M;
        Score ss = new Score();
        timeRange = (timeRange - 10) * 1.2f + 10;
        for (int i = 0; i < timeRange + 1; i++)
        {
            if (i < 10)
            {
                M = wine.mark_B;
                P = wine.P_B;
            }
            else if (i < 10 + (timeRange - 10) / 1.2f * 0.8f)
            {
                M = wine.mark_M;
                P = wine.P_M;
            }
            else if (i < timeRange)
            {
                M = wine.mark_L;
                P = wine.P_L;
            }
            else
            {
                M = wine.bonus;
                P = 1;
            }
            wine.HP -= wine.HPrate;
            if (wine.HP >= wine.HP * 0.3)
            {
                M = M;
                P = P;

            }
            else if (wine.HP >= wine.HP * 0.1)
            {
                M = M * 0.6f * (1 + wine.obj.evaluation.continuity / 1000);
                P = P * 0.4f * (1 + wine.obj.evaluation.continuity / 600);
            }
            else if (wine.HP > 0)
            {
                M = M * 0.3f * (1 + wine.obj.evaluation.continuity / 700);
                P = P * 0.25f * (1 + wine.obj.evaluation.continuity / 300);
            }
            else
            {
                M = M * 0.1f * (1 + wine.obj.evaluation.continuity / 100);
                P = P * 0.15f * (1 + wine.obj.evaluation.continuity / 50);
            }
            if (judge.isMain)
            {
                float i1 = 0 ;
                for(int k=0;k<5;k++)
                {
                    //if(judge.prefers[i]==1)
                    //{
                        i1 += judge.prefers[i] * 0.1f * wine.statics[i] / judge.exceptStrength;
                   // }
                }
                J_M = 1 + i1;
                J_P = 0.5f * i1;
            }
            else
            {
                float i1 = 0;
                for (int k = 0; k < 5; k++)
                {
                    //if (judge.prefers[i] == 1)
                    //{
                    i1 += judge.prefers[i] * 0.1f * wine.statics[i] / judge.exceptStrength;
                   // }
                }
                J_M = 1 + i1;
                J_P = (1 - P) * 0.2f * i1;
            }
            M *= J_M;
            P = (1 - P) * J_P + P;
            int a = UnityEngine.Random.Range(1, 101);
            if(a<=100*P)
            {
                ss.score = M;
                ss.isZero = false;
                scores.Add(ss);
            }
            else
            {
                ss.score = 0;
                ss.isZero = true;
                scores.Add(ss);
            }
        }
    }
    void Confirm()
    {
        scores.Clear();
        for(int i=0;i<wines.Count;i++)
        {
            for(int j=0;j<judges.Count;j++)
            {
                CalculateScore(wines[i], judges[j]);
                judgeScore.Add(scores);
                scores.Clear();
            }
            wineScore.Add(judgeScore);
        }
    }
    void CreateMatch()
    {
        RollJudge();
        RollWine();
    }
    public void Test()
    {
        RollJudge();
        for (int i = 0; i < judges.Count; i++)
        {
            Debug.Log(judges[i]);
        }
    }
    void Start()
    {
        ReadJudgesData();
        ReadWineData();
        RollJudge();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RollWine()
    {
        wines.Clear();
        List<int> a = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            a.Add(i);
        }
        for (int i = 0; i < 4; i++)
        {
            int b = UnityEngine.Random.Range(0, a.Count);
            wines.Add(winesE[a[b]]);
            a.Remove(a[b]);
        }
    }
    void RollJudge()
    {
        judges.Clear();
        List<int> a=new List<int>();
        for(int i=0;i<10;i++)
        {
            a.Add(i);
        }
        int a1=UnityEngine.Random.Range(0,5);
        judges.Add(mainJudges[a1]);
        for(int i=0;i<4;i++)
        {
            int b = UnityEngine.Random.Range(0, a.Count);
            judges.Add(commonJudges[a[b]]);
            a.Remove(a[b]);

        }
    }
    public struct Score
    {
        public float score;
        public bool isZero;
    }
}
