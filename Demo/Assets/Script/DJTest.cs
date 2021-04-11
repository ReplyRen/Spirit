using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DJTest : MonoBehaviour
{
    /*public GameObject a;
    public GameObject b;
    public Animator an;
    public void SetBoola()
    {
        an.SetBool("a", true);
        a.SetActive(true);
    }
    public void SetBoolb()
    {
        an.SetBool("b", true);
        b.SetActive(true);
    }
    public void Close()
    {
        an.SetBool("a", false);
        an.SetBool("b", false);
        a.SetActive(false);
        b.SetActive(false);
    }*/
    public void A()
    {
        Debug.Log("a");
        Invoke("ajk", 2);
    }
    public void B()
    {
        Debug.Log("b");
        Invoke("ajk", 2);
    }
    public void C()
    {
        Debug.Log("c");
        Invoke("ajk", 2);
    }
    public void D()
    {
        Debug.Log("d");
        Invoke("ajk", 2);
    }
    void ajk()
    {
        Debug.Log("ala");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
