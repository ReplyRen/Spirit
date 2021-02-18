using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluation
{
    /// <summary>
    /// 强度
    /// </summary>
    public float intensity;

    /// <summary>
    /// 浓郁度
    /// </summary>
    public float rich;

    /// <summary>
    /// 连绵度
    /// </summary>
    public float continuity;

    /// <summary>
    /// 陈敛细腻度
    /// </summary>
    public float fineness;

    /// <summary>
    /// 风味
    /// </summary>
    public float flavor;

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
