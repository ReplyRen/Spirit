using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteamerPanel : MonoBehaviour
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
    GameManager instance;
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc = new List<BaseFragment>();
    GameObject barChart;
    Slider valueSet;
    float valueChange;
    int index = -1;
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
    public void Confirm()
    {
        Non();
        isConfirm = true;
        staticsFix.AddElement(inclusions, bi);
    }
    public void Init()
    {
        isConfirm = false;
        batch = uiManager.buttonList[uiManager.currentUI].GetComponent<UIObject>().batch;
        GetBatch();
        Check(bi);
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        valueSet.value = 0;
    }
    void Start()
    {
        Non();
        staticsFix = GameObject.Find("Main Camera").GetComponent<StaticsFix>();
        uiManager = GameObject.Find("Canvas").transform.Find("FactoryPanel").GetComponent<UIManager>();
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet = gameObject.transform.Find("StatusSet").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            if (fragmentsOnDisc[i].name == "上甑")
            {
                index = i;
            }
        }
        if (index >= 0) SetEvaluation("上甑");
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
            e.value = valueChange * 0.4f;
            f.value = valueChange * 0.5f;
        }
        else if (valueSet.value <= 0.666f)
        {
            valueChange = (valueSet.value - 0.333f) / 0.333f;
            e.value = 0.4f + valueChange * 0.3f;
            f.value = 0.5f + valueChange * 0.5f;
        }
        else
        {
            valueChange = (valueSet.value - 0.666f) / 0.333f;
            e.value = 0.7f + valueChange * 0.3f;
            f.value = 1 - valueChange * 0.6f;
        }
        if (!isConfirm)
        {
            e.value = e.value * 0.1f + staticsFix.baseObj[bi].element.yield;
            f.value = f.value * 0.1f + staticsFix.baseObj[bi].element.taste;
            barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
        }

    }
}
