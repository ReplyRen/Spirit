﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsUpdate : MonoBehaviour
{
    public RectTransform canvas;
    public GameObject round;
    public GameObject information;
    private void OnEnable()
    {
        Invoke("InitialFragments", 0.01f);
    }
    public void InitialFragments()
    {
        ClearFragments();
        foreach(var fragment in FragmentsManager.GetFragments())
        {
            //Debug.Log(fragment.name);
            fragment.obj = StaticMethod.LoadPrefab("Prefab/fragment");
            fragment.obj = Instantiate(fragment.obj);
            fragment.obj.transform.SetParent(transform);
            fragment.obj.GetComponent<FragmentsControl>().fragmentInformation = fragment;
            fragment.obj.GetComponent<FragmentsControl>().canvas = canvas;
            fragment.obj.GetComponent<FragmentsControl>().round = round;
            fragment.obj.GetComponent<FragmentsControl>().information = information;
            fragment.obj.GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 1);
            //Debug.Log(fragment.name + "+" + fragment.facility);
        }
    }
    private void ClearFragments()
    {
        for (int i = transform.childCount - 1; i >= 0; i--) 
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
