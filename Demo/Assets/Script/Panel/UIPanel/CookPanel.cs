using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookPanel : MonoBehaviour
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
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc;
    GameManager instance;
    Slider valueSet1;
    Slider valueSet2;
    GameObject barChart;
    GameObject pieChart;
    float valueChange;
    int index = -1;
    void Init()
    {
        valueSet1.value = 0;
        valueSet2.value = 0;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().Init(1.5f, a, b, c);
    }
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet1 = gameObject.transform.Find("StatusSet1").GetComponent<Slider>();
        valueSet2 = gameObject.transform.Find("StatusSet2").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("蒸煮锅Panel").transform.Find("PieChart").gameObject;
        Init();
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
    void SetEvaluation(string name)
    {
        fragment = instance.fragmentDic[name];
        fragmentsOnDisc[index].element = fragment.element;
        fragmentsOnDisc[index].evaluation = fragment.evaluation;
    }
    void Update()
    {
        BaseFragment fragment1 = new BaseFragment();
        BaseFragment fragment2 = new BaseFragment();
        float aa, bb, cc, gg, hh, ii;
        float aa2, bb2, cc2, gg2, hh2, ii2;
        if (valueSet1.value <= 0.333f)
        {
            valueChange = valueSet1.value / 0.333f;
            aa = 0.5f + valueChange * 0.5f;
            bb = 0.5f + valueChange * 0.7f;
            cc = 0.5f + valueChange * 1;
            gg = valueChange * 1;
            hh = valueChange * 1;
            ii = valueChange * 1;
        }
        else if (valueSet1.value <= 0.666f)
        {
            valueChange = (valueSet1.value - 0.333f) / 0.333f;
            aa = 0.5f + 0.5f + valueChange * 0.2f;
            bb = 0.5f + 0.7f + valueChange * 0.3f;
            cc = 0.5f + 1 - valueChange * 0.2f;
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
        a.value = (aa + aa2) / 2;
        b.value = (bb + bb2) / 2;
        c.value = (cc + cc2) / 2;
        g.value = (gg + gg2) / 2;
        h.value = (hh + hh2) / 2;
        i.value = (ii + ii2) / 2;
        float sum = a.value + b.value + c.value;
        barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
        pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
    }
}
