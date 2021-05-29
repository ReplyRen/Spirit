using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookPanel : MonoBehaviour
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
    List<BaseFragment> fragmentsOnDisc;
    GameManager instance;
    Slider valueSet1;
    GameObject barChart;
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
    public void Init()
    {
        isConfirm = false;
        batch = uiManager.buttonList[uiManager.currentUI].GetComponent<UIObject>().batch;
        if (!CheckB(batch)) staticsFix.InitStart(batch);
        GetBatch();
        Check(bi);
        valueSet1.value = 0;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
    }
    void Start()
    {
        Non();
        staticsFix = GameObject.Find("Main Camera").GetComponent<StaticsFix>();
        uiManager = GameObject.Find("Canvas").transform.Find("FactoryPanel").GetComponent<UIManager>();
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet1 = gameObject.transform.Find("StatusSet1").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            if (fragmentsOnDisc[i].name == "蒸煮摊凉")
            {
                index = i;
            }
        }
        if (index >= 0) SetEvaluation("蒸煮摊凉");
        gameObject.SetActive(false);
    }
    public void Confirm()
    {
        isConfirm = true;
        Non();
        staticsFix.AddElement(inclusions, bi);
    }

    void SetEvaluation(string name)
    {
        fragment = instance.fragmentDic[name];
        fragmentsOnDisc[index].element = fragment.element;
        fragmentsOnDisc[index].evaluation = fragment.evaluation;
    }
    void Update()
    {
        if (valueSet1.value <= 0.333f)
        {
            valueChange = valueSet1.value / 0.333f;
            e.value = valueChange * 0.3f;
            f.value = valueChange * 0.4f;
        }
        else if (valueSet1.value <= 0.666f)
        {
            valueChange = (valueSet1.value - 0.333f) / 0.333f;
            e.value = 0.3f + valueChange * 0.4f;
            f.value = 0.4f + valueChange * 0.6f;
        }
        else
        {
            valueChange = (valueSet1.value - 0.666f) / 0.333f;
            e.value = 0.7f + valueChange * 0.4f;
            f.value = 1 - valueChange * 0.4f;
        }
        if (!isConfirm)
        {
            e.value = e.value * 0.15f + staticsFix.baseObj[bi].element.yield;
            f.value = f.value * 0.15f + staticsFix.baseObj[bi].element.taste;
         barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
        }

    }
}
