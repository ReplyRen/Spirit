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

    [SerializeField]
    private GameObject acidChart;

    [SerializeField]
    private GameObject sugerChart;

    [SerializeField]
    private GameObject alcoholChart;

    private PanelEnum panelEnum = PanelEnum.酒精含量;

    private BaseObject currentObj;

    private void Test()
    {
        List<BaseObject> baseObjs = new List<BaseObject>();
        BaseObject a = new BaseObject();
        a.mains.Add(主料.麸皮);
        a.mainStep = 3;
        BaseObject b = new BaseObject();
        b.mains.Add(主料.麸皮);
        b.mainStep = 3;
        b.name = "酒";
        BaseObject c = new BaseObject();
        c.mains.Add(主料.大米);
        c.mainStep = 3;
        c.name = "酒";
        baseObjs.Add(c);
        baseObjs.Add(a);
        baseObjs.Add(b);
        Init(baseObjs);
    }

    public void Init(List<BaseObject> baseObjs)
    {
        Debug.Log(baseObjs.Count);
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
        pairs.Clear();
        currentObj = null;
        acidChart.SetActive(false);
        alcoholChart.SetActive(false);
        sugerChart.SetActive(false);

        gameObject.SetActive(false);
    }
    public void OnClickCard()
    {
        currentObj = pairs[EventSystem.current.currentSelectedGameObject];
        UpdateChart(currentObj);


    }
    public void OnClickTag()
    {
        if (currentObj == null)
            return;
        ChoosePanel(EventSystem.current.currentSelectedGameObject);
    }
    private void ChoosePanel(GameObject obj)
    {
        switch (obj.name)
        {
            case "acid":
                panelEnum = PanelEnum.有机酸;
                break;
            case "suger":
                panelEnum = PanelEnum.糖醇;
                break;
            case "alcohol":
                panelEnum = PanelEnum.酒精含量;
                break;
            case "status":
                panelEnum = PanelEnum.状态;
                break;
        }
        switch (panelEnum)
        {
            case PanelEnum.有机酸:
                acidChart.SetActive(true);
                alcoholChart.SetActive(false);
                sugerChart.SetActive(false);
                acidBtn.GetComponent<Image>().color = new Color32(252, 255, 127, 255);
                alcoholBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                sugerBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                statusBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case PanelEnum.糖醇:
                acidChart.SetActive(false);
                alcoholChart.SetActive(false);
                sugerChart.SetActive(true);
                acidBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                alcoholBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                sugerBtn.GetComponent<Image>().color = new Color32(252, 255, 127, 255);
                statusBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case PanelEnum.酒精含量:
                acidChart.SetActive(false);
                alcoholChart.SetActive(true);
                sugerChart.SetActive(false);
                acidBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                alcoholBtn.GetComponent<Image>().color = new Color32(252, 255, 127, 255);
                sugerBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                statusBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case PanelEnum.状态:
                acidChart.SetActive(false);
                alcoholChart.SetActive(false);
                sugerChart.SetActive(false);
                acidBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                alcoholBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                sugerBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                statusBtn.GetComponent<Image>().color = new Color32(252, 255, 127, 255);
                break;
            default:
                Debug.LogError("面板状态error");
                break;
        }
    }
    enum PanelEnum
    {
        糖醇,有机酸, 酒精含量, 状态
    }
    private void UpdateChart(BaseObject obj)
    {
        alcoholChart.GetComponent<LineChart>().UpdateLine(obj.alcoholQueue);
        //switch
        //acidChart.GetComponent<PieChart>().UpdateChart()
    }
}
