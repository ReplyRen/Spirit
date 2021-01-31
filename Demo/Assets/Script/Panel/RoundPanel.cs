using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPanel : BasePanel
{
    private bool ifFirstTime = true;
    /// <summary>
    /// 流程里面把Start隐藏掉直接调用下面两个就行了
    /// 把testFragments也隐藏掉把
    /// </summary>
    void Start()
    {
        List<BaseFragment> testFragments;
        Round.InitialAngle();
        FragmentsManager.InitialFragments();
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
    /// <summary>
    /// 初始化函数
    /// </summary>
    /// <param name="newFragments">传fragmentList就行了</param>
    public void InitialRoundPanel(List<BaseFragment> newFragments)
    {
        if (FragmentsManager.fragmentsOnRound.Count != 0 && FragmentsManager.fragmentsOnRound != null)
        {
            for (int i = FragmentsManager.fragmentsOnRound.Count - 1; i >= 0; i--)
            {
                BaseFragment fragment = FragmentsManager.fragmentsOnRound[i];
                fragment.DurationDecrease();
                if (fragment.finished)
                    Destroy(fragment.obj);
                FragmentsManager.fragmentsOnRound.RemoveAt(i);
            }
        }
        gameObject.SetActive(true);
        if(ifFirstTime)
        {
            ifFirstTime = false;
            Round.InitialAngle();
            FragmentsManager.InitialFragments();
        }
        FragmentsManager.UpdateFragments(newFragments);
    }
    /// <summary>
    /// 结束函数
    /// </summary>
    /// <returns></returns>
    public List<BaseFragment> HideRoundPanel()
    {
        gameObject.SetActive(false);
        return FragmentsManager.fragmentsOnRound;
    }
}
