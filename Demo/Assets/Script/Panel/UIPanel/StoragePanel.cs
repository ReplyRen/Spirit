using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoragePanel : MonoBehaviour
{
    public List<BaseObject> storageObject = new List<BaseObject>();
    public GameObject panel;
    public GameObject fatherObj;
    public Text text;
    //更新物品list
    public void UpdateList()
    {
        /*storageObject.Clear();
        for (int i = 0; i < 10; i++)
        {
            BaseObject card = new BaseObject();
            card.description = i.ToString();
            card.name = i.ToString();
            card.sprite = Resources.Load<Sprite>("Sprite/30");
            storageObject.Add(card);
        }*/
        storageObject = GameObject.Find("Main Camera").GetComponent<GameManager>().baseList;
    }
    public void InstantiateObj(Sprite sprite,string name)
    {
        GameObject obj = new GameObject();
        Button btn = obj.AddComponent<Button>();
        Image img = obj.AddComponent<Image>();
        Outline outLine = obj.AddComponent<Outline>();
        outLine.enabled = false;
        outLine.effectDistance = new Vector2(5, -5);
        img.sprite = sprite;
        obj.name = name;
        obj.transform.SetParent(fatherObj.transform);
        btn.targetGraphic = img;
        btn.onClick.AddListener(ShowDescription);
        obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    //更新物品
    public void Clear()
    {
        for (int i = 0; i < fatherObj.transform.childCount; i++)
            Destroy(fatherObj.transform.GetChild(i).gameObject);
    }
    public void UpdateObjects()
    {
        Clear();
        for (int i=0;i<storageObject.Count;i++)
        {
            InstantiateObj(storageObject[i].sprite, storageObject[i].name);
        }
        Debug.Log(storageObject.Count);
    }
    //显示描述
    public void ShowDescription()
    {
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        text.text = storageObject[btn.transform.GetSiblingIndex()].name;
    }
    void Start()
    {
        text = GameObject.Find("FactoryPanel").transform.Find("仓库Panel/Description").GetComponent<Text>();
        //UpdateList();
       //UpdateObjects();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        //Debug.Log(storageObject.Count);
    }
}
