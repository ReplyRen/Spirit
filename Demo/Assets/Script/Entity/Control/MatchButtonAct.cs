﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchButtonAct : MonoBehaviour
{
    [SerializeField]
    GameObject matchBtn;
    [SerializeField]
    GameObject matchPanel;
    [SerializeField]
    GameObject winesPanel;
    [SerializeField]
    Bazzar bazzar;
    [SerializeField]
    List<GameObject> markets;
    [SerializeField]
    List<GameObject> matches;
    UIObject UIObject;
    int date = 0;
    List<int> sk = new List<int>();

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(1.2f);
        matchBtn.SetActive(false);
        matchPanel.SetActive(false);
        winesPanel.SetActive(false);
    }
    IEnumerator WaitFor2()
    {
        yield return new WaitForSeconds(1.2f);
        matchBtn.SetActive(true);
    }
    public void NextReset()
    {
        StartCoroutine(WaitFor());
    } 
    public void FinishReset()
    {
        StartCoroutine(WaitFor2());
    }
    public void OpenMatch()
    {
        matchPanel.SetActive(true);
    }
    public void CloseMatch()
    {
        matchPanel.SetActive(false);
    }
    public void CreatMod()
    {
        
    }
    public void JoinMod()
    {
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        UIObject = btn.gameObject.GetComponent<UIObject>();

        Match m = btn.transform.parent.gameObject.GetComponent<Match>();
        m.isJoin = true;
        bazzar.JoinMatch(m.wines,m.judges);
        markets[UIObject.index].SetActive(true);
        btn.SetActive(false);
    }
    void Start()
    {
        for(int i=0;i<3;i++)
        {
            sk.Add(i);
            markets[i].SetActive(true)  ;
            markets[i].SetActive(false);
        }
        matchBtn.SetActive(false);
        matchPanel.SetActive(false);
        winesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
