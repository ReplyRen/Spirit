﻿using System.Collections;
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
                //FragmentsManager.fragmentsOnRound[i].DurationDecrease();
                FragmentsManager.fragmentsOnRound[i].obj.GetComponent<FragmentsControl>().enabled = false;
                if (FragmentsManager.fragmentsOnRound[i].finished)
                {
                    Destroy(FragmentsManager.fragmentsOnRound[i].obj);
                    FragmentsManager.fragmentsOnRound[i].obj.GetComponent<FragmentsControl>().enabled = true;
                    FragmentsManager.fragmentsOnRound[i].obj.GetComponent<FragmentsControl>().ReMoveFragment();
                    FragmentsManager.fragmentsOnRound.RemoveAt(i);
                }                    
            }
        }
            
        gameObject.SetActive(true);
        if (ifFirstTime)
        {
            ifFirstTime = false;
            Round.InitialAngle();
            FragmentsManager.InitialFragments();
        }
        else if (GuideControl.id >= 602)
        {
            if (newFragments.Count < 5)
            {
                GuideControl.id = 1001;
                GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
            }
            else if (newFragments.Count < 8)
            {
                GuideControl.id = 1011;
                GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
            }
            else if (newFragments.Count < 12)
            {
                GuideControl.id = 1021;
                GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
            }
            else
            {
                GuideControl.id = 1031;
                GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
            }
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
