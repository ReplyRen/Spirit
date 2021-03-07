using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixPanel : MonoBehaviour
{
    BaseFragment fragment = new BaseFragment();
    GameManager instance;
    List<BaseFragment> fragmentsOnDisc;
    int index;
    void Start()
    {
        instance = GameObject.Find("Main Camera").GetComponent<GameManager>();
        fragmentsOnDisc = instance.fragmentOnDisc;
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            if (fragmentsOnDisc[i].name == "勾兑勾调")
            {
                index = i;
            }
        }
        SetEvaluation("勾兑勾调");
    }
    void SetEvaluation(string name)
    {
        fragment = instance.fragmentDic[name];
        fragmentsOnDisc[index].element = fragment.element;
        fragmentsOnDisc[index].evaluation = fragment.evaluation;
    }
    void Update()
    {
        
    }
}
