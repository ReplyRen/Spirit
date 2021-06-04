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

    float timeRange = 30;


    void ReadJudgesData()
    {

    }
    void ReadWineData()
    {

    }
    public void GetWine(BaseObject baseObject)
    {

    }
    void CalculateScore(Wine wine,Judge judge)
    {
        wine.time = timeRange;
        wine.expectStr = judge.exceptStrength;
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
                J_M = 1 + (0.1f * judge.favor / judge.exceptStrength);
                J_P = 0.5f * (0.1f * judge.favor / judge.exceptStrength);
            }
            else
            {
                J_M = 1 + (0.1f * judge.favor / judge.exceptStrength);
                J_P = (1 - P) * 0.2f * (0.1f * judge.favor / judge.exceptStrength);
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
            }
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
            a.Remove(a[b]);
            wines.Add(winesE[b]);
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
            a.Remove(a[b]);
            judges.Add(commonJudges[b]);
        }
    }
    public struct Score
    {
        public float score;
        public bool isZero;
    }
}
