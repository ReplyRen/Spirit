using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    /// <summary>
    /// 60个格子的状态
    /// 0--无
    /// 1--满
    /// 2--凹
    /// 3--凸
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
    /// 2--需反转
    /// </summary>
    /// <param name="index">中心线对应位置序号</param>
    /// <param name="fragmentModel">该碎片是什么样式</param>
    /// <returns></returns>
    public static int PlaceRight(int index,FragmentModel fragmentModel)
    {
        return 0;
    }
}
