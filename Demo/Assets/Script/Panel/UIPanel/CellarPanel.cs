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
    List<Inclusion> inclusions = new List<Inclusion>();
    UIManager uiManager;
    StaticsFix staticsFix;
    BaseFragment fragment1 = new BaseFragment();
    BaseFragment fragment2 = new BaseFragment();
    BaseFragment fragment3 = new BaseFragment();
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
    int batch;
    int bi;
    bool isConfirm = false;
    void Non()
    {
        inclusions.Clear();
        inclusions.Add(a);
        inclusions.Add(b);
        inclusions.Add(c);
        inclusions.Add(d);
        inclusions.Add(e);
        inclusions.Add(f);
        inclusions.Add(g);
        inclusions.Add(h);
        inclusions.Add(i);
    }
    void GetBatch()
    {
        switch (batch)
        {
            case 1:
                bi = staticsFix.b1;
                break;
            case 2:
                bi = staticsFix.b2;
                break;
            case 3:
                bi = staticsFix.b3;
                break;
            case 4:
                bi = staticsFix.b4;
                break;
        }
    }
    void Check(int s)
    {
        a.value = staticsFix.baseObj[s].element.acid;
        b.value = staticsFix.baseObj[s].element.ester;
        c.value = staticsFix.baseObj[s].element.alcohol;
        d.value = staticsFix.baseObj[s].element.microbe;
        e.value = staticsFix.baseObj[s].element.yield;
        f.value = staticsFix.baseObj[s].element.taste;
        g.value = staticsFix.baseObj[s].element.advancedAcid;
        h.value = staticsFix.baseObj[s].element.advancedEster;
        i.value = staticsFix.baseObj[s].element.advancedAlcohol;
    }
    BaseFragment SetEvaluations(string name)
    {
        BaseFragment fragment = instance.fragmentDic[name];
        return fragment;
    }
    public void Init()
    {
        isConfirm = false;
        valueSet1.value = 0;
        valueSet2.value = 0;
        valueSet3.value = 0;
        batch = uiManager.buttonList[uiManager.currentUI].GetComponent<UIObject>().batch;
        GetBatch();
        Check(bi);
        float sum = staticsFix.baseObj[bi].element.acid + staticsFix.baseObj[bi].element.ester + staticsFix.baseObj[bi].element.alcohol;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1.5f, a, b, c);
    }
    void Start()
    {
        Non();
        staticsFix = GameObject.Find("Main Camera").GetComponent<StaticsFix>();
        uiManager = GameObject.Find("Canvas").transform.Find("FactoryPanel").GetComponent<UIManager>();
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet1 = gameObject.transform.Find("StatusSet1").GetComponent<Slider>();
        valueSet2 = gameObject.transform.Find("StatusSet2").GetComponent<Slider>();
        valueSet3 = gameObject.transform.Find("StatusSet3").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("酒窖Panel").transform.Find("PieChart").gameObject;
        for(int i=0;i<fragmentsOnDisc.Count;i++)
        {
            if(fragmentsOnDisc[i].name== "陈酿")
            {
                index = i;
            }
        }
        gameObject.SetActive(false);
    }
    public void Confirm()
    {
        isConfirm = true;
        if (valueSet1.value <= 0.333f)
        {
            fragment1.element = SetEvaluations("陈酿温度（低）").element;
            fragment1.evaluation = SetEvaluations("陈酿温度（低）").evaluation;
            fragment1.baseObject.review.Clear();
            fragment1.baseObject.review.Add("陈酿温度：偏低");
        }
        else if (valueSet1.value <= 0.666f)
        {
            fragment1.element = SetEvaluations("陈酿温度（中）").element;
            fragment1.evaluation = SetEvaluations("陈酿温度（中）").evaluation;
            fragment1.baseObject.review.Clear();
            fragment1.baseObject.review.Add("陈酿温度：适中");
        }
        else
        {
            fragment1.element = SetEvaluations("陈酿温度（高）").element;
            fragment1.evaluation = SetEvaluations("陈酿温度（高）").evaluation;
            fragment1.baseObject.review.Clear();
            fragment1.baseObject.review.Add("陈酿温度：偏高");
        }
        //2
        if (valueSet2.value <= 0.333f)
        {
            fragment2.element = SetEvaluations("陈酿湿度（低）").element;
            fragment2.evaluation = SetEvaluations("陈酿湿度（低）").evaluation;
            fragment2.baseObject.review.Clear();
            fragment2.baseObject.review.Add("陈酿湿度：偏低");
        }
        else if (valueSet2.value <= 0.666f)
        {
            fragment2.element = SetEvaluations("陈酿湿度（中）").element;
            fragment2.evaluation = SetEvaluations("陈酿湿度（中）").evaluation;
            fragment2.baseObject.review.Clear();
            fragment2.baseObject.review.Add("陈酿湿度：适中");
        }
        else
        {
            fragment2.element = SetEvaluations("陈酿湿度（高）").element;
            fragment2.evaluation = SetEvaluations("陈酿湿度（高）").evaluation;
            fragment2.baseObject.review.Clear();
            fragment2.baseObject.review.Add("陈酿湿度：偏高");
        }
        //3
        if (valueSet3.value <= 0.333f)
        {
            fragment3.element = SetEvaluations("陈酿时长（低）").element;
            fragment3.evaluation = SetEvaluations("陈酿时长（低）").evaluation;
            fragment3.baseObject.review.Clear();
            fragment3.baseObject.review.Add("陈酿时长：偏低");
        }
        else if (valueSet3.value <= 0.666f)
        {
            fragment3.element = SetEvaluations("陈酿时长（中）").element;
            fragment3.evaluation = SetEvaluations("陈酿时长（中）").evaluation;
            fragment3.baseObject.review.Clear();
            fragment3.baseObject.review.Add("陈酿时长：适中");
        }
        else
        {
            fragment3.element = SetEvaluations("陈酿时长（高）").element;
            fragment3.evaluation = SetEvaluations("陈酿时长（高）").evaluation;
            fragment3.baseObject.review.Clear();
            fragment3.baseObject.review.Add("陈酿时长：偏高");
        }
        fragmentsOnDisc[index].element = fragment1.element + fragment2.element + fragment3.element;
        fragmentsOnDisc[index].evaluation = fragment1.evaluation + fragment2.evaluation + fragment3.evaluation;
        fragmentsOnDisc[index].baseObject.review.Add(fragment1.baseObject.review[0]);
        fragmentsOnDisc[index].baseObject.review.Add(fragment2.baseObject.review[0]);
        fragmentsOnDisc[index].baseObject.review.Add(fragment3.baseObject.review[0]);
        Non();
        staticsFix.AddElement(inclusions, bi);
    }
    void Update()
    {
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
        }
        else if (valueSet1.value <= 0.666f)
        {
            valueChange = (valueSet1.value - 0.333f) / 0.333f;
            aa = 0.5f + 0.9f - valueChange * 0;
            bb = 0.5f + 0.7f + valueChange * 0.3f;
            cc = 0.5f + 1 - valueChange * 0.1f;
            ee = 0.6f + valueChange * 0.2f;
        }
        else
        {
            valueChange = (valueSet1.value - 0.666f) / 0.333f;
            aa = 0.5f + 0.9f + valueChange * 0.1f;
            bb = 0.5f + 1 - valueChange * 0.2f;
            cc = 0.5f + 0.9f - valueChange * 0.2f;
            ee = 0.8f + valueChange * 0.2f;
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
        }
        //3
        if (valueSet3.value <= 0.333f)
        {
            valueChange = valueSet3.value / 0.333f;
            cc3 = valueChange * 0.3f;
        }
        else if (valueSet3.value <= 0.666f)
        {
            valueChange = (valueSet3.value - 0.333f) / 0.333f;
            cc3 = 0.3f + valueChange * 0.4f;
        }
        else
        {
            valueChange = (valueSet3.value - 0.666f) / 0.333f;
            cc3 = 0.7f + valueChange * 0.3f;
        }
        if (!isConfirm)
        {
            a.value = (aa + aa2) / 2 * 0.22f + staticsFix.baseObj[bi].element.acid;
            b.value = (bb + bb2) / 2 * 0.2f + staticsFix.baseObj[bi].element.ester;
            c.value = (cc + cc2 + cc3) / 3 * 0.2f + staticsFix.baseObj[bi].element.alcohol;
            f.value = ee * 0.325f + staticsFix.baseObj[bi].element.taste;
            g.value = gg * 0.25f + staticsFix.baseObj[bi].element.advancedAcid;
            h.value = hh * 0.25f + staticsFix.baseObj[bi].element.advancedEster;
            i.value = ii * 0.25f + staticsFix.baseObj[bi].element.advancedAlcohol;
            float sum = a.value + b.value + c.value;
            barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
            pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
        }

    }
}
