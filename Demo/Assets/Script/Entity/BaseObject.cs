using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject
{
    /// <summary>
    /// 物品的图片
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// 物品的名称
    /// </summary>
    public string name;

    /// <summary>
    /// 物品的描述
    /// </summary>
    public string description;

    /// <summary>
    /// 属性数据
    /// </summary>
    public Element element;

    /// <summary>
    /// 评价数据
    /// </summary>
    public Evaluation evaluation;

    public Queue<float> alcoholQueue = new Queue<float>();
}
