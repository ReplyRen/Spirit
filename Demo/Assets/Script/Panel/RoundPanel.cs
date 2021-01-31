using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPanel : BasePanel
{
    private bool ifFirstTime = true;
    /// <summary>
    /// 流程里面把Start隐藏掉直接调用下面两个就行了
    /// </summary>
    void Start()
    {

    }
    /// <summary>
    /// 初始化函数
    /// </summary>
    /// <param name="newFragments">传fragmentList就行了</param>
    public void InitialRoundPanel(List<BaseFragment> newFragments)
    {
        if (FragmentsManager.fragmentsOnRound != null && FragmentsManager.fragmentsOnRound.Count != 0) 
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
