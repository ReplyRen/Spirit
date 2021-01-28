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
    }
    public void UpdateObjects()
    {
        panel.SetActive(true);
        Debug.Log(storage.Count);
        for(int i=0;i<storage.Count;i++)
        {
            //GameObject storageObject = storage[i].gameObject;
            GameObject storageObject = new GameObject();
            GameObject temp = GameObject.Find("StoragePanel").transform.Find("Objects").gameObject;
            storageObject.transform.SetParent(temp.transform);
            //storageObject.sprite = storage[i].sprite;
            //storageObject.name = storage[i].name;
            //Text text;
            SpriteRenderer sprite = storageObject.AddComponent<SpriteRenderer>();
            Button btn = storageObject.AddComponent<Button>();
            sprite.sprite = Resources.Load<Sprite>("Sprite/30");
            storageObject.AddComponent<RectTransform>();
            storageObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //text.text = storage[i].description;
            //text.text = i.ToString();
        }
    }
    void Update()
    {
        //Debug.Log(storage.Count);
    }
}
