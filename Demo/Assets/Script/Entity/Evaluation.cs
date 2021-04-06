using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluation
{
    /// <summary>
    /// 基础分
    /// </summary>
    public float baseScore = 0;
    /// <summary>
    /// 强度
    /// </summary>
    public float intensity = 0;

    /// <summary>
    /// 浓郁度
    /// </summary>
    public float rich = 0;

    /// <summary>
    /// 连绵度
    /// </summary>
    public float continuity = 0;

    /// <summary>
    /// 陈敛细腻度
    /// </summary>
    public float fineness = 0;

    /// <summary>
    /// 风味
    /// </summary>
    public float flavor = 0;

    public static Evaluation operator +(Evaluation a,Evaluation b)
    {
        Evaluation evaluation = new Evaluation();
        evaluation.intensity = a.intensity + b.intensity;
        evaluation.rich = a.rich + b.rich;
        evaluation.fineness = a.fineness + b.fineness;
        evaluation.flavor = a.flavor + b.flavor;
        evaluation.continuity = a.continuity + b.continuity;
        return evaluation;
    }

}
