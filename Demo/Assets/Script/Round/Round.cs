using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    /// <summary>
    /// 60个格子的状态
    /// 0--无
    /// 1--满
    /// </summary>
    public static bool[] angleState;

    public static void InitialAngle()
    {
        angleState = new bool[60];
        for (int i = 0; i < 60; i++)
            angleState[i] = false;
    }
    /// <summary>
    /// 给出中心线位置判断是否可嵌入
    /// 0--不嵌入
    /// 1--可嵌入
    /// </summary>
    /// <param name="index">中心线对应位置序号</param>
    /// <param name="fragmentModel">该碎片是什么样式</param>
    /// <returns></returns>
    public static int PlaceRight(int index,FragmentModel fragmentModel)
    {
        int totalAngle = ((int)fragmentModel + 1) * 30;
        for (int i = 0; i < totalAngle; i++) 
        {
            if (angleState[index - totalAngle / 2 + i])
                return 0;
        }
        return 1;
    }
    /// <summary>
    /// 放置碎片
    /// </summary>
    /// <param name="index"></param>
    /// <param name="fragmentModel"></param>
    public static void PutFragment(int index, FragmentModel fragmentModel)
    {
        int totalAngle = ((int)fragmentModel + 1) * 30;
        for (int i = 0; i < totalAngle; i++)
        {
            angleState[index - totalAngle / 2 + i] = true;
        }
    }
    /// <summary>
    /// 移出碎片
    /// </summary>
    /// <param name="index"></param>
    /// <param name="fragmentModel"></param>
    public static void RemoveFragment(int index, FragmentModel fragmentModel)
    {
        int totalAngle = ((int)fragmentModel + 1) * 30;
        for (int i = 0; i < totalAngle; i++)
        {
            angleState[index - totalAngle / 2 + i] = false;
        }
    }
}
