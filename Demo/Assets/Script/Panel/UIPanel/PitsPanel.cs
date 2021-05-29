using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitsPanel : MonoBehaviour
{
    Inclusion a = new Inclusion("酸含量", 0);
    Inclusion b = new Inclusion("酯含量", 0);
    Inclusion c = new Inclusion("醇含量", 0);
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
    BaseFragment fragment = new BaseFragment();
    GameManager instance;
    List<BaseFragment> fragmentsOnDisc;
    Slider valueSet;
    Slider valueSet0;
    Slider valueSet1;
    Slider valueSet2;
    Slider valueSet3;
    Slider valueSet4;
    GameObject barChart;
    GameObject pieChart;
    float valueChange;
    int status;
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
    BaseFragment SetEvaluations(string name)
    {
        BaseFragment fragment = instance.fragmentDic[name];
        return fragment;
    }
    public void OpenPanel()
    {
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            switch (fragmentsOnDisc[i].name)
            {
                case "修窖":
                    barChart.SetActive(true);
                    pieChart.SetActive(true);
                    valueSet.gameObject.SetActive(false);
                    valueSet0.gameObject.SetActive(true);
                    valueSet1.gameObject.SetActive(false);
                    valueSet2.gameObject.SetActive(false);
                    valueSet3.gameObject.SetActive(false);
                    valueSet4.gameObject.SetActive(false);
                    status = 1;
                    index = i;
                    break;
                case "制曲、入曲":
                    barChart.SetActive(true);
                    pieChart.SetActive(false);
                    valueSet.gameObject.SetActive(true);
                    valueSet0.gameObject.SetActive(false);
                    valueSet1.gameObject.SetActive(false);
                    valueSet2.gameObject.SetActive(false);
                    valueSet3.gameObject.SetActive(false);
                    valueSet4.gameObject.SetActive(false);
                    status = 2;
                    index = i;
                    break;
                case "发酵":
                    barChart.SetActive(true);
                    pieChart.SetActive(true);
                    valueSet.gameObject.SetActive(false);
                    valueSet0.gameObject.SetActive(false);
                    valueSet1.gameObject.SetActive(true);
                    valueSet2.gameObject.SetActive(true);
                    valueSet3.gameObject.SetActive(true);
                    valueSet4.gameObject.SetActive(false);
                    status = 3;
                    index = i;
                    break;
                case "加原辅料":
                    barChart.SetActive(true);
                    pieChart.SetActive(false);
                    valueSet.gameObject.SetActive(false);
                    valueSet0.gameObject.SetActive(false);
                    valueSet1.gameObject.SetActive(false);
                    valueSet2.gameObject.SetActive(false);
                    valueSet3.gameObject.SetActive(false);
                    valueSet4.gameObject.SetActive(true);
                    status = 4;
                    index = i;
                    break;
                default: break;

            }
        }
    }
    public void Confirm()
    {
        isConfirm = true;
        switch (status)
        {
            case 1:
                SetEvaluation("修窖");
                Non();
                staticsFix.AddElement(inclusions, bi);
                break;
            case 2:
                if (valueSet.value <= 0.333f)
                {
                    SetEvaluation("加曲量（低）");
                }
                else if (valueSet.value <= 0.666f)
                {
                    SetEvaluation("加曲量（中）");
                }
                else
                {
                    SetEvaluation("加曲量（高）");
                }
                Non();
                staticsFix.AddElement(inclusions, bi);
                break;
            case 3:
                if (valueSet1.value <= 0.333f)
                {
                    fragment1.element = SetEvaluations("发酵温度（低）").element;
                    fragment1.evaluation = SetEvaluations("发酵温度（低）").evaluation;
                    fragment1.baseObject.review.Clear();
                    fragment1.baseObject.review.Add("发酵温度：偏低");
                }
                else if (valueSet1.value <= 0.666f)
                {
                    fragment1.element = SetEvaluations("发酵温度（中）").element;
                    fragment1.evaluation = SetEvaluations("发酵温度（中）").evaluation;
                    fragment1.baseObject.review.Clear();
                    fragment1.baseObject.review.Add("发酵温度：适中");
                }
                else
                {
                    fragment1.element = SetEvaluations("发酵温度（高）").element;
                    fragment1.evaluation = SetEvaluations("发酵温度（高）").evaluation;
                    fragment1.baseObject.review.Clear();
                    fragment1.baseObject.review.Add("发酵温度：偏高");
                }
                //2
                if (valueSet2.value <= 0.333f)
                {
                    fragment2.element = SetEvaluations("发酵酸度（低）").element;
                    fragment2.evaluation = SetEvaluations("发酵酸度（低）").evaluation;
                    fragment2.baseObject.review.Clear();
                    fragment2.baseObject.review.Add("发酵酸度：偏低");
                }
                else if (valueSet2.value <= 0.666f)
                {
                    fragment2.element = SetEvaluations("发酵酸度（中）").element;
                    fragment2.evaluation = SetEvaluations("发酵酸度（中）").evaluation;
                    fragment2.baseObject.review.Clear();
                    fragment2.baseObject.review.Add("发酵酸度：适中");
                }
                else
                {
                    fragment2.element = SetEvaluations("发酵酸度（高）").element;
                    fragment2.evaluation = SetEvaluations("发酵酸度（高）").evaluation;
                    fragment2.baseObject.review.Clear();
                    fragment2.baseObject.review.Add("发酵酸度：偏高");
                }
                //3
                if (valueSet3.value <= 0.333f)
                {
                    fragment3.element = SetEvaluations("发酵时长（低）").element;
                    fragment3.evaluation = SetEvaluations("发酵时长（低）").evaluation;
                    fragment3.baseObject.review.Clear();
                    fragment3.baseObject.review.Add("发酵时长：偏低");
                }
                else if (valueSet3.value <= 0.666f)
                {
                    fragment3.element = SetEvaluations("发酵时长（中）").element;
                    fragment3.evaluation = SetEvaluations("发酵时长（中）").evaluation;
                    fragment3.baseObject.review.Clear();
                    fragment3.baseObject.review.Add("发酵时长：适中");
                }
                else
                {
                    fragment3.element = SetEvaluations("发酵时长（高）").element;
                    fragment3.evaluation = SetEvaluations("发酵时长（高）").evaluation;
                    fragment3.baseObject.review.Clear();
                    fragment3.baseObject.review.Add("发酵时长：偏高");
                }
                fragmentsOnDisc[index].element = fragment1.element + fragment2.element + fragment3.element;
                fragmentsOnDisc[index].evaluation = fragment1.evaluation + fragment2.evaluation + fragment3.evaluation;
                fragmentsOnDisc[index].baseObject.review.Add(fragment1.baseObject.review[0]);
                fragmentsOnDisc[index].baseObject.review.Add(fragment2.baseObject.review[0]);
                fragmentsOnDisc[index].baseObject.review.Add(fragment3.baseObject.review[0]);
                Non();
                staticsFix.AddElement(inclusions, bi);
                break;
            case 4:
                SetEvaluation("加原辅料");
                Non();
                staticsFix.AddElement(inclusions, bi);
                break;
        }
    }
    public void Init()
    {
        isConfirm = false;
        valueSet.value = 0;
        valueSet0.value = 0;
        valueSet1.value = 0;
        valueSet2.value = 0;
        valueSet3.value = 0;
        valueSet4.value = 0;
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
        valueSet = gameObject.transform.Find("StatusSet").GetComponent<Slider>();
        valueSet0 = gameObject.transform.Find("StatusSet0").GetComponent<Slider>();
        valueSet1 = gameObject.transform.Find("StatusSet1").GetComponent<Slider>();
        valueSet2 = gameObject.transform.Find("StatusSet2").GetComponent<Slider>();
        valueSet3 = gameObject.transform.Find("StatusSet3").GetComponent<Slider>();
        valueSet4 = gameObject.transform.Find("StatusSet4").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        pieChart = gameObject.transform.Find("PieChart").gameObject;
        gameObject.SetActive(false);
    }
    void Update()
    {
        switch (status)
        {
            case 1:
                if (valueSet0.value <= 0.333f)
                {
                    valueChange = valueSet0.value / 0.333f;
                    a.value = 0.5f + valueChange * 0.1f;
                    b.value = 0.5f + valueChange * 0.3f;
                    c.value = 0.5f + valueChange * 0.3f;
                    d.value = valueChange * 0.4f;
                }
                else if (valueSet0.value <= 0.666f)
                {
                    valueChange = (valueSet0.value - 0.333f) / 0.333f;
                    a.value = 0.5f + 0.1f + valueChange * 0.1f;
                    b.value = 0.5f + 0.3f + valueChange * 0.3f;
                    c.value = 0.5f + 0.3f + valueChange * 0.1f;
                    d.value = 0.4f + valueChange * 0.3f;
                }
                else
                {
                    valueChange = (valueSet0.value - 0.666f) / 0.333f;
                    a.value = 0.5f + 0.2f + valueChange * 0.2f;
                    b.value = 0.5f + 0.6f + valueChange * 0.4f;
                    c.value = 0.5f + 0.4f + valueChange * 0.6f;
                    d.value = 0.7f + valueChange * 0.3f;
                }
                if (!isConfirm)
                {
                    a.value = a.value * 0.22f + staticsFix.baseObj[bi].element.acid;
                    b.value = b.value * 0.2f + staticsFix.baseObj[bi].element.ester;
                    c.value = c.value * 0.2f + staticsFix.baseObj[bi].element.alcohol;
                    d.value = d.value * 0.7f + staticsFix.baseObj[bi].element.microbe;
                    float sum = a.value + b.value + c.value;
                    barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                    pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
                }

                break;
            case 2:
                if (valueSet.value <= 0.333f)
                {
                    valueChange = valueSet.value / 0.333f;
                    e.value = valueChange * 1;
                    g.value = valueChange * 0.3f;
                    h.value = valueChange * 0.3f;
                    i.value = valueChange * 0.3f;

                }
                else if (valueSet.value <= 0.666f)
                {
                    valueChange = (valueSet.value - 0.333f) / 0.333f;
                    e.value = 1 - valueChange * 0.2f;
                    g.value = 0.3f + valueChange * 0.2f;
                    h.value = 0.3f + valueChange * 0.2f;
                    i.value = 0.3f + valueChange * 0.2f;
                }
                else
                {
                    valueChange = (valueSet.value - 0.666f) / 0.333f;
                    e.value = 0.8f - valueChange * 0.2f;
                    g.value = 0.5f + valueChange * 0.5f;
                    h.value = 0.5f + valueChange * 0.5f;
                    i.value = 0.5f + valueChange * 0.5f;
                }
                if (!isConfirm)
                {
                    e.value = e.value * 0.4f + staticsFix.baseObj[bi].element.yield;
                    g.value = g.value * 0.25f + staticsFix.baseObj[bi].element.advancedAcid;
                    h.value = h.value * 0.25f + staticsFix.baseObj[bi].element.advancedEster;
                    i.value = i.value * 0.25f + staticsFix.baseObj[bi].element.advancedAlcohol;
                    float sum = a.value + b.value + c.value;
                    barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                }

                break;
            case 3:
                float aa, bb, cc, ff, gg, hh, ii;
                float aa2, bb2, cc2;
                float aa3, bb3, cc3, gg3, hh3, ii3;
                if (valueSet1.value <= 0.333f)
                {
                    valueChange = valueSet1.value / 0.333f;
                    aa = 0.5f + valueChange * 1;
                    bb = 0.5f + valueChange * 0.8f;
                    cc = 0.5f + valueChange * 0.5f;
                    gg = valueChange * 0.3f;
                    hh = valueChange * 0.5f;
                    ii = valueChange * 0.3f;
                }
                else if (valueSet1.value <= 0.666f)
                {
                    valueChange = (valueSet1.value - 0.333f) / 0.333f;
                    aa = 0.5f + 1 - valueChange * 0.3f;
                    bb = 0.5f + 0.8f + valueChange * 0.2f;
                    cc = 0.5f + 0.5f + valueChange * 0.3f;
                    gg = 0.3f + valueChange * 0.7f;
                    hh = 0.5f + valueChange * 0.5f;
                    ii = 0.3f + valueChange * 0.2f;
                }
                else
                {
                    valueChange = (valueSet1.value - 0.666f) / 0.333f;
                    aa = 0.5f + 0.7f - valueChange * 0.2f;
                    bb = 0.5f + 1 - valueChange * 0.3f;
                    cc = 0.5f + 0.8f + valueChange * 0.2f;
                    gg = 1 - valueChange * 0.2f;
                    hh = 1 - valueChange * 0.3f;
                    ii = 0.5f + valueChange * 0.5f;
                }
                //2
                if (valueSet2.value <= 0.333f)
                {
                    valueChange = valueSet2.value / 0.333f;
                    aa2 = 0.5f + valueChange * 0.3f;
                    bb2 = 0.5f + valueChange * 0.7f;
                    cc2 = 0.5f + valueChange * 1;
                    ff = valueChange * 0.3f;
                }
                else if (valueSet2.value <= 0.666f)
                {
                    valueChange = (valueSet2.value - 0.333f) / 0.333f;
                    aa2 = 0.5f + 0.3f + valueChange * 0.4f;
                    bb2 = 0.5f + 0.7f + valueChange * 0.3f;
                    cc2 = 0.5f + 1 - valueChange * 0.2f;
                    ff = 0.3f + valueChange * 0.7f;
                }
                else
                {
                    valueChange = (valueSet2.value - 0.666f) / 0.333f;
                    aa2 = 0.5f + 0.7f + valueChange * 0.3f;
                    bb2 = 0.5f + 1 - valueChange * 0.2f;
                    cc2 = 0.5f + 0.8f - valueChange * 0.3f;
                    ff = 1 - valueChange * 0.3f;
                }
                //3
                if (valueSet3.value <= 0.333f)
                {
                    valueChange = valueSet3.value / 0.333f;
                    aa3 = 0.5f + valueChange * 0.4f;
                    bb3 = 0.5f + valueChange * 0.9f;
                    cc3 = 0.5f + valueChange * 0.4f;
                    gg3 = valueChange * 0.3f;
                    hh3 = valueChange * 0.5f;
                    ii3 = valueChange * 0.3f;
                }
                else if (valueSet3.value <= 0.666f)
                {
                    valueChange = (valueSet3.value - 0.333f) / 0.333f;
                    aa3 = 0.5f + 0.4f - valueChange * 0.6f;
                    bb3 = 0.5f + 0.9f + valueChange * 0.1f;
                    cc3 = 0.5f + 0.4f + valueChange * 0.4f;
                    gg3 = 0.3f + valueChange * 0.7f;
                    hh3 = 0.5f + valueChange * 0.5f;
                    ii3 = 0.3f + valueChange * 0.2f;
                }
                else
                {
                    valueChange = (valueSet3.value - 0.666f) / 0.333f;
                    aa3 = 0.5f + 1 - valueChange * 0.3f;
                    bb3 = 0.5f + 1 - valueChange * 0.2f;
                    cc3 = 0.5f + 0.8f + valueChange * 0.2f;
                    gg3 = 1 - valueChange * 0.2f;
                    hh3 = 1 - valueChange * 0.3f;
                    ii3 = 0.5f + valueChange * 0.5f;
                }
                if (!isConfirm)
                {
                    a.value = (aa + aa2 + aa3) / 3 * 0.22f + staticsFix.baseObj[bi].element.acid;
                    b.value = (bb + bb2 + bb3) / 3 * 0.2f + staticsFix.baseObj[bi].element.ester;
                    c.value = (cc + cc2 + cc3) / 3 * 0.2f + staticsFix.baseObj[bi].element.alcohol;
                    f.value = ff * 0.325f + staticsFix.baseObj[bi].element.taste;
                    g.value = (gg + gg3) / 2 * 0.25f + staticsFix.baseObj[bi].element.advancedAcid;
                    h.value = (hh + hh3) / 2 * 0.25f + staticsFix.baseObj[bi].element.advancedEster;
                    i.value = (ii + ii3) / 2 * 0.25f + staticsFix.baseObj[bi].element.advancedAlcohol;
                    float sum = a.value + b.value + c.value;
                    barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                    pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
                }

                break;
            case 4:
                if (valueSet4.value <= 0.333f)
                {
                    valueChange = valueSet4.value / 0.333f;
                    e.value = valueChange * 0.4f;
                }
                else if (valueSet4.value <= 0.666f)
                {
                    valueChange = (valueSet4.value - 0.333f) / 0.333f;
                    e.value = 0.4f + valueChange * 0.6f;
                }
                else
                {
                    valueChange = (valueSet4.value - 0.666f) / 0.333f;
                    e.value = 1 - valueChange * 0.7f;
                }
                if (!isConfirm)
                {
                    e.value = e.value * 0.4f + staticsFix.baseObj[bi].element.yield;
                    barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                }
                break;
        }
    }
}
