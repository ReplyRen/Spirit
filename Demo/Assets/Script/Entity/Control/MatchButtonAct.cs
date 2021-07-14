using System.Collections;
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
    List<int> sk = new List<int>();
    public int date = 0;
    int count = 0;

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
        date++;
        if(date==2||date==7||date==14)
        {
            int a = UnityEngine.Random.Range(0, sk.Count);
            matches[sk[a]].SetActive(true);
            bazzar.CreateMatch();

        }
    }
    public void JoinMod()
    {
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        UIObject = btn.gameObject.GetComponent<UIObject>();
        markets[UIObject.index].SetActive(true);
        bazzar.JoinMatch();
        btn.gameObject.transform.parent.gameObject.SetActive(false);
    }
    void Start()
    {
        for (int i = 0; i < 3; i++)
            sk.Add(i);
        matchBtn.SetActive(false);
        matchPanel.SetActive(false);
        winesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
