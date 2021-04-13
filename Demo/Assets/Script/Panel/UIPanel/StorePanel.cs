using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    Inclusion a = new Inclusion("酸含量", 0.5f);
    Inclusion b = new Inclusion("酯含量", 0.5f);
    Inclusion c = new Inclusion("醇含量", 0.5f);
    Inclusion d = new Inclusion("微生物", 0);
    Inclusion e = new Inclusion("产量", 0);
    Inclusion f = new Inclusion("质感", 0);
    Inclusion g = new Inclusion("高级酸", 0);
    Inclusion h = new Inclusion("高级酯", 0);
    Inclusion i = new Inclusion("高级醇", 0);
    BaseFragment fragment = new BaseFragment();
    GameManager instance;
    List<BaseFragment> fragmentsOnDisc;
    Slider valueSet;
    GameObject barChart;
    GameObject pieChart;
    float valueChange;
    int index;
    public void Init()
    {
        valueSet.value = 0;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1.5f, a, b, c);
    }
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet = GameObject.Find("储藏室Panel").transform.Find("StatusSet").GetComponent<Slider>();
        barChart = GameObject.Find("储藏室Panel").transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("储藏室Panel").transform.Find("PieChart").gameObject;
        Init();
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            if (fragmentsOnDisc[i].name == "配料")
            {
                index = i;
            }
        }
        gameObject.SetActive(false);
    }
    void SetEvaluation(string name)
    {
        fragment = instance.fragmentDic[name];
        fragmentsOnDisc[index].element = fragment.element;
        fragmentsOnDisc[index].evaluation = fragment.evaluation;
        try
        {
            name = name.Replace("（", "：偏");
            name = name.Replace("）", "");
            fragmentsOnDisc[index].baseObject.review.Add(name);
        }
        catch { }
        Debug.Log(name);
    }
    public void Confirm()
    {
        if (valueSet.value <= 0.333f)
        {
            SetEvaluation("原、辅料配比（低）");
        }
        else if (valueSet.value <= 0.666f)
        {
            SetEvaluation("原、辅料配比（中）");
        }
        else
        {
            SetEvaluation("原、辅料配比（高）");
        }
    }
    void Update()
    {
        if (valueSet.value <= 0.333f)
        {
            valueChange = valueSet.value / 0.333f;
            a.value = valueChange * 0.9f;
            b.value = valueChange * 0.9f;
            c.value = valueChange * 0.6f;
            e.value = valueChange * 0.6f;
        }
        else if (valueSet.value <= 0.666f)
        {
            valueChange = (valueSet.value - 0.333f) / 0.333f;
            a.value = 0.9f + valueChange * 0.1f;
            b.value = 0.9f + valueChange * 0;
            c.value = 0.6f + valueChange * 0.2f;
            e.value = 0.6f + valueChange * 0.2f;
        }
        else
        {
            valueChange = (valueSet.value - 0.666f) / 0.333f;
            a.value = 1 - valueChange * 0.3f;
            b.value = 0.9f + valueChange * 0.1f;
            c.value = 0.8f + valueChange * 0.2f;
            e.value = 0.8f + valueChange * 0.2f;
        }
        float sum = a.value + b.value + c.value;
        barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
    }
}
