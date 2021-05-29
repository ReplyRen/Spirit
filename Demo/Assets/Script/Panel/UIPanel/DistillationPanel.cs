using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistillationPanel : MonoBehaviour
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
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc;
    GameManager instance;
    Slider valueSet1;
    Slider valueSet2;
    Slider valueSet;
    GameObject barChart;
    GameObject pieChart;
    float valueChange;
    int index;
    int status;
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
    void SetEvaluation(string name)
    {
        fragment = instance.fragmentDic[name];
        fragmentsOnDisc[index].element = fragment.element;
        fragmentsOnDisc[index].evaluation = fragment.evaluation;
    }
    BaseFragment SetEvaluations(string name)
    {
        BaseFragment fragment = instance.fragmentDic[name];
        return fragment;
    }
    public void OpenPanel()
    {
        for(int i=0;i<fragmentsOnDisc.Count;i++)
        {
            switch(fragmentsOnDisc[i].name)
            {
                case "蒸馏":
                    valueSet1.gameObject.SetActive(true);
                    valueSet2.gameObject.SetActive(true);
                    valueSet.gameObject.SetActive(false);
                    barChart.SetActive(true);
                    pieChart.SetActive(true);
                    status = 1;
                    index = i;
                    break;
                case "看花摘酒":
                    valueSet1.gameObject.SetActive(false);
                    valueSet2.gameObject.SetActive(false);
                    valueSet.gameObject.SetActive(true);
                    barChart.SetActive(true);
                    pieChart.SetActive(false);
                    status = 2;
                    index = i;
                    break;
            }
        }
    }
    public void Confirm()
    {
        isConfirm = true;
        switch (status)
        {
            case 1:
                if (valueSet1.value <= 0.333f)
                {
                    fragment1.element = SetEvaluations("蒸馏温度（低）").element;
                    fragment1.evaluation = SetEvaluations("蒸馏温度（低）").evaluation;
                    fragment1.baseObject.review.Clear();
                    fragment1.baseObject.review.Add("蒸馏温度：偏低");
                }
                else if (valueSet1.value <= 0.666f)
                {
                    fragment1.element = SetEvaluations("蒸馏温度（中）").element;
                    fragment1.evaluation = SetEvaluations("蒸馏温度（中）").evaluation;
                    fragment1.baseObject.review.Clear();
                    fragment1.baseObject.review.Add("蒸馏温度：适中");
                }
                else
                {
                    fragment1.element = SetEvaluations("蒸馏温度（高）").element;
                    fragment1.evaluation = SetEvaluations("蒸馏温度（高）").evaluation;
                    fragment1.baseObject.review.Clear();
                    fragment1.baseObject.review.Add("蒸馏温度：偏高");
                }
                //2
                if (valueSet2.value <= 0.333f)
                {
                    fragment2.element = SetEvaluations("蒸馏时长（低）").element;
                    fragment2.evaluation = SetEvaluations("蒸馏时长（低）").evaluation;
                    fragment2.baseObject.review.Clear();
                    fragment2.baseObject.review.Add("蒸馏时长：偏低");
                }
                else if (valueSet2.value <= 0.666f)
                {
                    fragment2.element = SetEvaluations("蒸馏时长（中）").element;
                    fragment2.evaluation = SetEvaluations("蒸馏时长（中）").evaluation;
                    fragment2.baseObject.review.Clear();
                    fragment2.baseObject.review.Add("蒸馏时长：适中");
                }
                else
                {
                    fragment2.element = SetEvaluations("蒸馏时长（高）").element;
                    fragment2.evaluation = SetEvaluations("蒸馏时长（高）").evaluation;
                    fragment2.baseObject.review.Clear();
                    fragment2.baseObject.review.Add("蒸馏时长：偏高");
                }
                fragmentsOnDisc[index].element = fragment1.element + fragment2.element;
                fragmentsOnDisc[index].evaluation = fragment1.evaluation + fragment2.evaluation;
                fragmentsOnDisc[index].baseObject.review.Add(fragment1.baseObject.review[0]);
                fragmentsOnDisc[index].baseObject.review.Add(fragment2.baseObject.review[0]);
                Non();
                staticsFix.AddElement(inclusions, bi);
                break;
            case 2:
                SetEvaluation("看花摘酒");
                Non();
                staticsFix.AddElement(inclusions, bi);
                break;
        }
    }
    public void Init()
    {
        isConfirm = false;
        valueSet1.value = 0;
        valueSet2.value = 0;
        valueSet.value = 0;
        batch = uiManager.buttonList[uiManager.currentUI].GetComponent<UIObject>().batch;
        GetBatch();
        Check(bi);
        float sum = staticsFix.baseObj[bi].element.acid + staticsFix.baseObj[bi].element.ester + staticsFix.baseObj[bi].element.alcohol;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(sum, a, b, c);
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
        valueSet = gameObject.transform.Find("StatusSet").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("蒸馏设备Panel").transform.Find("PieChart").gameObject;
        gameObject.SetActive(false);
    }
    void Update()
    {
        switch (status)
        {
            case 1:
                float aa, bb, cc, gg, hh, ii;
                float aa2, bb2, cc2, gg2, hh2, ii2;
                if (valueSet1.value <= 0.333f)
                {
                    valueChange = valueSet1.value / 0.333f;
                    aa = 0.5f + valueChange * 0.3f;
                    bb = 0.5f + valueChange * 0.7f;
                    cc = 0.5f + valueChange * 1;
                    gg = valueChange * 1;
                    hh = valueChange * 1;
                    ii = valueChange * 1;
                }
                else if (valueSet1.value <= 0.666f)
                {
                    valueChange = (valueSet1.value - 0.333f) / 0.333f;
                    aa = 0.5f + 0.3f + valueChange * 0.4f;
                    bb = 0.5f + 0.7f + valueChange * 0.3f;
                    cc = 1 - valueChange * 0.2f;
                    gg = 1 - valueChange * 0.4f;
                    hh = 1 - valueChange * 0.5f;
                    ii = 1 - valueChange * 0.3f;
                }
                else
                {
                    valueChange = (valueSet1.value - 0.666f) / 0.333f;
                    aa = 0.5f + 0.7f + valueChange * 0.3f;
                    bb = 0.5f + 1 - valueChange * 0.2f;
                    cc = 0.5f + 0.8f - valueChange * 0.3f;
                    gg = 0.6f - valueChange * 0.3f;
                    hh = 0.5f - valueChange * 0.2f;
                    ii = 0.7f - valueChange * 0.3f;
                }
                //2
                if (valueSet2.value <= 0.333f)
                {
                    valueChange = valueSet2.value / 0.333f;
                    aa2 = 0.5f + valueChange * 0.4f;
                    bb2 = 0.5f + valueChange * 0.9f;
                    cc2 = 0.5f + valueChange * 0.4f;
                    gg2 = valueChange * 1;
                    hh2 = valueChange * 1;
                    ii2 = valueChange * 1;
                }
                else if (valueSet2.value <= 0.666f)
                {
                    valueChange = (valueSet2.value - 0.333f) / 0.333f;
                    aa2 = 0.5f + 0.4f + valueChange * 0.6f;
                    bb2 = 0.5f + 0.9f + valueChange * 0.1f;
                    cc2 = 0.5f + 0.4f + valueChange * 0.4f;
                    gg2 = 1 - valueChange * 0.4f;
                    hh2 = 1 - valueChange * 0.5f;
                    ii2 = 1 - valueChange * 0.3f;
                }
                else
                {
                    valueChange = (valueSet2.value - 0.666f) / 0.333f;
                    aa2 = 0.5f + 1 - valueChange * 0.3f;
                    bb2 = 0.5f + 1 - valueChange * 0.2f;
                    cc2 = 0.5f + 0.8f + valueChange * 0.2f;
                    gg2 = 0.6f - valueChange * 0.3f;
                    hh2 = 0.5f - valueChange * 0.2f;
                    ii2 = 0.7f - valueChange * 0.3f;
                }
                if (!isConfirm)
                {
                    a.value = (aa + aa2) / 2 * 0.22f + staticsFix.baseObj[bi].element.acid;
                    b.value = (bb + bb2) / 2 * 0.2f + staticsFix.baseObj[bi].element.ester;
                    c.value = (cc + cc2) / 2 * 0.2f + staticsFix.baseObj[bi].element.alcohol;
                    g.value = (gg + gg2) / 2 * 0.25f + staticsFix.baseObj[bi].element.advancedAcid;
                    h.value = (hh + hh2) / 2 * 0.25f + staticsFix.baseObj[bi].element.advancedEster;
                    i.value = (ii + ii2) / 2 * 0.25f + staticsFix.baseObj[bi].element.advancedAlcohol;
                    float sum = a.value + b.value + c.value;
                    barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                    pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
                }

                break;
            case 2:
                if (valueSet.value <= 0.25f)
                {
                    valueChange = valueSet.value / 0.25f;
                    e.value = valueChange * 0.5f;
                    f.value = valueChange * 0.3f;
                }
                else if (valueSet.value <= 0.5f)
                {
                    valueChange = (valueSet.value - 0.25f) / 0.25f;
                    e.value = 0.5f + valueChange * 0.5f;
                    f.value = 0.3f + valueChange * 0.3f;
                }
                else if(valueSet.value <= 0.75f)
                {
                    valueChange = (valueSet.value - 0.5f) / 0.25f;
                    e.value = 1 - valueChange * 0.3f;
                    f.value = 0.6f + valueChange * 0.4f;
                }
                else
                {
                    valueChange = (valueSet.value - 0.75f) / 0.25f;
                    e.value = 0.7f - valueChange * 0.1f;
                    f.value = 1 - valueChange * 0.2f;
                }
                if (!isConfirm)
                {
                    e.value = e.value * 0.1f + staticsFix.baseObj[bi].element.yield;
                    f.value = f.value * 0.1f + staticsFix.baseObj[bi].element.taste;
                    barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                }

                break;
        }
    }
}
