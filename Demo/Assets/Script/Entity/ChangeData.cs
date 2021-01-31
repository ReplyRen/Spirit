using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeData
{
    public static float Acid(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.acid;
    }
    public static float Ester(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.ester;
    }
    public static float Alcohol(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.alcohol;
    }
    public static float Microbe(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.microbe;
    }
    public static float Yield(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.yield;
    }
    public static float Taste(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.taste;
    }
    public static float AdvancedAcid(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.advancedAcid;
    }
    public static float AdvancedEster(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.advancedEster;
    }
    public static float AdvancedAlcohol(ref BaseFragment fragment, float valueChange)
    {
        return fragment.element.advancedAlcohol;
    }
}
