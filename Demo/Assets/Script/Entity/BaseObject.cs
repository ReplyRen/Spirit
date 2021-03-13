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

    /// <summary>
    /// 主料
    /// </summary>
    public List<主料> mains = new List<主料>();

    /// <summary>
    /// 辅料
    /// </summary>
    public List<辅料> minors = new List<辅料>();

    public int mainStep = 1;
}
public enum 主料
{
    糯性高梁,高粱,大米,小麦,麸皮
}
public enum 辅料
{
    高粱皮,豌豆,大米,稻壳,猪肉,草药,糯米,小麦,玉米
}

