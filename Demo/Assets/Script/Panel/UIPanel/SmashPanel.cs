using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmashPanel : MonoBehaviour
{
    GameManager instance;
    BaseFragment fragment = new BaseFragment();
    List<BaseFragment> fragmentsOnDisc = new List<BaseFragment>();
    Slider valueSet;
    int index;
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        valueSet = GameObject.FindWithTag("StatusSet").GetComponent<Slider>();
        fragmentsOnDisc = GameObject.Find("Main Camera").GetComponent<GameManager>().fragmentOnDisc;
        valueSet.value = 0;
        for(int i=0;i<fragmentsOnDisc.Count;i++)
        {
            if(fragmentsOnDisc[i].name== "粉碎润料")
            {
                index = i;
            }
        }
    }
    void SetEvaluation(string name)
    {
        fragment = instance.fragmentDic[name];
        fragmentsOnDisc[index].element = fragment.element;
        fragmentsOnDisc[index].evaluation = fragment.evaluation;
    }
    void Update()
    {   
        if(valueSet.value<=0.333f)
        {
            SetEvaluation("粉碎强度（低）");
        }
        else if(valueSet.value<=0.666f)
        {
            SetEvaluation("粉碎强度（中）");
        }
        else
        {
            SetEvaluation("粉碎强度（高）");
        }
    }
}
