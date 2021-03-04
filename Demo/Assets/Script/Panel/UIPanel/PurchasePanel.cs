using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    public List<BaseObject> storageObject = new List<BaseObject>();
    public List<BaseObject> purchaseObject = new List<BaseObject>();
    public GameObject buy;
    public GameObject panel;
    public GameObject fatherObj;
    List<GameObject> objects = new List<GameObject>();
    int num = 10;
    //更新物品list
    public void UpdateList()
    {
        purchaseObject.Clear();
        for (int i = 0; i < num; i++)
        {
            BaseObject card = new BaseObject();
            card.description = i.ToString();
            card.name = i.ToString();
            card.sprite = Resources.Load<Sprite>("Sprite/圆盘/60");
            purchaseObject.Add(card);
        }
        /* List<BaseObject> newList = GameObject.Find("Main Camera").GetComponent<GameManager>().baseList;
         foreach (var element in newList)
             storageObject.Add(element);*/
    }
    public void Clear()
    {
        for (int i = 0; i < fatherObj.transform.childCount; i++)
            Destroy(fatherObj.transform.GetChild(i).gameObject);
    }
    public void UpdateObjects()
    {
        if (!buy.GetComponent<UIObject>().isConfirm)
        {
            Clear();
            for (int i = 0; i < purchaseObject.Count; i++)
            {
                InstantiateObj(purchaseObject[i].sprite, purchaseObject[i].name);
            }
        }
    }
    public void Purchase()
    {
        for(int i=0;i<purchaseObject.Count;i++)
        {
            if (fatherObj.transform.GetChild(i).GetComponent<UIObject>().isUse)
            {
                Destroy(fatherObj.transform.GetChild(i).gameObject);
            }
        }
        buy.GetComponent<UIObject>().isConfirm = true;
        buy.SetActive(false);
    }

    public void Select()
    {
        if (!buy.GetComponent<UIObject>().isConfirm)
        {
            for(int i=0;i<num;i++)
            {
                objects[i].GetComponent<Outline>().enabled = false;
                objects[i].GetComponent<UIObject>().isUse = false;
            }
            var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            btn.GetComponent<UIObject>().isUse = !btn.GetComponent<UIObject>().isUse;
            btn.GetComponent<Outline>().enabled = !btn.GetComponent<Outline>().enabled;
        }
    }
    public void InstantiateObj(Sprite sprite, string name)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<UIObject>();
        Button btn = obj.AddComponent<Button>();
        Image img = obj.AddComponent<Image>();
        Outline outLine = obj.AddComponent<Outline>();
        outLine.enabled = false;
        outLine.effectDistance = new Vector2(5, -5);
        outLine.effectColor = new Color(0, 0, 0, 1);
        img.sprite = sprite;
        obj.name = name;
        obj.transform.SetParent(fatherObj.transform);
        btn.targetGraphic = img;
        btn.onClick.AddListener(Select);
        obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    public void NextDay()
    {
        buy.GetComponent<UIObject>().isConfirm = false;
        UpdateList();
        UpdateObjects();
    }
    void Start()
    {
        storageObject = GameObject.Find("Main Camera").GetComponent<GameManager>().baseList;
        UpdateList();
        UpdateObjects();
        for(int i=0;i<num;i++)
        {
            GameObject temp = fatherObj.transform.GetChild(i).gameObject;
            objects.Add(temp);
        }
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
