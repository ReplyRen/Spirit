using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    /// <summary>
    /// 120个格子的状态
    /// 0--无
    /// 1--满
    /// 2--边缘1
    /// 3--边缘2
    /// </summary>
    public static int[] angleState;
    /// <summary>
    /// 0无，-1炸了
    /// </summary>
    public static int[] State;

    public static void InitialAngle()
    {
        angleState = new int[120];
        for (int i = 0; i < 120; i++)
            angleState[i] = 0;
        State = new int[12];
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
        int totalAngle = ((int)fragmentModel + 1) * 10;
        for (int i = 0; i <= totalAngle; i++) 
        {
            if (angleState[(index - totalAngle / 2 + i + 120) % 120] == 1) 
                return 0;

        }
        return 1;
    }
    public static bool ResourceRight(int index,string name)
    {
        switch(name)
        {
            case "原、辅料准备":
                if (index == 25 || index == 55 || index == 85 || index == 115)
                    return true;
                else
                    return false;
            case "粉碎润料":
                return true;
            case "配料":
                if (index == 10 || index == 40 || index == 70 || index == 100)
                    return true;
                else
                    return false;
            case "蒸煮摊凉":
                return true;
            case "修窖":
                return true;
            case "制曲、入曲":
                if (index == 15 || index == 45 || index == 75 || index == 105)
                    return true;
                else
                    return false;
            case "发酵":
                if (index == 25 || index == 55 || index == 85 || index == 115)
                    return true;
                else
                    return false;
            case "加原辅料":
                if (index == 10 || index == 40 || index == 70 || index == 100)
                    return true;
                else
                    return false;
            case "上甑":
                return true;
            case "蒸馏":
                if (index == 25 || index == 55 || index == 85 || index == 115)
                    return true;
                else
                    return false;
            case "看花摘酒":
                return true;
            case "陈酿":
                if (index == 25 || index == 55 || index == 85 || index == 115)
                    return true;
                else
                    return false;
            case "勾兑勾调":
                if (index == 5 || index == 35 || index == 65 || index == 95)
                    return true;
                else
                    return false;
            case "罐装":
                return true;
            case "鉴酒":
                if (index == 25 || index == 55 || index == 85 || index == 115)
                    return true;
                else
                    return false;
        }
        return true;
    }
    /// <summary>
    /// 放置碎片
    /// </summary>
    /// <param name="index"></param>
    /// <param name="fragmentModel"></param>
    public static void PutFragment(int index, FragmentModel fragmentModel)
    {
        int totalAngle = ((int)fragmentModel + 1) * 10;
        for (int i = 0; i <= totalAngle; i++)
        {
            angleState[(index - totalAngle / 2 + i + 120) % 120] = 1;
            if (i == 0 || i == totalAngle)
                angleState[(index - totalAngle / 2 + i + 120) % 120] = 2;
        }
    }
    /// <summary>
    /// 移出碎片
    /// </summary>
    /// <param name="index"></param>
    /// <param name="fragmentModel"></param>
    public static void RemoveFragment(int index, FragmentModel fragmentModel)
    {
        int totalAngle = ((int)fragmentModel + 1) * 10;
        for (int i = 0; i <= totalAngle; i++)
        {
            angleState[(index - totalAngle / 2 + i + 120) % 120] = 0;
        }
    }
}
