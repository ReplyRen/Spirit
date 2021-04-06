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

    public List<string> review = new List<string>();

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

    public Kind GetKind()
    {
        if (mains.Contains(主料.麸皮))
        {
            return Kind.芝麻香型;
        }
        else if (mains.Contains(主料.大米))
        {
            if (mains.Count == 1)
            {
                return Kind.米香型;
            }
            else
            {
                return Kind.特香型;
            }
        }
        else if (mains.Contains(主料.小麦))
        {
            return Kind.兼香型;
        }
        else if (mains.Contains(主料.糯性高梁))
        {
            return Kind.浓香型;
            return Kind.酱香型;
        }
        else
        {
            if (minors.Contains(辅料.猪肉))
                return Kind.豉香型;
            else if (minors.Contains(辅料.草药))
                return Kind.药香型;
            else if (minors.Count > 4)
                return Kind.馥郁香型;
            else if (minors.Contains(辅料.稻壳))
                return Kind.凤香型;
            else
            {
                return Kind.清香型;
                return Kind.老白干香型;
            }
        }
    }
}
public enum 主料
{
    糯性高梁,高粱,大米,小麦,麸皮
}
public enum 辅料
{
    高粱皮,豌豆,大米,稻壳,猪肉,草药,糯米,小麦,玉米
}
public enum Kind
{
    酱香型,浓香型,清香型,兼香型,米香型,凤香型,芝麻香型,豉香型,特香型,药香型,老白干香型,馥郁香型
}

