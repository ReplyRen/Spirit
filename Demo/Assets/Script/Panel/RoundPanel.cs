using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPanel : BasePanel
{
    private List<BaseFragment> testFragments;
    void Start()
    {
        Round.InitialAngle();FragmentsManager.InitialFragments();
        testFragments = new List<BaseFragment>();
        BaseFragment [] baseFragment = new BaseFragment[4];
        for(int i=0;i<4;i++)
        {
            baseFragment[i] = new BaseFragment();
        }
        baseFragment[0].name = "30";
        baseFragment[0].model = FragmentModel.thirty;
        testFragments.Add(baseFragment[0]);
        baseFragment[1].name = "60";
        baseFragment[1].model = FragmentModel.sixty;
        testFragments.Add(baseFragment[1]);
        baseFragment[2].name = "90";
        baseFragment[2].model = FragmentModel.ninety;
        testFragments.Add(baseFragment[2]);
        baseFragment[3].name = "120";
        baseFragment[3].model = FragmentModel.oneHundredAndTwenty;
        testFragments.Add(baseFragment[3]);
        FragmentsManager.UpdateFragments(testFragments);
    }
}
