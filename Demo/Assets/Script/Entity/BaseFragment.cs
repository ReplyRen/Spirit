﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 碎片模型
/// </summary>
public enum FragmentModel
{
    thirty, sixty, ninety, oneHundredAndTwenty
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
    public int duration = 1;

    /// <summary>
    /// 属性数据
    /// </summary>
    public Element element;

    /// <summary>
    /// 评价数据
    /// </summary>
    public Evaluation evaluation;

    /// <summary>
    /// 对应酒基
    /// </summary>
    public BaseObject baseObject=new BaseObject();

    /// <summary>
    /// 是否完成
    /// </summary>
    public bool finished = false;

    /// <summary>
    /// 主料(仅在原辅料准备使用)
    /// </summary>
    public List<主料> mains = new List<主料>();
    
    public string facility;

    /// <summary>
    /// 辅料(仅在原辅料准备使用)
    /// </summary>
    public List<辅料> minors = new List<辅料>();

    public Process process;

    public void DurationDecrease()
    {
        duration--;
        if (duration <= 0)
        {
            finished = true;
        }
    }


}
