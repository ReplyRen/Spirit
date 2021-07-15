using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmashPanel : MonoBehaviour
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
    GameManager instance;
    UIManager uiManager;
    StaticsFix staticsFix;
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc = new List<BaseFragment>();
    GameObject barChart;
    GameObject pieChart;
    Slider valueSet;
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
        isConfirm = false;
    }
    public void Init()
    {
        isConfirm = false;

        batch = uiManager.buttonList[uiManager.currentUI].GetComponent<UIObject>().batch;
        if (!CheckB(batch)) staticsFix.InitStart(batch);
        GetBatch();
        Check(bi);
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1, b, c);
        valueSet.value = 0;
    }
    public void Merge()
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
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1, b, c);
        valueSet.value = 0;
    }
    void Awake()
    {
        Non();
        staticsFix= GameObject.Find("Main Camera").GetComponent<StaticsFix>();
        uiManager = GameObject.Find("Canvas").transform.Find("FactoryPanel").GetComponent<UIManager>();
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet = gameObject.transform.Find("StatusSet").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("粉碎机Panel").transform.Find("PieChart").gameObject;
        for(int i=0;i<fragmentsOnDisc.Count;i++)
        {
            if(fragmentsOnDisc[i].name== "粉碎润料")
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
    void GetBatch()
    {
        switch(batch)
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
        for (int i = 0; i < staticsFix.baseObj.Count; i++)
        {
            if (staticsFix.baseObj[i].batch == s)
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
    public void Confirm()
    {

        if (valueSet.value <= 0.333f)
        {
            SetEvaluation("粉碎强度（低）");

        }
        else if (valueSet.value <= 0.666f)
        {
            SetEvaluation("粉碎强度（中）");
        }
        else
        {
            SetEvaluation("粉碎强度（高）");
        }
        Non();
        isConfirm = true;
        staticsFix.AddElement(inclusions, bi);
    }
    void Update()
    {   
        if(valueSet.value<=0.333f)
        {
            valueChange = valueSet.value / 0.333f;
            b.value = 0.5f + valueChange * 0.4f;
            c.value = 0.5f + valueChange * 0.7f;
            d.value = valueChange * 0.4f;
        }
        else if(valueSet.value<=0.666f)
        {
            valueChange = (valueSet.value - 0.333f) / 0.333f;
            b.value = 0.5f + 0.4f + valueChange * 0.2f;
            c.value = 0.5f + 0.7f + valueChange * 0.3f;
            d.value = 0.4f + valueChange * 0.2f;
        }
        else
        {
            valueChange = (valueSet.value - 0.666f) / 0.333f;
            b.value = 0.5f + 0.6f + valueChange * 0.4f;
            c.value = 0.5f + 1 - valueChange * 0.2f;
            d.value = 0.6f + valueChange * 0.4f;
        }
        if (!isConfirm)
        {
            b.value = b.value * 0.1f + staticsFix.baseObj[bi].element.ester;
            c.value = c.value * 0.1f + staticsFix.baseObj[bi].element.alcohol;
            d.value = d.value * 0.3f + staticsFix.baseObj[bi].element.microbe;
            float sum = b.value + c.value;
            barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
            pieChart.GetComponent<PieChart>().UpdateChart(sum, b, c);
        }

    }
}
