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
    GameObject barChart;
    float valueChange;
    int index = -1;
    public void Init()
    {
        valueSet1.value = 0;
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
    }
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet1 = gameObject.transform.Find("StatusSet1").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
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
        barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
    }
}
