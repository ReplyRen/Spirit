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
    List<Inclusion> inclusions = new List<Inclusion>();
    UIManager uiManager;
    StaticsFix staticsFix;
    BaseFragment fragment = new BaseFragment();
    GameManager instance;
    List<BaseFragment> fragmentsOnDisc;
    Slider valueSet;
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
    bool CheckB(int s)
    {
        bool isIn = false;
        for(int i=0;i<staticsFix.baseObj.Count;i++)
        {
            if(staticsFix.baseObj[i].batch==s)
            {
                isIn = true;
                break;
            }
        }
        return isIn;
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
    public void Init()
    {
        isConfirm = false;
        batch = uiManager.buttonList[uiManager.currentUI].GetComponent<UIObject>().batch;
        if (!CheckB(batch))
        {
            staticsFix.InitStart(batch);
        }
        GetBatch();
        Check(bi);
        valueSet.value = 0;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1.5f, a, b, c);
    }
    void Awake()
    {
        Non();
        staticsFix = GameObject.Find("Main Camera").GetComponent<StaticsFix>();
        uiManager = GameObject.Find("Canvas").transform.Find("FactoryPanel").GetComponent<UIManager>();
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet = GameObject.Find("储藏室Panel").transform.Find("StatusSet").GetComponent<Slider>();
        barChart = GameObject.Find("储藏室Panel").transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("储藏室Panel").transform.Find("PieChart").gameObject;
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
        Non();
        isConfirm = true;
        staticsFix.AddElement(inclusions, bi);
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
        if (!isConfirm)
        {
            a.value = a.value * 0.12f + staticsFix.baseObj[bi].element.acid;
            b.value = b.value * 0.1f + staticsFix.baseObj[bi].element.ester;
            c.value = c.value * 0.1f + staticsFix.baseObj[bi].element.alcohol;
            e.value = e.value * 0.15f + staticsFix.baseObj[bi].element.yield;
            float sum = a.value + b.value + c.value;
            barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
            pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
        }

    }
}
