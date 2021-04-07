using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationPanel : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text kindText;
    [SerializeField]
    private Text evaluateText;
    [SerializeField]
    private Text reviewText;

    [SerializeField]
    private GameObject ePanel;
    [SerializeField]
    private GameObject bPanel;
    private void Start()
    {
        Test();
    }
    private void Test()
    {
        BaseObject obj = new BaseObject();
        obj.evaluation = new Evaluation();
        obj.evaluation.intensity = 30;
        obj.evaluation.rich = 30;
        obj.evaluation.continuity = 30;
        obj.evaluation.fineness = 30;
        obj.evaluation.flavor = 30;
        obj.mains.Add(主料.麸皮);
        obj.review.Add("发酵时间：偏高");
        obj.review.Add("发酵温度：偏高");
        Evaluate(obj);
    }
    public void Init(BaseObject obj)
    {
        gameObject.SetActive(true);
        Evaluate(obj);
    }
    private void Evaluate(BaseObject obj)
    {
        float score = GetScore(obj)*100;
        if (score >= 90)
            evaluateText.text = "五维俱佳，乃神仙之酒";
        else if (score >= 80)
            evaluateText.text = "味香酒醇，属王公之酒";
        else if (score >= 60)
            evaluateText.text = "品味适度，为市井之酒";
        else
            evaluateText.text = "诸味不调，当弃之";
        scoreText.text = score.ToString();
        kindText.text = "品类：" + obj.GetKind().ToString();
        gameObject.GetComponentInChildren<EvaluationChart>().Init(obj.evaluation);
        reviewText.text = "";
        foreach (var a in obj.review)
        {
            reviewText.text += a+"\n";
        }

    }
    private float GetScore(BaseObject obj)
    {
        float res = -1;
        string kind = "";
        Evaluation normal = new Evaluation();
        float normalScore = 0;
        int errorCount = 0;

        kind = obj.GetKind().ToString();

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
            default:
                Debug.LogError("酒类型为空");
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
        float x = (P1 + P2 + P3 + P4) / 4 * Mathf.Sqrt(P5);
        float res;
        res = Mathf.Pow(x, 50 / (normalScore - 3 * errorCount));
        if (res > 100)
            res = 100;
        if (res < 0)
            res = 0;
        return res;
    }
    private float ReturnScore(float a, float normal)
    {
        float P;
        float x = 0.63f * a / normal;
        P = 2.1f * (-x * x * x + 1) * x;
        return P;
    }

    public void EClick()
    {
        ePanel.SetActive(true);
        bPanel.SetActive(false);
    }
    public void BClick()
    {
        if (GuideControl.id == 18)
            GuideControl.id = 508;
        ePanel.SetActive(false);
        bPanel.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }



}
