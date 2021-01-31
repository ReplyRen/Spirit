using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoragePanel : MonoBehaviour
{
    public List<BaseObject> storageUI = new List<BaseObject>();
    GameObject temp;
    public GameObject panel;
    //更新物品list
    public void UpdateList()
    {
        storageUI.Clear();
        //storageUI = GameObject.Find("GameManager").GetComponent<GameManager>().baseList;
        temp = GameObject.Find("FactoryPanel").transform.Find("StoragePanel/Objects").gameObject;
        for (int i = 0; i < 10/*storageUI。count*/; i++)
        {
            BaseObject card = new BaseObject();
            card.description = i.ToString();
            storageUI.Add(card);
            card.storageObject.name = card.description;
            storageUI[i].storageObject.transform.SetParent(temp.transform);
        }
    }
    //更新物品
    public void UpdateObjects()
    {
        for(int i=0;i<storageUI.Count;i++)
        {
            Image img = storageUI[i].storageObject.AddComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprite/30");
            Button btn = storageUI[i].storageObject.AddComponent<Button>();
            btn.onClick.AddListener(ShowDescription);
            btn.targetGraphic = img;
            storageUI[i].storageObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
    //显示描述
    public void ShowDescription()
    {
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Text text = GameObject.Find("StoragePanel/Description").GetComponent<Text>();
        text.text = storageUI[btn.transform.GetSiblingIndex()].description;
    }
    public void Show()
    {
        panel.SetActive(true);
    }
    void Start()
    {
        UpdateList();
        UpdateObjects();
    }
}
