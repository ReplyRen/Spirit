using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitsPanel : MonoBehaviour
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
    GameManager instance;
    List<BaseFragment> fragmentsOnDisc;
    Slider valueSet;
    Slider valueSet1;
    Slider valueSet2;
    Slider valueSet3;
    GameObject barChart;
    GameObject pieChart;
    float valueChange;
    int status;
    int index;
    float G, H, I;
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
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            switch (fragmentsOnDisc[i].name)
            {
                case "修窖":
                    barChart.SetActive(false);
                    pieChart.SetActive(false);
                    valueSet.gameObject.SetActive(false);
                    valueSet1.gameObject.SetActive(false);
                    valueSet2.gameObject.SetActive(false);
                    valueSet3.gameObject.SetActive(false);
                    status = 1;
                    index = i;
                    break;
                case "制曲、入曲":
                    barChart.SetActive(true);
                    pieChart.SetActive(false);
                    valueSet.gameObject.SetActive(true);
                    valueSet1.gameObject.SetActive(false);
                    valueSet2.gameObject.SetActive(false);
                    valueSet3.gameObject.SetActive(false);
                    status = 2;
                    index = i;
                    break;
                case "发酵":
                    barChart.SetActive(true);
                    pieChart.SetActive(true);
                    valueSet.gameObject.SetActive(false);
                    valueSet1.gameObject.SetActive(true);
                    valueSet2.gameObject.SetActive(true);
                    valueSet3.gameObject.SetActive(true);
                    status = 3;
                    index = i;
                    break;
                case "加原辅料":
                    barChart.SetActive(false);
                    pieChart.SetActive(false);
                    valueSet.gameObject.SetActive(false);
                    valueSet1.gameObject.SetActive(false);
                    valueSet2.gameObject.SetActive(false);
                    valueSet3.gameObject.SetActive(false);
                    status = 4;
                    index = i;
                    break;
                default: break;

            }
        }
    }
    public void Init()
    {
        valueSet.value = 0;
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
        valueSet = GameObject.Find("窖池Panel").transform.Find("StatusSet").GetComponent<Slider>();
        valueSet1 = GameObject.Find("窖池Panel").transform.Find("StatusSet1").GetComponent<Slider>();
        valueSet2 = GameObject.Find("窖池Panel").transform.Find("StatusSet2").GetComponent<Slider>();
        valueSet3 = GameObject.Find("窖池Panel").transform.Find("StatusSet3").GetComponent<Slider>();
        barChart = GameObject.Find("窖池Panel").transform.Find("Histogram").gameObject;
        pieChart = GameObject.Find("窖池Panel").transform.Find("PieChart").gameObject;
        Init();
        gameObject.SetActive(false);
    }
    void Update()
    {
        switch (status)
        {
            case 1:
                SetEvaluation("修窖");
                break;
            case 2:
                if (valueSet.value <= 0.333f)
                {
                    valueChange = valueSet.value / 0.333f;
                    e.value = valueChange * 1;
                    g.value = valueChange * 0.3f;
                    h.value = valueChange * 0.3f;
                    i.value = valueChange * 0.3f;
                    SetEvaluation("加曲量（低）");

                }
                else if (valueSet.value <= 0.666f)
                {
                    valueChange = (valueSet.value - 0.333f) / 0.333f;
                    e.value = 1 - valueChange * 0.2f;
                    g.value = 0.3f + valueChange * 0.2f;
                    h.value = 0.3f + valueChange * 0.2f;
                    i.value = 0.3f + valueChange * 0.2f;
                    SetEvaluation("加曲量（中）");
                }
                else
                {
                    valueChange = (valueSet.value - 0.666f) / 0.333f;
                    e.value = 0.8f - valueChange * 0.2f;
                    g.value = 0.5f + valueChange * 0.5f;
                    h.value = 0.5f + valueChange * 0.5f;
                    i.value = 0.5f + valueChange * 0.5f;
                    SetEvaluation("加曲量（高）");
                }
                float sum = a.value + b.value + c.value;
                barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                G = g.value;
                H = h.value;
                I = i.value;
                break;
            case 3:
                BaseFragment fragment1 = new BaseFragment();
                BaseFragment fragment2 = new BaseFragment();
                BaseFragment fragment3 = new BaseFragment();
                float aa, bb, cc, ff, gg, hh, ii;
                float aa2, bb2, cc2;
                float aa3, bb3, cc3, gg3, hh3, ii3;
                if (valueSet1.value <= 0.333f)
                {
                    valueChange = valueSet1.value / 0.333f;
                    aa = valueChange * 1;
                    bb = valueChange * 0.8f;
                    cc = valueChange * 0.5f;
                    gg = valueChange * 0.3f;
                    hh = valueChange * 0.5f;
                    ii = valueChange * 0.3f;
                    fragment1.element = SetEvaluations("发酵温度（低）").element;
                    fragment1.evaluation = SetEvaluations("发酵温度（低）").evaluation;
                }
                else if (valueSet1.value <= 0.666f)
                {
                    valueChange = (valueSet1.value - 0.333f) / 0.333f;
                    aa = 1 - valueChange * 0.3f;
                    bb = 0.8f + valueChange * 0.2f;
                    cc = 0.5f + valueChange * 0.3f;
                    gg = 0.3f + valueChange * 0.7f;
                    hh = 0.5f + valueChange * 0.5f;
                    ii = 0.3f + valueChange * 0.2f;
                    fragment1.element = SetEvaluations("发酵温度（中）").element;
                    fragment1.evaluation = SetEvaluations("发酵温度（中）").evaluation;
                }
                else
                {
                    valueChange = (valueSet1.value - 0.666f) / 0.333f;
                    aa = 0.7f - valueChange * 0.2f;
                    bb = 1 - valueChange * 0.3f;
                    cc = 0.8f + valueChange * 0.2f;
                    gg = 1 - valueChange * 0.2f;
                    hh = 1 - valueChange * 0.3f;
                    ii = 0.5f + valueChange * 0.5f;
                    fragment1.element = SetEvaluations("发酵温度（高）").element;
                    fragment1.evaluation = SetEvaluations("发酵温度（高）").evaluation;
                }
                //2
                if (valueSet2.value <= 0.333f)
                {
                    valueChange = valueSet2.value / 0.333f;
                    aa2 = valueChange * 0.3f;
                    bb2 = valueChange * 0.7f;
                    cc2 = valueChange * 1;
                    ff = valueChange * 0.3f;
                    fragment2.element = SetEvaluations("发酵酸度（低）").element;
                    fragment2.evaluation = SetEvaluations("发酵酸度（低）").evaluation;
                }
                else if (valueSet2.value <= 0.666f)
                {
                    valueChange = (valueSet2.value - 0.333f) / 0.333f;
                    aa2 = 0.3f + valueChange * 0.4f;
                    bb2 = 0.7f + valueChange * 0.3f;
                    cc2 = 1 - valueChange * 0.2f;
                    ff = 0.3f + valueChange * 0.7f;
                    fragment2.element = SetEvaluations("发酵酸度（中）").element;
                    fragment2.evaluation = SetEvaluations("发酵酸度（中）").evaluation;
                }
                else
                {
                    valueChange = (valueSet2.value - 0.666f) / 0.333f;
                    aa2 = 0.7f + valueChange * 0.3f;
                    bb2 = 1 - valueChange * 0.2f;
                    cc2 = 0.8f - valueChange * 0.3f;
                    ff = 1 - valueChange * 0.3f;
                    fragment2.element = SetEvaluations("发酵酸度（高）").element;
                    fragment2.evaluation = SetEvaluations("发酵酸度（高）").evaluation;
                }
                //3
                if (valueSet3.value <= 0.333f)
                {
                    valueChange = valueSet3.value / 0.333f;
                    aa3 = valueChange * 0.4f;
                    bb3 = valueChange * 0.9f;
                    cc3 = valueChange * 0.4f;
                    gg3 = valueChange * 0.3f;
                    hh3 = valueChange * 0.5f;
                    ii3 = valueChange * 0.3f;
                    fragment3.element = SetEvaluations("发酵时长（低）").element;
                    fragment3.evaluation = SetEvaluations("发酵时长（低）").evaluation;

                }
                else if (valueSet3.value <= 0.666f)
                {
                    valueChange = (valueSet3.value - 0.333f) / 0.333f;
                    aa3 = 0.4f - valueChange * 0.6f;
                    bb3 = 0.9f + valueChange * 0.1f;
                    cc3 = 0.4f + valueChange * 0.4f;
                    gg3 = 0.3f + valueChange * 0.7f;
                    hh3 = 0.5f + valueChange * 0.5f;
                    ii3 = 0.3f + valueChange * 0.2f;
                    fragment3.element = SetEvaluations("发酵时长（中）").element;
                    fragment3.evaluation = SetEvaluations("发酵时长（中）").evaluation;
                }
                else
                {
                    valueChange = (valueSet3.value - 0.666f) / 0.333f;
                    aa3 = 1 - valueChange * 0.3f;
                    bb3 = 1 - valueChange * 0.2f;
                    cc3 = 0.8f + valueChange * 0.2f;
                    gg3 = 1 - valueChange * 0.2f;
                    hh3 = 1 - valueChange * 0.3f;
                    ii3 = 0.5f + valueChange * 0.5f;
                    fragment3.element = SetEvaluations("发酵时长（高）").element;
                    fragment3.evaluation = SetEvaluations("发酵时长（高）").evaluation;
                }
                a.value = (aa + aa2 + aa3) / 3;
                b.value = (bb + bb2 + bb3) / 3;
                c.value = (cc + cc2 + cc3) / 3;
                f.value = ff;
                g.value = (G + gg + gg3) / 3;
                h.value = (H + hh + hh3) / 3;
                i.value = (I + ii + ii3) / 3;
                sum = a.value + b.value + c.value;
                barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
                pieChart.GetComponent<PieChart>().UpdateChart(sum, a, b, c);
                fragmentsOnDisc[index].element = fragment1.element + fragment2.element + fragment3.element;
                fragmentsOnDisc[index].evaluation = fragment1.evaluation + fragment2.evaluation + fragment3.evaluation;
                break;
            case 4:
                SetEvaluation("加原辅料");
                break;
        }
    }
}
