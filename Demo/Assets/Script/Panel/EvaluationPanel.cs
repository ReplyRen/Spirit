using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationPanel : MonoBehaviour
{
    private float Evaluate(BaseObject obj)
    {
        float res = -1;
        string kind;
        Evaluation normal = new Evaluation();
        float normalScore = 0;
        int errorCount = 0;

        if (obj.mains.Contains(主料.麸皮))
        {
            kind = "";
        }
        else
        {
            kind = null;
        }

        switch (kind)
        {
            case "酱香型":
                normalScore = 80;
                normal.intensity = 80;
                normal.rich = 90;
                normal.continuity = 80;
                normal.fineness = 70;
                normal.flavor = 55;
                break;
            case "浓香型":
                normalScore = 70;
                normal.intensity = 70;
                normal.rich = 80;
                normal.continuity = 80;
                normal.fineness = 60;
                normal.flavor = 50;
                break;
            case "清香型":
                normalScore = 60;
                normal.intensity = 65;
                normal.rich = 60;
                normal.continuity = 60;
                normal.fineness = 50;
                normal.flavor = 50;
                break;
            case "兼香型":
                normalScore = 70;
                normal.intensity = 60;
                normal.rich = 70;
                normal.continuity = 75;
                normal.fineness = 55;
                normal.flavor = 60;
                break;
            case "米香型":
                normalScore = 75;
                normal.intensity = 50;
                normal.rich = 60;
                normal.continuity = 60;
                normal.fineness = 60;
                normal.flavor = 70;
                break;
            case "凤香型":
                normalScore = 65;
                normal.intensity = 65;
                normal.rich = 70;
                normal.continuity = 70;
                normal.fineness = 55;
                normal.flavor = 50;
                break;
            case "芝麻香型":
                normalScore = 85;
                normal.intensity = 80;
                normal.rich = 80;
                normal.continuity = 80;
                normal.fineness = 75;
                normal.flavor = 80;
                break;
            case "豉香型":
                normalScore = 70;
                normal.intensity = 60;
                normal.rich = 70;
                normal.continuity = 65;
                normal.fineness = 70;
                normal.flavor = 80;
                break;
            case "特香型":
                normalScore = 70;
                normal.intensity = 70;
                normal.rich = 60;
                normal.continuity = 60;
                normal.fineness = 60;
                normal.flavor = 75;
                break;
            case "药香型":
                normalScore = 70;
                normal.intensity = 65;
                normal.rich = 60;
                normal.continuity = 60;
                normal.fineness = 70;
                normal.flavor = 75;
                break;
            case "老白干香型":
                normalScore = 65;
                normal.intensity = 90;
                normal.rich = 65;
                normal.continuity = 65;
                normal.fineness = 65;
                normal.flavor = 50;
                break;
            case "馥香型":
                normalScore = 65;
                normal.intensity = 70;
                normal.rich = 70;
                normal.continuity = 65;
                normal.fineness = 70;
                normal.flavor = 60;
                break;
        }

        res = ReturnScore(obj.evaluation, normal, normalScore, errorCount);
        return res;


    }

    private float ReturnScore(Evaluation obj, Evaluation normal, float normalScore, int errorCount)
    {
        float P1 = ReturnScore(obj.rich, normal.rich);
        float P2 = ReturnScore(obj.continuity, normal.continuity);
        float P3 = ReturnScore(obj.fineness, normal.fineness);
        float P4 = ReturnScore(obj.intensity, normal.intensity);
        float P5 = ReturnScore(obj.flavor, normal.flavor);
        return (normalScore - 3 * errorCount) / 100 * (P1 + P2 + P3 + P4) / 4 * Mathf.Sqrt(P5);

    }
    private float ReturnScore(float a, float normal)
    {
        float P;
        float x = 0.63f * a / normal;
        P = 2.1f * (-x * x * x + 1) * x;
        return P;
    }



}
