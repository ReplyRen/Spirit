using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoragePanel : MonoBehaviour
{
    public List<BaseObject> storage = new List<BaseObject>();
    //BaseObject card=new BaseObject();
    public GameObject panel;
    void Start()
    {
        int i;
        for (i = 0; i < 10; i++)
        {
            BaseObject card = new BaseObject();
            card.description = i.ToString();
            storage.Add(card);
        }
        for(int j=0;j<i;j++)
        {
            Debug.Log(storage[j].description);
        }
    }
    public void Add()
    {
        panel.SetActive(true);
        for(int i=0;i<storage.Count;i++)
        {
            GameObject a = new GameObject();
            Text text = a.AddComponent(typeof(Text)) as Text;
            text.text = storage[i].description;
            
            //text.text = i.ToString();
        }
    }
    void Update()
    {
        //Debug.Log(storage.Count);
    }
}
