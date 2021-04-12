using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> buttonList = new List<GameObject>();
    public List<GameObject> panelList = new List<GameObject>();
    public List<GameObject> confirmList = new List<GameObject>();
    public List<GameObject> quickSetList = new List<GameObject>();
    public List<BaseFragment> fragmentsOnDisc;
    public Shader shader;
    public Material mt;
    public List<Material> mats;
    ViewPanel viewPanel;
    GameManager gameManager;
    CameraController cameraController;
    List<GameObject> panelCanvas = new List<GameObject>();
    List<string> names1=new List<string>();
    List<string> names2 = new List<string>();
    GuideControl guideControl;
    GuideManager guideManager;
    int currentUI;//当前打开UI在list中的序号
    int num = 10;//UI总数
    public static bool isOpen = false;
    private bool check = true;
    string[] str1 = null;
    string[] str2 = null;
    string[] str3 = null;
    bool isConfirm = false;
    UIManager instance = null;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    public void CloseFactory()//关闭工厂panel
    {
        panelList[11].SetActive(false);
    }
    public void OpenFactory()//打开工厂panel
    {
        panelList[11].SetActive(true);
    }
    //打开/关闭panel
    public void OpenUI()
    {
        Slider slider;
        ///获取当前button并打开相应panel/打开总览
        if (!isOpen)
        {
            var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (btn.name == "总览") 
            {
                panelList[currentUI].SetActive(true);
                isOpen = true;
                panelCanvas[3].SetActive(true);
                cameraController.locked = true;
            }
            else if (btn.name == "仓库")
            {
                viewPanel.Init(gameManager.baseList);
                //panelList[10].SetActive(true);
                panelCanvas[3].SetActive(true);
                cameraController.locked = true;
                if(!guideControl.newGamer)
                {
                    var a = gameManager.GetDeviation(fragmentsOnDisc[fragmentsOnDisc.Count - 1].baseObject);
                    if (a.deviation <= 0.05)
                        GuideControl.id = 901;
                    else if (a.deviation > 0.05 && a.deviation <= 0.1) 
                    {
                        GuideControl.id = 911;
                        GuideInfo guideInfo = new GuideInfo();
                        guideManager.guideInfoDict.TryGetValue(911, out guideInfo);
                        if (str2 == null) str2 = guideInfo.dialogText.Split('，');
                        guideInfo.dialogText = str2[0];
                        guideInfo.dialogText += "<color=red>" + a.kind + "</color>";
                        guideInfo.dialogText += str2[1];
                    }
                    else
                    {
                        GuideControl.id = 921;
                        GuideInfo guideInfo = new GuideInfo();
                        guideManager.guideInfoDict.TryGetValue(921, out guideInfo);
                        if (str3 == null) str3 = guideInfo.dialogText.Split('，');
                        guideInfo.dialogText = str3[0];
                        guideInfo.dialogText += "<color=red>" + a.kind + "</color>";
                        guideInfo.dialogText += str3[1];
                    }
                    guideControl.Run();
                }
            }
            else if (btn.GetComponent<UIObject>().isUse && cameraController.large)
            {
                panelList[btn.GetComponent<UIObject>().index].SetActive(true);
                currentUI = btn.GetComponent<UIObject>().index;
                isOpen = true;
                panelCanvas[3].SetActive(true);
                cameraController.locked = true;
                if(currentUI==9)
                {
                    if (!guideControl.newGamer)
                    {
                        int a = Random.Range(1, 10);
                        if (a % 2 == 0)
                        {
                            GuideControl.id = 801;
                            guideControl.Run();
                        }
                        else
                        {
                            GuideControl.id = 811;
                            guideControl.Run();
                        }
                    }
                    else
                    {
                        GuideControl.id = 501;
                        guideControl.Run();
                    }
                }
            }
            if (GuideControl.id == 4 && currentUI == 0)
            {
                GuideControl.id = 221;
                guideControl.Run();
            }
            if (GuideControl.id == 12 && currentUI == 1)
            {
                GuideControl.id = 310;
                guideControl.Run();
            }
            if (GuideControl.id == 14 && btn.name == "仓库")
            {
                GuideControl.id = 401;
                guideControl.Run();
            }
            btn = null;
        }
        else
        {
            panelList[currentUI].SetActive(true);
            panelCanvas[3].SetActive(true);
            cameraController.locked = true;
            if (GuideControl.id == 4 && currentUI == 0)
            {
                //Debug.Log("dadada");
                GuideControl.id = 221;
                guideControl.Run();
            }
            if (GuideControl.id == 12 && currentUI == 1)
            {
                GuideControl.id = 308;
                guideControl.Run();
            }
            if (GuideControl.id == 14)
            {
                GuideControl.id = 401;
                guideControl.Run();
            }
            if (GuideControl.id == 18)
            {
                GuideControl.id = 508;
                guideControl.Run();
            }
        }
            ///若参数未确认，则设置参数初值
        if (!buttonList[currentUI].GetComponent<UIObject>().isConfirm && !panelList[10].activeSelf)
        {
            switch (buttonList[currentUI].name)
            {
                case "粉碎机":
                    panelList[currentUI].GetComponent<SmashPanel>().Init();
                    break;
                case "储藏室":
                    panelList[currentUI].GetComponent<StorePanel>().Init();
                    break;
                case "蒸煮锅":
                    panelList[currentUI].GetComponent<CookPanel>().Init();
                    break;
                case "窖池":
                    panelList[currentUI].GetComponent<PitsPanel>().Init();
                    break;
                case "甑子":
                    panelList[currentUI].GetComponent<SteamerPanel>().Init();
                    break;
                case "蒸馏设备":
                    panelList[currentUI].GetComponent<DistillationPanel>().Init();
                    break;
                case "酒窖":
                    panelList[currentUI].GetComponent<CellarPanel>().Init();
                    break;
                case "调酒室":
                    panelList[currentUI].GetComponent<MixPanel>().Init();
                    break;
            }
            for (int i = 0; i < panelList[currentUI].transform.childCount; i++)
            {
                try
                {
                    slider = GameObject.Find(panelList[currentUI].name).transform.GetChild(i).GetComponent<Slider>();
                    slider.interactable = true;
                }
                catch { }
            }
        }
        
    }
    public void CloseUI()
    {
        if (panelList[currentUI].activeSelf)
            panelList[currentUI].SetActive(false);
        else
        {
            panelList[10].SetActive(false);
        }
        cameraController.locked = false;
        isOpen = false;
        panelCanvas[3].SetActive(false);
        if (GuideControl.id == 9 && currentUI == 0) 
        {
            GuideControl.id = 227;
            guideControl.Run();
        }
    }
    //
    //确认并检查是否完成设置
    public void Confirm()
    {
        Slider slider;
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        buttonList[currentUI].transform.GetComponent<UIObject>().isConfirm = true;
        mats[currentUI].SetFloat("_Flag", 1);
        if (panelList[currentUI].name != "采购部Panel" && panelList[currentUI].name != "评价Panel") btn.SetActive(false);
        try
        {
            quickSetList[currentUI - 1].SetActive(false);
        }
        catch { }
        for(int i=0;i<panelList[currentUI].transform.childCount;i++)
        {
            try
            {
                slider = GameObject.Find(panelList[currentUI].name).transform.GetChild(i).GetComponent<Slider>();
                slider.interactable = false;
            }
            catch {}
        }
        isConfirm = true;
        for(int i=0;i<buttonList.Count-1;i++)
        {
            if (buttonList[i].GetComponent<UIObject>().isUse && !buttonList[i].GetComponent<UIObject>().isConfirm) isConfirm = false;
        }
        if (isConfirm) buttonList[buttonList.Count - 1].SetActive(true);
        if (GuideControl.id == 13 && currentUI == 1)
        {
            GuideControl.id = 315;
            guideControl.Run();
        }
    }
    //
    //上一个/下一个panel
    public void Next()
    {
        panelList[currentUI].SetActive(false);
        currentUI++;
        if (currentUI > num - 1)
            currentUI = 0;
        while (!buttonList[currentUI].transform.GetComponent<UIObject>().isUse)
        {
            currentUI++;
            if (currentUI > num - 1)
                currentUI = 0;
        }
        OpenUI();

    }
    public void Previous()
    {
        panelList[currentUI].SetActive(false);
        currentUI--;
        if (currentUI < 0)
            currentUI = 9;
        while (!buttonList[currentUI].transform.GetComponent<UIObject>().isUse)
        {
            currentUI--;
            if (currentUI < 0)
                currentUI = 9;
        }
        OpenUI();
    }
    void SetStatus(bool a)
    {
        panelCanvas[1].SetActive(a);
        panelCanvas[2].SetActive(a);
    }
    #region 初始化相关
    /// <summary>
    /// 初始化函数
    /// </summary>
    public void InitializeFactory(List<BaseFragment> newList)
    {
        panelList[11].SetActive(true);
        fragmentsOnDisc = newList;
        ResetState();
        ResetOutline();
        SetStatus(true);
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            switch (fragmentsOnDisc[i].name)
            {
                case "配料":
                    buttonList[2].SetActive(true);
                    buttonList[3].SetActive(false);
                    break;
                case "蒸煮摊凉":
                    buttonList[2].SetActive(false);
                    buttonList[3].SetActive(true);
                    break;
                case "看花摘酒":
                case "陈酿":
                    buttonList[7].SetActive(true);
                    buttonList[8].SetActive(false);
                    break;
                case "勾兑勾调":
                    buttonList[7].SetActive(false);
                    buttonList[8].SetActive(true);
                    break;
                /*case "灌装":
                    buttonList[7].SetActive(false);
                    buttonList[8].SetActive(false);
                    buttonList[9].SetActive(true);
                    break;*/
            }
            names1.Add(fragmentsOnDisc[i].name);
        }
        if(!guideControl.newGamer)
        {
            GuideControl.id = 701;
            if (GuideControl.id == 701)
            {
                GuideInfo guideInfo = new GuideInfo();
                guideManager.guideInfoDict.TryGetValue(701, out guideInfo);
                if(str1==null) str1 = guideInfo.dialogText.Split('，');
                guideInfo.dialogText = str1[0];
                for(int i=0;i<names2.Count;i++)
                {
                    if (i == names2.Count - 1 && names2.Count > 1) 
                    {
                        guideInfo.dialogText += "和" +names2[i] ;
                    }
                    else
                    {
                        guideInfo.dialogText += "，" + names2[i] ;
                    }
                }
                guideInfo.dialogText += str1[1];
            }
            guideControl.Run();
        }
    }
    bool a;
    public void ResetState()///初始化工厂
    {
        a = false;
        for (int i = 0; i < num; i++)
        {
            buttonList[i].GetComponent<UIObject>().isUse = false;
            buttonList[i].GetComponent<UIObject>().isConfirm = false;
        }
        for (int i = 0; i < confirmList.Count - 1; i++)
        {
            confirmList[i].SetActive(true);
            try
            {
                quickSetList[i].SetActive(true);
            }
            catch { }
        }
        //confirmList[9].GetComponent<UIObject>().isConfirm = false;
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            switch (fragmentsOnDisc[i].name)
            {
                case "原、辅料准备":
                    buttonList[0].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 0;
                    break;
                case "粉碎润料":
                    buttonList[1].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 1;
                    break;
                case "配料":
                    buttonList[2].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 2;
                    break;
                case "蒸煮摊凉":
                    buttonList[3].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 3;
                    break;
                case "修窖":
                case "制曲、入曲":
                case "发酵":
                case "加原辅料":
                    buttonList[4].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 4;
                    break;
                case "上甑":
                    buttonList[5].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 5;
                    break;
                case "蒸馏":
                case "看花摘酒":
                    buttonList[6].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 6;
                    break;
                case "陈酿":
                    buttonList[7].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 7;
                    break;
                case "勾兑勾调":
                    buttonList[8].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 8;
                    break;
                case "鉴酒":
                    buttonList[9].GetComponent<UIObject>().isUse = true;
                    a = true;
                    currentUI = 9;
                    break;
            }
        }
        if (a) buttonList[buttonList.Count - 1].SetActive(false);
    }
    public void ResetOutline()///初始化高亮
    {
        for (int i = 0; i < num; i++)
        {
            if (buttonList[i].GetComponent<UIObject>().isUse) mats[i].SetFloat("_Flag", 0);
            else mats[i].SetFloat("_Flag", 1);
        }
    }
    #endregion

    //快速设置
    public void QuickSet()
    {

    }
    //

    public void NextDay()
    {
        names2.Clear();
        for(int i=0;i<names1.Count;i++)
        {
            names2.Add(names1[i]);
        }
        names1.Clear();
        SetStatus(false);
        panelList[11].SetActive(false);
    }
    //
    void Start()
    {
        guideManager = GameObject.Find("Main Camera").GetComponent<GuideManager>();
        guideControl = GameObject.Find("Main Camera").GetComponent<GuideControl>();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        viewPanel = GameObject.Find("PanelCanvas").transform.Find("检视Panel").GetComponent<ViewPanel>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        fragmentsOnDisc = gameManager.fragmentOnDisc;
        GameObject temp;
        int i;
        for (i = 7; i < num + 8; i++) /// 初始化list
        {
            Transform text;
            try
            {
                temp = GameObject.Find("FactoryPanel").transform.GetChild(i).gameObject;
                text = temp.transform.Find("Text");
                if (temp.name != "Next")
                {
                    text.GetComponent<Text>().text = temp.name;
                    buttonList.Add(temp);
                    Material mat = Instantiate(mt);
                    mat.SetFloat("_Flag", 0);
                    mat.SetFloat("_MinOffset", 8f);
                    mat.SetColor("_OutLineCol", Color.yellow);
                    temp.GetComponent<Image>().material = mat;
                    mats.Add(temp.GetComponent<Image>().material);
                }
            }
            catch { }
            //
            temp = GameObject.Find("PanelCanvas").transform.GetChild(i - 3).gameObject;
            try
            {
                text = temp.transform.Find("Title");
                text.GetComponent<Text>().text = temp.name.Replace("Panel", "");
            }
            catch { }
            panelList.Add(temp);
            //
            try
            {
                temp = panelList[i - 7].transform.Find("Confirm").gameObject;
                confirmList.Add(temp);
                temp = panelList[i - 7].transform.Find("QuickSet").gameObject;
                quickSetList.Add(temp);
            }
            catch { }
        }
        temp = GameObject.Find("PanelCanvas").transform.Find("采购部Panel/Buy").gameObject;
        confirmList.Add(temp);
        temp = GameObject.Find("FactoryPanel");
        panelList.Add(temp);
        buttonList.Add(temp.transform.Find("Next").gameObject);
        for (i = 0; i < 4; i++)
        {
            temp = GameObject.Find("PanelCanvas").transform.GetChild(i).gameObject;
            panelCanvas.Add(temp);
        }
        ResetOutline();
        panelCanvas[3].SetActive(false);
        SetStatus(false);
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CloseUI();
    }
}
