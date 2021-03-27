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

    [SerializeField]
    private Text mainText;

    [SerializeField]
    private Text minorText;

    [SerializeField]
    private GameObject statusText;

    private PanelEnum panelEnum = PanelEnum.状态;

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
        if (baseObjs != null)
        {
            UpdateChart(baseObjs[0]);
            currentObj = baseObjs[0];
        }
        acidChart.SetActive(false);
        alcoholChart.SetActive(false);
        sugerChart.SetActive(false);
        statusText.SetActive(true);

    }
    private void SetCard(GameObject obj, BaseObject baseObj)
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
        if (GuideControl.id == 15)
        {
            GuideControl.id = 411;
            GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
        }

    }
    public void OnClickTag()
    {
        if (currentObj == null)
            return;
        ChoosePanel(EventSystem.current.currentSelectedGameObject);
        if (GuideControl.id == 16)
        {
            GuideControl.id = 414;
            GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
        }
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
                statusText.SetActive(false);
                acidBtn.GetComponent<Image>().color = new Color32(252, 255, 127, 255);
                alcoholBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                sugerBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                statusBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case PanelEnum.糖醇:
                acidChart.SetActive(false);
                alcoholChart.SetActive(false);
                sugerChart.SetActive(true);
                statusText.SetActive(false);
                acidBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                alcoholBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                sugerBtn.GetComponent<Image>().color = new Color32(252, 255, 127, 255);
                statusBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case PanelEnum.酒精含量:
                acidChart.SetActive(false);
                alcoholChart.SetActive(true);
                sugerChart.SetActive(false);
                statusText.SetActive(false);
                acidBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                alcoholBtn.GetComponent<Image>().color = new Color32(252, 255, 127, 255);
                sugerBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                statusBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            case PanelEnum.状态:
                acidChart.SetActive(false);
                alcoholChart.SetActive(false);
                sugerChart.SetActive(false);
                statusText.SetActive(true);
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
        糖醇, 有机酸, 酒精含量, 状态
    }
    private void UpdateChart(BaseObject obj)
    {
        alcoholChart.GetComponent<LineChart>().UpdateLine(obj.alcoholQueue);
        var a = GetInclusions(obj);
        sugerChart.GetComponent<PieChart>().UpdateChart(1, a.Item2[0], a.Item2[1], a.Item2[2], a.Item2[3], a.Item2[4], a.Item2[5]);
        acidChart.GetComponent<PieChart>().UpdateChart(1, a.Item1[0], a.Item1[1], a.Item1[2], a.Item1[3], a.Item1[4], a.Item1[5]);
        string main = "主料：";
        string minor = "辅料：";
        foreach (var b in obj.mains)
        {
            main += b.ToString() + " ";
        }
        foreach (var b in obj.minors)
        {
            minor += b.ToString() + " ";
        }
        mainText.text = main;
        minorText.text = minor;

    }
    private (List<Inclusion>, List<Inclusion>) GetInclusions(BaseObject obj)
    {
        List<Inclusion> res1 = new List<Inclusion>();
        List<Inclusion> res2 = new List<Inclusion>();
        float a = 0;
        float b = 0;
        float c = 0;
        float d = 0;
        float e = 0;
        switch (obj.GetKind())
        {
            case Kind.兼香型:
                a = 0.8f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.凤香型:
                a = 0.6f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.1f;
                break;
            case Kind.浓香型:
                a = 0.8f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.1f;
                break;
            case Kind.清香型:
                a = 0.6f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.特香型:
                a = 0.95f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.米香型:
                a = 0.8f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.老白干香型:
                a = 0.95f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.芝麻香型:
                a = 0.95f;
                b = 0.03f;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.药香型:
                a = 0.8f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.豉香型:
                a = 0.8f;
                b = 0;
                c = 0.7f;
                d = 0.1f;
                e = 0.01f;
                break;
            case Kind.酱香型:
                a = 0.95f;
                b = 0;
                c = 0.08f;
                d = 0.5f;
                e = 0.01f;
                break;
            case Kind.馥郁香型:
                a = 0.8f;
                b = 0;
                c = 0.08f;
                d = 0.1f;
                e = 0.01f;
                break;
        }
        res1.Add(new Inclusion("乳酸", a));
        res1.Add(new Inclusion("2-羟基异乙酸", b));
        res1.Add(new Inclusion("酒石酸", (1 - a - b) / 2));
        res1.Add(new Inclusion("山梨酸", (1 - a - b) * 0.3f));
        res1.Add(new Inclusion("苹果酸", (1 - a - b) * 0.2f));
        res1.Add(new Inclusion("柠檬酸", (1 - a - b) * 0.2f));
        res2.Add(new Inclusion("甘油", c));
        res2.Add(new Inclusion("阿拉伯糖醇", d));
        res2.Add(new Inclusion("山梨糖醇", e));
        res2.Add(new Inclusion("葡萄糖醇", (1 - c - d - e) / 2));
        res2.Add(new Inclusion("木糖醇", (1 - c - d - e) / 3));
        res2.Add(new Inclusion("半乳糖醇", (1 - c - d - e)*1.3f / 6));

        return (res1, res2);
    }

}
