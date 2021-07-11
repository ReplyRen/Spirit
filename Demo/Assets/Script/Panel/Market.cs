using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField]
    GameObject marketPanel;
    [SerializeField]
    GameObject market;
    List<GameObject> matches = new List<GameObject>();
    public void OpenPanel()
    {
        marketPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        marketPanel.SetActive(false);
    }
    public void OpenMarket()
    {
        market.SetActive(true);
        ClosePanel();
    }
    void LoadMatch()
    {
        for (int i = 0; i < 5; i++)
        {
            Object a = Resources.Load("Prefab/UIPrefab/Match"+i) as GameObject;
            GameObject temp = Instantiate(a) as GameObject;
            temp.transform.SetParent(marketPanel.transform);
            matches.Add(temp);
        }
    }
    void CreateMatch()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
