using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element
{
    //元素，指酒的基本属性

    /// <summary>
    /// 酸含量
    /// </summary>
    public float acid=0;

    /// <summary>
    /// 酯含量
    /// </summary>
    public float ester=0;

    /// <summary>
    /// 醇含量
    /// </summary>
    public float alcohol=0;

    /// <summary>
    /// 微生物含量
    /// </summary>
    public float microbe=0;

    /// <summary>
    /// 产量
    /// </summary>
    public float yield=0;

    /// <summary>
    /// 质感
    /// </summary>
    public float taste=0;

    /// <summary>
    /// 高级酸
    /// </summary>
    public float advancedAcid=0;

    /// <summary>
    /// 高级酯
    /// </summary>
    public float advancedEster=0;

    /// <summary>
    /// 高级醇
    /// </summary>
    public float advancedAlcohol=0;

    /// <summary>
    /// 未命名
    /// </summary>
    public float unnamed;

    public Element()
    {

    }

    //重写操作符
    public static Element operator+ (Element a,Element b)
    {
        Element element = new Element();
        element.acid = a.acid + b.acid;
        element.ester = a.ester + b.ester;
        element.alcohol = a.alcohol + b.alcohol;
        element.microbe = a.microbe + b.microbe;
        element.yield = a.yield + b.yield;
        element.taste = a.taste + b.taste;
        element.advancedAcid = a.advancedAcid + b.advancedAcid;
        element.advancedAlcohol = a.advancedAlcohol + b.advancedAlcohol;
        element.advancedEster = a.advancedEster + b.advancedEster;
        element.unnamed = a.unnamed + b.unnamed;


        return element;
    }

}
