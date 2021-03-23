using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class GuideManager : MonoBehaviour
{
    public Dictionary<int, GuideInfo> guideInfoDict = new Dictionary<int, GuideInfo>();
    private GameObject guideCanvas;
    private GameObject mask;
    private GameObject rectMask;
    private GameObject circleMask;
    private GameObject dialogImage;
    private GameObject dialog;
    private GameObject character;
    private GameObject character1;
    private GameObject character2;
    private GameObject mainCharacter;
    private GameObject Image;
    public DialogGraph dialogGraph;
    private GameObject round;
    public GameObject next;
    public GameObject zhuObject;
    public GameObject fuObject;
    public GameObject mySwitch;
    public GameObject buy;
    public GameObject close;
    public GameObject Status;
    public GameObject card1;
    public GameObject acid;
    public GameObject next1;
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
        guideInfoDict[111].rectMask = GameObject.Find("完成").GetComponent<RectTransform>();
        guideInfoDict[201].rectMask = GameObject.Find("替身201").GetComponent<RectTransform>();
    }
    public void AddMaskEventDelay()
    {
        guideInfoDict[220].rectMask = GameObject.Find("采购部").GetComponent<RectTransform>();
        guideInfoDict[222].rectMask = zhuObject.GetComponent<RectTransform>(); 
        guideInfoDict[223].rectMask = mySwitch.GetComponent<RectTransform>();
        guideInfoDict[224].rectMask = fuObject.GetComponent<RectTransform>();
        guideInfoDict[225].rectMask = buy.GetComponent<RectTransform>();
        guideInfoDict[226].rectMask = close.GetComponent<RectTransform>();
        guideInfoDict[228].rectMask = next.GetComponent<RectTransform>();
        guideInfoDict[229].rectMask = next.GetComponent<RectTransform>();
        guideInfoDict[301].circleMask = GameObject.Find("总览").GetComponent<RectTransform>();
        guideInfoDict[302].rectMask = GameObject.Find("粉碎机").GetComponent<RectTransform>();
        guideInfoDict[307].rectMask = GameObject.Find("粉碎机").GetComponent<RectTransform>();
        guideInfoDict[311].rectMask = Status.GetComponent<RectTransform>();
        guideInfoDict[312].rectMask = Status.GetComponent<RectTransform>();
        guideInfoDict[313].rectMask = Status.GetComponent<RectTransform>();
        guideInfoDict[410].rectMask = card1.GetComponent<RectTransform>();
        guideInfoDict[413].circleMask = acid.GetComponent<RectTransform>();
        guideInfoDict[507].circleMask = next1.GetComponent<RectTransform>();
    }
   
    #endregion
    #region 操作
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
                break;
            case 1:
                character1.SetActive(true);
                character2.SetActive(false);
                break;
            case 2:
                character1.SetActive(false);
                character2.SetActive(true);
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
