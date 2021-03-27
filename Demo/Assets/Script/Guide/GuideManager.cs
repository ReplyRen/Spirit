using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class GuideManager : MonoBehaviour
{
    public Dictionary<int, GuideInfo> guideInfoDict = new Dictionary<int, GuideInfo>();
    private Dictionary<int, GameObject> guideObject = new Dictionary<int, GameObject>();
    private GameObject guideCanvas;
    private GameObject mask;
    private GameObject rectMask;
    private GameObject circleMask;
    private GameObject dialogImage;
    private GameObject dialog;
    private GameObject character;
    private GameObject character1;
    private GameObject character2;
    private GameObject character3;
    private GameObject mainCharacter;
    private GameObject Image;
    public DialogGraph dialogGraph;
    private GameObject round;
    public GameObject zhengZhu;
    public GameObject wanChen;
    public GameObject caiGouBu;
    public GameObject next;
    public GameObject zongLan;
    public GameObject zhuObject;
    public GameObject fuObject;
    public GameObject mySwitch;
    public GameObject buy;
    public GameObject close;
    public GameObject Status;
    public GameObject card1;
    public GameObject acid;
    public GameObject next1;
    public GameObject fenSuiJi;
    public void Start()
    {
        guideCanvas = GameObject.Find("GuideCanvas");
        round = GameObject.Find("圆盘");
        LoadData();
        disfObject();
        AddMaskEvent();
    }
    #region 数据处理
    /// <summary>
    /// 加载数值
    /// </summary>
    private void LoadData()
    {
        var nodeList = dialogGraph.nodes;
        for (int i = 0; i < nodeList.Count; i++)
        {
            Guidenode node = (Guidenode)nodeList[i];
            GuideInfo guideInfo = new GuideInfo();
            guideInfo.guideID = node.guideID;
            //Debug.Log(node.guideID);
            if (node.dialogNeed)
                guideInfo.dialogNeed = 1;
            else
                guideInfo.dialogNeed = 0;
            guideInfo.dialogText = node.dialogText;
            guideInfo.dialogCharacter = node.dialogCharacter;
            if (node.mainCharacter)
                guideInfo.mainCharacter = 1;
            else
                guideInfo.mainCharacter = 0;
            guideInfo.maskType = node.maskType;

            guideInfoDict.Add(guideInfo.guideID, guideInfo);
        }

    }
    public void AddMaskEvent()
    {
        guideInfoDict[109].circleMask = round.GetComponent<RectTransform>();
        guideInfoDict[111].rectMask = wanChen.GetComponent<RectTransform>();
        guideObject.Add(111, wanChen);
        guideInfoDict[201].rectMask = zhengZhu.GetComponent<RectTransform>();
        guideObject.Add(202, zhengZhu);
    }
    public void AddMaskEventDelay()
    {
        guideInfoDict[220].rectMask = caiGouBu.GetComponent<RectTransform>();
        guideObject.Add(220, caiGouBu);
        guideInfoDict[222].rectMask = zhuObject.GetComponent<RectTransform>();
        guideObject.Add(5, zhuObject);
        guideInfoDict[223].rectMask = mySwitch.GetComponent<RectTransform>();
        guideObject.Add(6, mySwitch);
        guideInfoDict[224].rectMask = fuObject.GetComponent<RectTransform>();
        guideObject.Add(7, fuObject);
        guideInfoDict[225].rectMask = buy.GetComponent<RectTransform>();
        guideObject.Add(8, buy);
        guideInfoDict[226].rectMask = close.GetComponent<RectTransform>();
        guideObject.Add(9, close);
        guideInfoDict[228].rectMask = next.GetComponent<RectTransform>();
        guideObject.Add(10, next);
        guideInfoDict[229].rectMask = next.GetComponent<RectTransform>();
        guideObject.Add(11, next);
        guideInfoDict[301].circleMask = zongLan.GetComponent<RectTransform>();
        //guideObject.Add(301, zongLan);
        guideInfoDict[302].rectMask = fenSuiJi.GetComponent<RectTransform>();
        //guideObject.Add(304, fenSuiJi);
        guideInfoDict[307].rectMask = zhengZhu.GetComponent<RectTransform>();
        guideObject.Add(307, zhengZhu);
        guideInfoDict[309].rectMask = fenSuiJi.GetComponent<RectTransform>();
        guideObject.Add(309, fenSuiJi);
        guideInfoDict[313].rectMask = Status.GetComponent<RectTransform>();
        //guideObject.Add(313, Status);
        guideInfoDict[314].rectMask = Status.GetComponent<RectTransform>();
        guideObject.Add(314, Status);
        guideInfoDict[315].rectMask = Status.GetComponent<RectTransform>();
        guideObject.Add(13, Status);
        guideInfoDict[410].rectMask = card1.GetComponent<RectTransform>();
        guideObject.Add(410, card1);
        guideInfoDict[413].circleMask = acid.GetComponent<RectTransform>();
        guideObject.Add(413, acid);
        guideInfoDict[507].circleMask = next1.GetComponent<RectTransform>();
        guideObject.Add(507, next1);
    }

    #endregion
    #region 操作
    private void Update()
    {
        if (guideObject.ContainsKey(GuideControl.id))
        {
            rectMask.GetComponent<EventPermeate>().target = guideObject[GuideControl.id];
            circleMask.GetComponent<EventPermeate>().target = guideObject[GuideControl.id];
        }
    }
    private void disfObject()
    {
        mask = guideCanvas.transform.GetChild(0).gameObject;
        rectMask = mask.transform.GetChild(0).gameObject;
        circleMask = mask.transform.GetChild(1).gameObject;

        dialogImage = guideCanvas.transform.GetChild(1).gameObject;
        dialog = dialogImage.transform.GetChild(0).gameObject;

        character = guideCanvas.transform.GetChild(2).gameObject;
        character1 = character.transform.GetChild(0).gameObject;
        character2 = character.transform.GetChild(1).gameObject;
        character3 = character.transform.GetChild(3).gameObject;
        mainCharacter = character.transform.GetChild(2).gameObject;
        Image = guideCanvas.transform.GetChild(3).gameObject;
    }
    public void Show(int GuideID)
    {
        if (!guideCanvas.activeInHierarchy)
            guideCanvas.SetActive(true);
        GuideInfo guideInfo = new GuideInfo();
        guideInfoDict.TryGetValue(GuideID, out guideInfo);
        if (guideInfo.maskType == 0)
        {
            mask.SetActive(false);
            Image.SetActive(true);
        }
        else if (guideInfo.maskType == 1)
        {
            Image.SetActive(false);
            mask.SetActive(true);
            rectMask.SetActive(true);
            rectMask.GetComponent<RectGuidanceController>().SetTarget(guideInfo.rectMask);
            circleMask.SetActive(false);
        }   
        else if (guideInfo.maskType == 2)
        {
            Image.SetActive(false);
            mask.SetActive(true);
            rectMask.SetActive(false);
            circleMask.SetActive(true);
            circleMask.GetComponent<CircleGuidanceController>().SetTarget(guideInfo.circleMask);
        }
        else if(guideInfo.maskType==3)
        {
            Image.SetActive(false);
            mask.SetActive(true);
        }
        dialog.GetComponent<Text>().text = guideInfo.dialogText;
        if (guideInfo.dialogNeed == 0)
            dialogImage.SetActive(false);
        else
            dialogImage.SetActive(true);
        switch(guideInfo.dialogCharacter)
        {
            case 0:
                character1.SetActive(false);
                character2.SetActive(false);
                character3.SetActive(false);
                break;
            case 1:
                character1.SetActive(true);
                character2.SetActive(false);
                character3.SetActive(false);
                break;
            case 2:
                character1.SetActive(false);
                character2.SetActive(true);
                character3.SetActive(false);
                break;
            case 3:
                character1.SetActive(false);
                character2.SetActive(false);
                character3.SetActive(true);
                break;
        }
        if (guideInfo.mainCharacter == 0)
            mainCharacter.SetActive(false);
        else
            mainCharacter.SetActive(true);
    }
    public void Hide()
    {
        if(guideCanvas.activeInHierarchy)
            guideCanvas.SetActive(false);
    }
    #endregion
}
