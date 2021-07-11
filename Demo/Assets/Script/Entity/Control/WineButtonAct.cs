using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineButtonAct : MonoBehaviour
{
    Wine wine;
    Bazzar bazzar;
    public void SelectWine()
    {
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        wine = btn.GetComponent<Wine>();
    }
    public void UploadWine()
    {
        bazzar.GetWine(wine);
    }

    void Start()
    {
        bazzar = GameObject.Find("Main Camera").GetComponent<Bazzar>();
        wine = this.gameObject.GetComponent<Wine>();
    }

    void Update()
    {
        
    }
}
