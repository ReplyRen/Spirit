using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsManager
{
    private static List<BaseFragment> fragments;
    public static List<BaseFragment> fragmentsOnRound;
    /// <summary>
    /// 清零碎片列
    /// </summary>
    public static void ClearFragments()
    {
        fragments.Clear();
    }
    public static void InitialFragments()
    {
        fragments = new List<BaseFragment>();
        fragmentsOnRound = new List<BaseFragment>();
    }
    /// <summary>
    /// 获得碎片列
    /// </summary>
    /// <returns></returns>
    public static List<BaseFragment> GetFragments()
    {
        return fragments;
    }
    /// <summary>
    /// 获得特定碎片
    /// </summary>
    /// <param name="index">序列号</param>
    /// <returns></returns>
    public static BaseFragment GetFragment(int index)
    {
        return fragments[index];
    }

    /// <summary>
    /// 更新碎片列
    /// </summary>
    /// <param name="newFragments"></param>
    public static void UpdateFragments(List<BaseFragment> newFragments)
    {
        if (fragments != null || fragments.Count != 0)  
            ClearFragments();
        foreach (var fragment in newFragments)
        {
            fragments.Add(fragment);
        }
            
    }
    /// <summary>
    /// 返回确认时候圆盘上的碎片
    /// </summary>
    /// <returns></returns>
    /*public static List<BaseFragment> RetrueFragments()
    {
        //return FragmentsManager.GetFragments();
    }*/
}
