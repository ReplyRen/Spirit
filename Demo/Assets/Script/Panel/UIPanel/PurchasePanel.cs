using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    public List<BaseObject> storageObject = new List<BaseObject>();
    public List<BaseObject> purchaseObject1 = new List<BaseObject>();
    public List<BaseObject> purchaseObject2 = new List<BaseObject>();
    public GameObject buy;
    public GameObject panel;
    public GameObject fatherObj1;
    public GameObject fatherObj2;
    Text text;
    int num = 3;
    public void Switch()
    {
        if(fatherObj1.activeSelf)
        {
            fatherObj1.SetActive(false);
            fatherObj2.SetActive(true);
            text.text = "查看主料";
        }
        else
        {
            fatherObj1.SetActive(true);
            fatherObj2.SetActive(false);
            text.text = "查看辅料";
        }
    }
    //更新物品list
    public void UpdateList()
    {
        purchaseObject1.Clear();
        for (int i = 0; i < num; i++)
        {
            BaseObject card = new BaseObject();
            card.description = i.ToString();
            card.name = i.ToString();
            card.sprite = Resources.Load<Sprite>("Sprite/圆盘/60");
            purchaseObject1.Add(card);
        }
    }
    public void Clear()
    {
        for (int i = 0; i < fatherObj1.transform.childCount; i++)
            Destroy(fatherObj1.transform.GetChild(i).gameObject);
    }
    public void UpdateObjects()
    {
        if (!buy.GetComponent<UIObject>().isConfirm)
        {
            Clear();
            for (int i = 0; i < purchaseObject1.Count; i++)
            {
                InstantiateObj(purchaseObject1[i].sprite, purchaseObject1[i].name, fatherObj1);
            }
        }
    }
    public void Purchase()
    {
        for(int i=0;i<purchaseObject1.Count;i++)
        {
            if (fatherObj1.transform.GetChild(i).GetComponent<UIObject>().isUse)
            {
                Destroy(fatherObj1.transform.GetChild(i).gameObject);
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
                GameObject temp = fatherObj1.transform.GetChild(i).gameObject;
                temp.GetComponent<Outline>().enabled = false;
                temp.GetComponent<UIObject>().isUse = false;
            }
            var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            btn.GetComponent<UIObject>().isUse = !btn.GetComponent<UIObject>().isUse;
            btn.GetComponent<Outline>().enabled = !btn.GetComponent<Outline>().enabled;
        }
    }
    public void InstantiateObj(Sprite sprite, string name, GameObject fatherObj)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<UIObject>();
        Button btn = obj.AddComponent<Button>();
        Image img = obj.AddComponent<Image>();
        Outline outLine = obj.AddComponent<Outline>();
        outLine.enabled = false;
        outLine.OutlineWidth = 5f;
        outLine.OutlineColor = Color.yellow;
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
        text = GameObject.Find("Switch").transform.GetChild(0).GetComponent<Text>();
        storageObject = GameObject.Find("Main Camera").GetComponent<GameManager>().baseList;
        UpdateList();
        UpdateObjects();
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
