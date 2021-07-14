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
    UIObject UIObject;

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
        markets[UIObject.index].SetActive(true);
    }
    void Start()
    {
        matchBtn.SetActive(false);
        matchPanel.SetActive(false);
        winesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
