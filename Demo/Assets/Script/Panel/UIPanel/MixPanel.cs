using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixPanel : MonoBehaviour
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
    GameManager instance;
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc = new List<BaseFragment>();
    GameObject barChart;
    Slider valueSet;
    float valueChange;
    int index = -1;
    public void Init()
    {
        barChart.GetComponent<Histogram>().Init(d, e, f, g, h, i);
        valueSet.value = 0;
    }
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        valueSet = gameObject.transform.Find("StatusSet").GetComponent<Slider>();
        barChart = gameObject.transform.Find("Histogram").gameObject;
        Init();
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            if (fragmentsOnDisc[i].name == "勾兑勾调")
            {
                index = i;
            }
        }
        if (index >= 0) SetEvaluation("勾兑勾调");
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
        if (valueSet.value <= 0.2f)
        {
            valueChange = valueSet.value / 0.2f;
            f.value = valueChange * 0.2f;
        }
        else if (valueSet.value <= 0.4f)
        {
            valueChange = (valueSet.value - 0.2f) / 0.2f;
            f.value = 0.2f + valueChange * 0.5f;
        }
        else if (valueSet.value <= 0.6f)
        {
            valueChange = (valueSet.value - 0.5f) / 0.2f;
            f.value = 0.7f - valueChange * 0.1f;
        }
        else if (valueSet.value <= 0.8f) 
        {
            valueChange = (valueSet.value - 0.75f) / 0.2f;
            f.value = 0.6f + valueChange * 0.4f;
        }
        else
        {
            valueChange = (valueSet.value - 0.75f) / 0.2f;
            f.value = 1 - valueChange * 0.6f;
        }
        barChart.GetComponent<Histogram>().UpdateLength(d, e, f, g, h, i);
    }
}
