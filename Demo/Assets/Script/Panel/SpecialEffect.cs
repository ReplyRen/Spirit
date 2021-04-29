using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEffect : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    Shader shader;
    [SerializeField]
    Material mt;
    
    public List<Material> mats;

    void Awake()
    {
        GameObject temp;
        int i;
        for (i = 7; i < 10 + 8; i++) /// 初始化list
        {
            temp = GameObject.Find("Canvas").transform.Find("FactoryPanel").GetChild(i).gameObject;
            if (temp.name != "Next")
            {
                Material mat = Instantiate(mt);
                mat.SetFloat("_Flag", 0);
                mat.SetFloat("_MinOffset", 8f);
                mat.SetColor("_OutLineCol", Color.yellow);
                temp.GetComponent<Image>().material = mat;
                mats.Add(temp.GetComponent<Image>().material);
            }
        }
    }

    void Update()
    {
        
    }
}
