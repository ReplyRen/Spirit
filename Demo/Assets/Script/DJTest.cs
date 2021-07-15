using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DJTest : MonoBehaviour
{
    [SerializeField]
    GameObject dialog;
    [SerializeField]
    GameObject tt;
    void CreateDialog()
    {
        //Object a = Resources.Load("/Prefab/对话") as GameObject;
        GameObject d = Instantiate(tt) as GameObject;
        d.transform.SetParent(dialog.transform);
        d.transform.localScale = new Vector3(1, 1, 1);
        d.transform.SetSiblingIndex(0);
        d.GetComponent<MarketDialog>().RandomScore();
    }
    void Start()
    {
        
    }
    float t = 0.007f;
    int ii = 0, jj = 0, kk = 0;
    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        if (kk < 35  && t <= 0)
        {
            t = 0.007f;
            ii++;
            if (ii == 4)
            {
                ii = 0;
                jj++;
            }
            if (jj == 5)
            {
                jj = 0;
                kk++;
                if (kk % 5 == 0) CreateDialog();
            }
        }
    }
}

