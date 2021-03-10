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
    public List<Image> gridList = new List<Image>();
    public List<BaseFragment> fragmentsOnDisc;
    public List<Material> mats;
    public Shader shader;
    public Material mt;
    CameraController cameraController;
    GameObject blur;
    int currentUI;//当前打开UI在list中的序号
    int num = 11;//UI总数
    public static bool isOpen = false;
    UIManager instance = null;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    public void CloseFactory()//关闭工厂panel
    {
        panelList[12].SetActive(false);
    }
    public void OpenFactory()//打开工厂panel
    {
        panelList[12].SetActive(true);
    }
    //打开/关闭panel
    public void OpenUI()
    {
        Slider slider;
        blur.SetActive(true);
        ///获取当前button并打开相应panel/打开总览
        if (!isOpen)
        {
            var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (btn.name == "总览")
            {
                panelList[currentUI].SetActive(true);
                isOpen = true;
            }
            else if (btn.name == "仓库")
            {
                panelList[11].SetActive(true);
            }
            else if (btn.GetComponent<UIObject>().isUse && !cameraController.small)
            {
                panelList[btn.GetComponent<UIObject>().index].SetActive(true);
                currentUI = btn.GetComponent<UIObject>().index;
                isOpen = true;
            }
            btn = null;
        }
        else
        {
            panelList[currentUI].SetActive(true);
        }
        ///若参数未确认，则设置参数初值
        if (!buttonList[currentUI].GetComponent<UIObject>().isConfirm && !panelList[11].activeSelf)
        {
            switch (buttonList[currentUI].name)
            {
                case "粉碎机":
                    panelList[currentUI].GetComponent<SmashPanel>().Init();
                    break;
                case "储藏室":
                    panelList[currentUI].GetComponent<StorePanel>().Init();
                    break;
                case "窖池":
                    panelList[currentUI].GetComponent<PitsPanel>().Init();
                    break;
                case "蒸馏设备":
                    panelList[currentUI].GetComponent<DistillationPanel>().Init();
                    break;
                case "酒窖":
                    panelList[currentUI].GetComponent<CellarPanel>().Init();
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
            panelList[11].SetActive(false);
        }
        isOpen = false;
        blur.SetActive(false);
    }
    //
    //确认并检查是否完成设置
    public void Confirm()
    {
        Slider slider;
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        buttonList[currentUI].transform.GetComponent<UIObject>().isConfirm = true;
        mats[currentUI].SetFloat("_Flag", 1);
        btn.SetActive(false);
        /*if (currentUI > 0)
        {
            confirmList[currentUI - 1].SetActive(false);
            quickSetList[currentUI - 1].SetActive(false);
        }*/
        for(int i=0;i<panelList[currentUI].transform.childCount;i++)
        {
            try
            {
                slider = GameObject.Find(panelList[currentUI].name).transform.GetChild(i).GetComponent<Slider>();
                slider.interactable = false;
            }
            catch {}
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
            currentUI = 10;
        while (!buttonList[currentUI].transform.GetComponent<UIObject>().isUse)
        {
            currentUI--;
            if (currentUI < 0)
                currentUI = 10;
        }
        OpenUI();
    }

    #region 初始化相关
    /// <summary>
    /// 初始化函数
    /// </summary>
    public void InitializeFactory(List<BaseFragment> newList)
    {
        panelList[12].SetActive(true);
        fragmentsOnDisc = newList;
        ResetState();
        ResetOutline();
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            switch (fragmentsOnDisc[i].name)
            {
                case "配料":
                    buttonList[2].SetActive(true);
                    buttonList[4].SetActive(false);
                    break;
                case "修窖":
                    buttonList[2].SetActive(false);
                    buttonList[4].SetActive(true);
                    break;
                case "看花摘酒":
                case "陈酿":
                    buttonList[7].SetActive(true);
                    buttonList[8].SetActive(false);
                    buttonList[9].SetActive(false);
                    break;
                case "勾兑勾调":
                    buttonList[7].SetActive(false);
                    buttonList[8].SetActive(true);
                    buttonList[9].SetActive(false);
                    break;
                case "灌装":
                    buttonList[7].SetActive(false);
                    buttonList[8].SetActive(false);
                    buttonList[9].SetActive(true);
                    break;
            }
        }
    }
    public void ResetState()///初始化工厂
    {
        for (int i = 0; i < num; i++)
        {
            buttonList[i].GetComponent<UIObject>().isUse = false;
            buttonList[i].GetComponent<UIObject>().isConfirm = false;
        }
        for (int i = 0; i < confirmList.Count; i++)
        {
            confirmList[i].SetActive(true);
            try
            {
                quickSetList[i].SetActive(true);
            }
            catch { }
        }
        confirmList[10].GetComponent<UIObject>().isConfirm = false;
        for (int i = 0; i < fragmentsOnDisc.Count; i++)
        {
            switch (fragmentsOnDisc[i].name)
            {
                case "原、辅料准备":
                    buttonList[0].GetComponent<UIObject>().isUse = true;
                    currentUI = 0;
                    break;
                case "粉碎润料":
                    buttonList[1].GetComponent<UIObject>().isUse = true;
                    currentUI = 1;
                    break;
                case "配料":
                    buttonList[2].GetComponent<UIObject>().isUse = true;
                    currentUI = 2;
                    break;
                case "蒸煮摊凉":
                    buttonList[3].GetComponent<UIObject>().isUse = true;
                    currentUI = 3;
                    break;
                case "修窖":
                case "制曲、入曲":
                case "发酵":
                case "加原辅料":
                    buttonList[4].GetComponent<UIObject>().isUse = true;
                    currentUI = 4;
                    break;
                case "上甑":
                    buttonList[5].GetComponent<UIObject>().isUse = true;
                    currentUI = 5;
                    break;
                case "蒸馏":
                case "看花摘酒":
                    buttonList[6].GetComponent<UIObject>().isUse = true;
                    currentUI = 6;
                    break;
                case "陈酿":
                    buttonList[7].GetComponent<UIObject>().isUse = true;
                    currentUI = 7;
                    break;
                case "勾兑勾调":
                    buttonList[8].GetComponent<UIObject>().isUse = true;
                    currentUI = 8;
                    break;
                case "灌装":
                    buttonList[9].GetComponent<UIObject>().isUse = true;
                    currentUI = 9;
                    break;
                case "鉴酒":
                    buttonList[10].GetComponent<UIObject>().isUse = true;
                    currentUI = 10;
                    break;
            }
        }
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
        
        panelList[12].SetActive(false);
    }
    //
    void Start()
    {
        blur = GameObject.Find("PanelCanvas").transform.Find("采购部Panel").transform.Find("Blur").gameObject;
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        fragmentsOnDisc = GameObject.Find("Main Camera").GetComponent<GameManager>().fragmentOnDisc;
        GameObject temp;
        int i;
        for (i = 7; i < num + 8; i++) /// 初始化list
        {
            Transform text;
            try
            {
                temp = GameObject.Find("FactoryPanel").transform.GetChild(i).gameObject;
                text = temp.transform.Find("Text");
                text.GetComponent<Text>().text = temp.name;
                buttonList.Add(temp);
                Material mat = Instantiate(mt);
                mat.SetFloat("_Flag", 0);
                mat.SetFloat("_MinOffset", 8f);
                mat.SetColor("_OutLineCol", Color.yellow);
                temp.GetComponent<Image>().material = mat;
                mats.Add(temp.GetComponent<Image>().material);
            }
            catch { }
            //
            temp = GameObject.Find("PanelCanvas").transform.GetChild(i - 3).gameObject;
            text = temp.transform.Find("Title");
            text.GetComponent<Text>().text = temp.name.Replace("Panel","");
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
        //ResetState();
        ResetOutline();
        temp = null;
        blur.SetActive(false);
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CloseUI();
    }
}
