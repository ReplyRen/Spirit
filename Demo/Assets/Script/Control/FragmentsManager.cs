using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsManager
{
    private static List<BaseFragment> fragments;
    /// <summary>
    /// 清零碎片列
    /// </summary>
    public static void ClearFragments()
    {
        fragments.Clear();
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
        ClearFragments();
        foreach (var fragment in newFragments)
            fragments.Add(fragment);
    }
}
