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
    List<string> names = new List<string>();
    Text text;
    int status = 0;
    public void Switch()
    {
        if(fatherObj1.activeSelf)
        {
            fatherObj1.SetActive(false);
            fatherObj2.SetActive(true);
            text.text = "查看主料";
            if (status == 1) buy.SetActive(!buy.activeSelf);
        }
        else
        {
            fatherObj1.SetActive(true);
            fatherObj2.SetActive(false);
            text.text = "查看辅料";
            if (status == 1) buy.SetActive(!buy.activeSelf);
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
        if (!buy.GetComponent<UIObject>().isConfirm)
        {
            for(int i=0;i<images.Count;i++)
            {
                images[i].enabled = false;
                purchaseObject[i].GetComponent<UIObject>().isUse = false;
            }
        }
        status = 0;
    }
    public void Purchase()
    {
        for(int i=0;i<purchaseObject.Count;i++)
        {
            if (purchaseObject[i].GetComponent<UIObject>().isUse)
            {
                purchaseObject[i].SetActive(false);
            }
        }
        status++;
        buy.GetComponent<UIObject>().isConfirm = true;
        buy.SetActive(false);
        //Switch();
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
    public void Select()
    {
        if (status!=2)
        {
            var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            images[btn.GetComponent<UIObject>().index].enabled = !images[btn.GetComponent<UIObject>().index].enabled;
            purchaseObject[btn.GetComponent<UIObject>().index].GetComponent<UIObject>().isUse = !purchaseObject[btn.GetComponent<UIObject>().index].GetComponent<UIObject>().isUse;
            if (btn.GetComponent<UIObject>().isUse)
            {
                names.Add(btn.name);
            }
            else
            {
                for(int i=0;i<names.Count;i++)
                {
                    if (names[i] == btn.name)
                        names.Remove(names[i]);
                }
            }
            Change(true, 0, 1, 2, 3, 4);
            for(int i=0;i< names.Count; i++)
            {
                switch(names[i])
                {
                    case "麸皮":
                        Change(false, 1, 3);
                        break;
                    case "大米":
                        Change(false, 2, 0, 4);
                        break;
                    case "糯性高梁":
                        Change(false, 1, 3);
                        break;
                    case "小麦":
                        Change(false, 3);
                        break;
                    case "高梁":
                        Change(false, 0, 4);
                        break;
                }
            }
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
        text = GameObject.Find("Switch").transform.GetChild(0).GetComponent<Text>();
        Instance();
        UpdateList();
        UpdateObjects();
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
