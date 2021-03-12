using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellarPanel : MonoBehaviour
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
    GameManager instance;
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc;
    Slider valueSet1;
    Slider valueSet2;
    Slider valueSet3;
    GameObject barChart;
    GameObject pieChart;
    float valueChange;
    int index;
    BaseFragment SetEvaluations(string name)
    {
        BaseFragment fragment = instance.fragmentDic[name];
        return fragment;
    }
    public void Init()
    {
        valueSet1.value = 0;
        valueSet2.value = 0;
        valueSet3.value = 0;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1.5f, a, b, c);
    }
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet1 = gameObject.transform.Find("StatusSet1").GetComponent<Slider>();
        valueSet2 = gameObject.transform.Find("StatusSet2").GetComponent<Slider>();
        valueSet3 = gameObject.transform.Find("StatusSet3").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("酒窖Panel").transform.Find("PieChart").gameObject;
        Init();
        for(int i=0;i<fragmentsOnDisc.Count;i++)
        {
            if(fragmentsOnDisc[i].name== "陈酿")
            {
                index = i;
            }
        }
        gameObject.SetActive(false);
    }
    void Update()
    {
        BaseFragment fragment1 = new BaseFragment();
        BaseFragment fragment2 = new BaseFragment();
        BaseFragment fragment3 = new BaseFragment();
        float aa, bb, cc, ee, gg, hh, ii;
        float aa2, bb2, cc2, ee2;
        float cc3;
        if (valueSet1.value <= 0.333f)
        {
            valueChange = valueSet1.value / 0.333f;
            aa = 0.5f + valueChange * 0.9f;
            bb = 0.5f + valueChange * 0.7f;
            cc = 0.5f + valueChange * 1;
            ee = valueChange * 0.6f;
            fragment1.element = SetEvaluations("陈酿温度（低）").element;
            fragment1.evaluation = SetEvaluations("陈酿温度（低）").evaluation;
        }
        else if (valueSet1.value <= 0.666f)
        {
            valueChange = (valueSet1.value - 0.333f) / 0.333f;
            aa = 0.5f + 0.9f - valueChange * 0;
            bb = 0.5f + 0.7f + valueChange * 0.3f;
            cc = 0.5f + 1 - valueChange * 0.1f;
            ee = 0.6f + valueChange * 0.2f;
            fragment1.element = SetEvaluations("陈酿温度（中）").element;
            fragment1.evaluation = SetEvaluations("陈酿温度（中）").evaluation;
        }
        else
        {
            valueChange = (valueSet1.value - 0.666f) / 0.333f;
            aa = 0.5f + 0.9f + valueChange * 0.1f;
            bb = 0.5f + 1 - valueChange * 0.2f;
            cc = 0.5f + 0.9f - valueChange * 0.2f;
            ee = 0.8f + valueChange * 0.2f;
            fragment1.element = SetEvaluations("陈酿温度（高）").element;
            fragment1.evaluation = SetEvaluations("陈酿温度（高）").evaluation;
        }
        //2
        if (valueSet2.value <= 0.333f)
        {
            valueChange = valueSet2.value / 0.333f;
            aa2 = 0.5f + valueChange * 0.7f;
            bb2 = 0.5f + valueChange * 0.9f;
            cc2 = 0.5f + valueChange * 1;
            ee2 = valueChange * 0.6f;
            gg = valueChange * 0.5f;
            hh = valueChange * 0.3f;
            ii = valueChange * 0.3f;
            fragment2.element = SetEvaluations("陈酿湿度（低）").element;
            fragment2.evaluation = SetEvaluations("陈酿湿度（低）").evaluation;
        }
        else if (valueSet2.value <= 0.666f)
        {
            valueChange = (valueSet2.value - 0.333f) / 0.333f;
            aa2 = 0.5f + 0.7f + valueChange * 0.3f;
            bb2 = 0.5f + 0.9f + valueChange * 0.1f;
            cc2 = 0.5f + 1 - valueChange * 0.3f;
            ee2 = 0.6f + valueChange * 0.2f;
            gg = 0.5f + valueChange * 0;
            hh = 0.3f + valueChange * 0;
            ii = 0.3f + valueChange * 0;
            fragment2.element = SetEvaluations("陈酿湿度（中）").element;
            fragment2.evaluation = SetEvaluations("陈酿湿度（中）").evaluation;
        }
        else
        {
            valueChange = (valueSet2.value - 0.666f) / 0.333f;
            aa2 = 0.5f + 1 - valueChange * 0.2f;
            bb2 = 0.5f + 1 - valueChange * 0.1f;
            cc2 = 0.5f + 0.7f + valueChange * 0.2f;
            ee2 = 0.8f + valueChange * 0.2f;
            gg = 0.5f + valueChange * 0.5f;
            hh = 0.3f + valueChange * 0.7f;
            ii = 0.3f + valueChange * 0.7f;
            fragment2.element = SetEvaluations("陈酿湿度（高）").element;
            fragment2.evaluation = SetEvaluations("陈酿湿度（高）").evaluation;
        }
        //3
        if (valueSet3.value <= 0.333f)
        {
            valueChange = valueSet3.value / 0.333f;
            cc3 = valueChange * 0.3f;
            fragment3.element = SetEvaluations("陈酿时长（低）").element;
            fragment3.evaluation = SetEvaluations("陈酿时长（低）").evaluation;
        }
        else if (valueSet3.value <= 0.666f)
        {
            valueChange = (valueSet3.value - 0.333f) / 0.333f;
            cc3 = 0.3f + valueChange * 0.4f;
            fragment3.element = SetEvaluations("陈酿时长（中）").element;
            fragment3.evaluation = SetEvaluations("陈酿时长（中）").evaluation;
        }
        else
        {
            valueChange = (valueSet3.value - 0.666f) / 0.333f;
            cc3 = 0.7f + valueChange * 0.3f;
            fragment3.element = SetEvaluations("陈酿时长（高）").element;
            fragment3.evaluation = SetEvaluations("陈酿时长（高）").evaluation;
        }
        a.value = (aa + aa2) / 2;
        b.value = (bb + bb2) / 2;
        c.value = (cc + cc2 + cc3) / 3;
        f.value = ee;
        g.value = gg;
        h.value = hh;
        i.value = ii;
        float sum = a.value + b.value + c.value;
        barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
        fragmentsOnDisc[index].element = fragment1.element + fragment2.element + fragment3.element;
        fragmentsOnDisc[index].evaluation = fragment1.evaluation + fragment2.evaluation + fragment3.evaluation;
    }
}
