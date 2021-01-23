using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 碎片模型
/// </summary>
public enum FragmentModel
{
    singleConcave, singleConvex, doubleConvex, halfConcaveAndConvex
}

public class BaseFragment
{
    
    /// <summary>
    /// 名称
    /// </summary>
    public string name;
    /// <summary>
    /// 描述
    /// </summary>
    public string description;

    ///<summary>
    ///对应样式
    /// </summary>
    public FragmentModel model;

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
