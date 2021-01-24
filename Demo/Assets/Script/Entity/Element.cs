using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element
{
    //元素，指酒的基本属性

    /// <summary>
    /// 酸含量
    /// </summary>
    public float acid;

    /// <summary>
    /// 酯含量
    /// </summary>
    public float ester;

    /// <summary>
    /// 醇含量
    /// </summary>
    public float alcohol;

    /// <summary>
    /// 微生物含量
    /// </summary>
    public float microbe;

    /// <summary>
    /// 产量
    /// </summary>
    public float yield;

    /// <summary>
    /// 质感
    /// </summary>
    public float taste;

    /// <summary>
    /// 高级酸
    /// </summary>
    public float advancedAcid;

    /// <summary>
    /// 高级酯
    /// </summary>
    public float advancedEster;

    /// <summary>
    /// 高级醇
    /// </summary>
    public float advancedAlcohol;

    /// <summary>
    /// 未命名
    /// </summary>
    public float unnamed;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="元素种类，如果是酒基，则十维默认为1"></param>
    public Element(ElementKind kind)
    {
        if (kind == ElementKind.BaseObject)
        {
            acid = 1;
            ester = 1;
            alcohol = 1;
            microbe = 1;
            yield = 1;
            taste = 1;
            advancedAcid = 1;
            advancedEster = 1;
            advancedAlcohol = 1;
            unnamed = 1;
        }
    }

    //重写操作符
    public static Element operator+ (Element a,Element b)
    {
        Element element = new Element(ElementKind.Fragment);
        element.acid = a.acid * b.acid;
        element.ester = a.ester * b.ester;
        element.alcohol = a.alcohol * b.alcohol;
        element.microbe = a.microbe * b.microbe;
        element.yield = a.yield * b.yield;
        element.taste = a.taste * b.taste;
        element.advancedAcid = a.advancedAcid * b.advancedAcid;
        element.advancedAlcohol = a.advancedAlcohol * b.advancedAlcohol;
        element.advancedEster = a.advancedEster * b.advancedEster;
        element.unnamed = a.unnamed * b.unnamed;


        return element;
    }

}
public enum ElementKind
{
    Fragment,   //碎片
    BaseObject  //酒
}
