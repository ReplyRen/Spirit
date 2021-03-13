using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewPanel : MonoBehaviour
{
    [SerializeField]
    private Button statusBtn;

    [SerializeField]
    private Button acidBtn;

    [SerializeField]
    private Button sugerBtn;

    [SerializeField]
    private Button alcoholBtn;

    [SerializeField]
    private List<GameObject> cards;

    private Dictionary<GameObject, BaseObject> pairs = new Dictionary<GameObject, BaseObject>();

    private BaseObject currentObj;

    private void Start()
    {
        Test();
    }
    private void Test()
    {
        List<BaseObject> baseObjs = new List<BaseObject>();
        BaseObject a = new BaseObject();
        a.mains.Add(主料.麸皮);
        a.mainStep = 3;
        a.name = "酒";
        baseObjs.Add(a);
        Init(baseObjs);
    }

    public void Init(List<BaseObject> baseObjs)
    {
        gameObject.SetActive(true);
        int i = 0;
        for (; i < baseObjs.Count; i++)
        {
            pairs.Add(cards[i], baseObjs[i]);
            SetCard(cards[i], baseObjs[i]);
        }
        for (; i < cards.Count; i++)
        {
            cards[i].SetActive(false);
        }
    }
    private void SetCard(GameObject obj,BaseObject baseObj)
    {
        string name = ReturnMain(baseObj);
        obj.transform.Find("Main").GetComponent<Image>().sprite = StaticMethod.LoadSprite("Sprite/检视素材/" + name);
        obj.transform.Find("Step").GetComponent<Image>().sprite = StaticMethod.LoadSprite("Sprite/检视素材/" + baseObj.mainStep);
    }
    private string ReturnMain(BaseObject obj)
    {
        if (obj.mains.Contains(主料.麸皮))
            return "糯性高粱小麦麸皮";
        else if (obj.mains.Contains(主料.大米) && obj.mains.Count == 1)
        {
            return "大米";
        }
        else if (obj.mains.Contains(主料.大米) && obj.mains.Count == 2)
            return "高粱大米";
        else if (obj.mains.Contains(主料.小麦) && obj.mains.Count == 2)
            return "高粱小麦";
        else if (obj.mains.Contains(主料.高粱) && obj.mains.Count == 1)
            return "高粱";
        else if (obj.mains.Contains(主料.糯性高梁) && obj.mains.Count == 1)
            return "糯性高粱";
        else
        {
            Debug.LogError("检视主料种类错误");
            return "高粱小麦";
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void OnClickCard()
    {
        currentObj = pairs[EventSystem.current.currentSelectedGameObject];
        Debug.Log(currentObj.name);
    }
    enum PanelEnum
    {
        酸,酯,醇,状态
    }
}
