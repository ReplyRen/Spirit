using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    public List<GameObject> purchaseObject = new List<GameObject>();
    public GameObject buy;
    public GameObject panel;
    public GameObject fatherObj1;
    public GameObject fatherObj2;
    public List<Image> images;
    List<BaseObject> baseObjects = new List<BaseObject>();
    List<int> names = new List<int>();
    Text text;
    ViewPanel viewPanel;
    bool a = false;
    bool b = false;
    public void Switch()
    {
        if(fatherObj1.activeSelf)
        {
            fatherObj1.SetActive(false);
            fatherObj2.SetActive(true);
            text.text = "查看主料";
            Check();
        }
        else
        {
            fatherObj1.SetActive(true);
            fatherObj2.SetActive(false);
            text.text = "查看辅料";
            Check();
        }
    }
    //更新物品list
    public void UpdateList()
    {

    }
    public void Clear()
    {
        for (int i = 0; i < fatherObj1.transform.childCount; i++)
            Destroy(fatherObj1.transform.GetChild(i).gameObject);
    }
    public void UpdateObjects()
    {
        for(int i=0;i<images.Count;i++)
        {
            images[i].enabled = false;
            purchaseObject[i].GetComponent<UIObject>().isUse = false;
        }
        for(int i=0;i<purchaseObject.Count;i++)
        {
            purchaseObject[i].SetActive(true);
        }
        Change(true, 0, 1, 2, 3, 4);
        names.Clear();
        a = false;
        b = false;
    }
    public void Purchase()
    {
        BaseObject temp = new BaseObject();
        baseObjects.Add(temp);
        for(int i=0;i<purchaseObject.Count;i++)
        {
            if (purchaseObject[i].GetComponent<UIObject>().isUse)
            {
                purchaseObject[i].SetActive(false);
            }
        }
        for (int i = 0; i < names.Count; i++)
        {
            if (names[i] < 5) baseObjects[baseObjects.Count - 1].mains.Add((主料)names[i]);
            else baseObjects[baseObjects.Count - 1].minors.Add((辅料)(names[i] - 5));
        }
        buy.GetComponent<UIObject>().isConfirm = true;
        buy.SetActive(false);
        for(int i=0;i<5;i++)
        {
            try
            {
                Change(true, i);
            }
            catch { }
        }
    }
    void Change(bool a,params int[] index)
    {
        if (!a)
        {
            for (int i = 0; i < index.Length; i++)
            {
                purchaseObject[index[i]].GetComponent<Button>().interactable = false;
                purchaseObject[index[i]].GetComponent<Image>().color = new Color(0.35f, 0.35f, 0.35f, 1);
            }
        }
        else
        {
            for (int i = 0; i < index.Length; i++)
            {
                purchaseObject[index[i]].GetComponent<Button>().interactable = true;
                purchaseObject[index[i]].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    void Check()
    {
        if (!buy.GetComponent<UIObject>().isConfirm)
        {
            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] < 3) a = true;
                else if (names[i] >= 5) 
                {
                    b = true;
                    if (a) break;
                }
            }
            if (a && b) buy.SetActive(true);
            else buy.SetActive(false);
            a = false;
            b = false;
        }
    }
    public void Select()
    {
        if (!buy.GetComponent<UIObject>().isConfirm)
        {
            var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            images[btn.GetComponent<UIObject>().index].enabled = !images[btn.GetComponent<UIObject>().index].enabled;
            purchaseObject[btn.GetComponent<UIObject>().index].GetComponent<UIObject>().isUse = !purchaseObject[btn.GetComponent<UIObject>().index].GetComponent<UIObject>().isUse;
            if (btn.GetComponent<UIObject>().isUse)
            {
                names.Add(btn.GetComponent<UIObject>().index);
            }
            else
            {
                for (int i = 0; i < names.Count; i++)
                {
                    if (names[i] == btn.GetComponent<UIObject>().index)
                        names.Remove(names[i]);
                }
            }
            Change(true, 0, 1, 2, 3, 4);
            for (int i = 0; i < names.Count; i++)
            {
                switch (names[i])
                {
                    case 4:
                        Change(false, 1, 2);
                        break;
                    case 2:
                        Change(false, 3, 0, 4);
                        break;
                    case 0:
                        Change(false, 1, 2);
                        break;
                    case 3:
                        Change(false, 2);
                        break;
                    case 1:
                        Change(false, 0, 4);
                        break;
                }
            }
            Check();
        }
    }
    void Instance()
    {
        for (int i = 0; i < fatherObj1.transform.childCount; i++)
        {
            purchaseObject.Add(fatherObj1.transform.GetChild(i).gameObject);
            purchaseObject[i].AddComponent<UIObject>();
            purchaseObject[i].GetComponent<UIObject>().index = i;
            images.Add(fatherObj1.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Image>());
        }
        for (int i = fatherObj1.transform.childCount; i < fatherObj2.transform.childCount + fatherObj1.transform.childCount; i++) 
        {
            purchaseObject.Add(fatherObj2.transform.GetChild(i - fatherObj1.transform.childCount).gameObject);
            purchaseObject[i].AddComponent<UIObject>();
            purchaseObject[i].GetComponent<UIObject>().index = i;
            images.Add(fatherObj2.transform.GetChild(i - fatherObj1.transform.childCount).gameObject.transform.GetChild(0).GetComponent<Image>());
        }
        for(int i = 0; i < fatherObj2.transform.childCount + fatherObj1.transform.childCount; i++)
        {
            var btn = purchaseObject[i].GetComponent<Button>();
            btn.onClick.AddListener(Select);
        }
    }
    void Start()
    {
        viewPanel = GameObject.Find("PanelCanvas").transform.Find("检视Panel").GetComponent<ViewPanel>();
        baseObjects = GameObject.Find("Main Camera").GetComponent<GameManager>().baseList;
        text = GameObject.Find("Switch").transform.GetChild(0).GetComponent<Text>();
        Instance();
        UpdateObjects();
        buy.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
