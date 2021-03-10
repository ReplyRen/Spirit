using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixPanel : MonoBehaviour
{
    Inclusion a = new Inclusion("酸含量", 0.5f);
    Inclusion b = new Inclusion("酯含量", 0.5f);
    Inclusion c = new Inclusion("醇含量", 0.5f);
    Inclusion d = new Inclusion("微生物含量", 0);
    Inclusion e = new Inclusion("产量", 0);
    Inclusion f = new Inclusion("质感", 0);
    Inclusion g = new Inclusion("高级酸", 0);
    Inclusion h = new Inclusion("高级酯", 0);
    Inclusion i = new Inclusion("高级醇", 0);
    GameManager instance;
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc = new List<BaseFragment>();
    GameObject barChart;
    GameObject pieChart;
    Slider valueSet;
    float valueChange;
    int index = -1;
    void Init()
    {
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1, b, c);
        valueSet.value = 0;
    }
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet = gameObject.transform.Find("StatusSet").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("调酒室Panel").transform.Find("PieChart").gameObject;
        Init();
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            if (fragmentsOnDisc[i].name == "勾兑勾调")
            {
                index = i;
            }
        }
        if (index >= 0) SetEvaluation("勾兑勾调");
        gameObject.SetActive(false);
    }
    void SetEvaluation(string name)
    {
        fragment = instance.fragmentDic[name];
        fragmentsOnDisc[index].element = fragment.element;
        fragmentsOnDisc[index].evaluation = fragment.evaluation;
    }
    void Update()
    {
        if (valueSet.value <= 0.333f)
        {
            valueChange = valueSet.value / 0.333f;
            b.value = 0.5f + valueChange * 0.5f;
            c.value = 0.5f + valueChange * 0.4f;
            d.value = valueChange * 0.3f;
        }
        else if (valueSet.value <= 0.666f)
        {
            valueChange = (valueSet.value - 0.333f) / 0.333f;
            b.value = 0.5f + 0.5f + valueChange * 0.2f;
            c.value = 0.5f + 0.4f + valueChange * 0.3f;
            d.value = 0.3f + valueChange * 0.2f;
        }
        else
        {
            valueChange = (valueSet.value - 0.666f) / 0.333f;
            b.value = 0.5f + 0.7f + valueChange * 0.3f;
            c.value = 0.5f + 0.7f - valueChange * 0.2f;
            d.value = 0.5f + valueChange * 0.4f;
        }
        float sum = b.value + c.value;
        barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().UpdateChart(sum, b, c);
    }
}
