using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFragment
{
    /// <summary>
    /// 名称
    /// </summary>
    public string name;

    /// <summary>
    /// 对应游戏物体
    /// </summary>
    public GameObject obj;

    /// <summary>
    /// 持续时间
    /// </summary>
    public int duration;

    public void DurationDecrease()
    {
        duration--;
        if (duration <= 0)
        {
            
        }
    }
}
