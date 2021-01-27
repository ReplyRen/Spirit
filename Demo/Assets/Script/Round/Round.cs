using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    /// <summary>
    /// 60个格子的状态
    /// 0--无
    /// 1--满
    /// 2--边缘1
    /// 3--边缘2
    /// </summary>
    public static int[] angleState;

    public static void InitialAngle()
    {
        angleState = new int[60];
        for (int i = 0; i < 60; i++)
            angleState[i] = 0;
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
        int totalAngle = ((int)fragmentModel + 1) * 5;
        for (int i = 0; i <= totalAngle; i++) 
        {
            if (angleState[(index - totalAngle / 2 + i + 60) % 60] == 1) 
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
        int totalAngle = ((int)fragmentModel + 1) * 5;
        for (int i = 0; i <= totalAngle; i++)
        {
            angleState[(index - totalAngle / 2 + i + 60) % 60] = 1;
            if (i == 0 || i == totalAngle)
                angleState[(index - totalAngle / 2 + i + 60) % 60] = 2;
        }
    }
    /// <summary>
    /// 移出碎片
    /// </summary>
    /// <param name="index"></param>
    /// <param name="fragmentModel"></param>
    public static void RemoveFragment(int index, FragmentModel fragmentModel)
    {
        int totalAngle = ((int)fragmentModel + 1) * 5;
        for (int i = 0; i <= totalAngle; i++)
        {
            angleState[(index - totalAngle / 2 + i + 60) % 60] = 0;
        }
    }
}
