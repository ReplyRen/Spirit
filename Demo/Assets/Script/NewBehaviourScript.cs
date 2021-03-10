using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Shader shader;
    public Material mat;
    public void A()
    {
        mat.SetFloat("_Flag", 0);
        mat.SetFloat("_MinOffset", 5f);
        mat.SetColor("_OutLineCol", Color.red);
    }
    void Start()
    {
        mat = new Material(shader);
        GameObject.Find("Button1").GetComponent<Image>().material = mat;
        //mat = GameObject.Find("Button1").GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
