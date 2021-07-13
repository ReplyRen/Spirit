using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;


public class Wine : MonoBehaviour
{
    public BaseObject obj;
    public string name;
    public float primaryScore;

    public float time = 0;
    public float HP = 0;
    public float cruiseP = 0;
    public float HPrate = 0;
    public float expectStr = 0;

    public float mark_B;
    public float P_B;
    public float mark_M;
    public float P_M;
    public float mark_L;
    public float P_L;
    public float bonus;

    public List<float> statics = new List<float>();

    
    void GetStatics()
    {
        name = obj.name;
        statics.Add(obj.evaluation.intensity);
        statics.Add(obj.evaluation.rich);
        statics.Add(obj.evaluation.continuity);
        statics.Add(obj.evaluation.fineness);
        statics.Add(obj.evaluation.flavor);
        Debug.Log("1112|||3");
    }
    public void Calculate()
    {
        GetStatics();
        float a = GetKind();
        HP = a * obj.evaluation.rich + time;
        HPrate = 1 + 200 / (float)Math.Pow(1000 * obj.evaluation.continuity, 0.5f);
        cruiseP = 0.75f * 2.1f * (1 - (float)Math.Pow(obj.evaluation.intensity / expectStr * 0.63, 3)) * obj.evaluation.intensity / expectStr * 0.63f;
        mark_B = a * 100 * primaryScore * (float)Math.Pow((1 + obj.evaluation.flavor / 120), 0.5);
        P_B = (1 + obj.evaluation.flavor / 500) * cruiseP * 1.05f;
        mark_M = a * 100 * primaryScore;
        P_M = cruiseP;
        mark_L = a * 100 * primaryScore * (1 + obj.evaluation.fineness / 200);
        P_L = cruiseP * (1 + obj.evaluation.fineness / 150);
        bonus = a * 300 * (1 + obj.evaluation.fineness / 50) * (1 + obj.evaluation.intensity / 200) * (1 + obj.evaluation.flavor / 500);
    }

    float GetKind()
    {
        switch (obj.GetKind().ToString())
        {
            case "酱香型":
                return (float)Kinds.酱香型;
            case "浓香型":
                return (float)Kinds.浓香型;
            case "清香型":
                return (float)Kinds.清香型;
            case "兼香型":
                return (float)Kinds.兼香型;
            case "米香型":
                return (float)Kinds.米香型;
            case "凤香型":
                return (float)Kinds.凤香型;
            case "芝麻香型":
                return (float)Kinds.芝麻香型;
            case "豉香型":
                return (float)Kinds.豉香型;
            case "特香型":
                return (float)Kinds.特香型;
            case "药香型":
                return (float)Kinds.药香型;
            case "老白干香型":
                return (float)Kinds.老白干香型;
            case "馥香型":
                return (float)Kinds.馥郁香型;
            default:
                return 0;
        }
    }
    private void Start()
    {
    }
    enum Kinds
    {
        酱香型 = 1,
        浓香型 = 1,
        清香型 = 1,
        兼香型 = 1,
        米香型 = 1,
        凤香型 = 1,
        芝麻香型 = 1,
        豉香型 = 1,
        特香型 = 1,
        药香型 = 1,
        老白干香型 = 1,
        馥郁香型 = 1
    }
}
