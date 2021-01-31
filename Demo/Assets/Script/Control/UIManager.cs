using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> buttonList = new List<GameObject>();
    public List<GameObject> panelList = new List<GameObject>();
    public List<Image> gridList = new List<Image>();
    public List<BaseFragment> fragmentsOnDisc;
    public GameObject temp;
    public Slider status;
    public Slider statusSet;
    public float statusValue,setValue;//参数/调整初值（连续变化）
    public float setValue2;//调整数值（离散）
    public float parameter;//倍数（连续变化）
    float valueChange;
    int currentUI;//当前打开UI在list中的序号
    int num = 12;//UI总数
    bool isOpen = false;
    bool isLast = false;

    UIManager instance = null;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
   
    public void SetState(string name)
    {
        temp = GameObject.Find(name);
        temp.GetComponent<UIObject>().isUse = true;
        temp = null;
    }
    public void CloseFactory()//关闭工厂panel
    {
        temp = GameObject.Find("Canvas").transform.Find("Factorypanel").gameObject;
        temp.SetActive(false);
        temp = null;
    }
    public void OpenFactory()//打开工厂panel
    {
        temp = GameObject.Find("Canvas").transform.Find("Factorypanel").gameObject;
        temp.SetActive(false);
        temp = null;
    }
    //打开/关闭panel
    public void OpenUI()
    {
        ///获取当前button并打开相应panel/打开总览
        if (!isOpen)
        {
            var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (btn.name == "Overview")
                panelList[currentUI].SetActive(true);
            else if(btn.name=="Storage")
            {
                temp = GameObject.Find("FactoryPanel").transform.Find("StoragePanel").gameObject;
                temp.SetActive(true);
            }
            else if (btn.GetComponent<UIObject>().isUse)
            {
                for (int i = 0; i < num; i++)
                {
                    if (btn.name + "Panel" == panelList[i].name)
                    {
                        panelList[i].SetActive(true);
                        currentUI = i;
                    }
                } 
            }
            isOpen = true;
            btn = null;
        }
        else
        {
            panelList[currentUI].SetActive(true);
        }
        ///若参数未确认，则设置参数初值
        if (!buttonList[currentUI].GetComponent<UIObject>().isConfirm)
        {
            status = GameObject.Find(panelList[currentUI].name + "/Status").GetComponent<Slider>();
            statusSet = GameObject.Find(panelList[currentUI].name + "/StatusSet").GetComponent<Slider>();
            gridList.Clear();
            for (int i = 0; i < 10; i++)
            {
                Image tempImage = GameObject.Find(panelList[currentUI].name + "/Grid").transform.GetChild(i).GetComponent<Image>();
                gridList.Add(tempImage);
                if (i < setValue2 * 10)
                {
                    gridList[i].color = new Color(0, 0, 0);
                }
            }
            status.value = statusValue;
            statusSet.value = setValue;
        }
    }
    public void CloseUI()
    {
        if(panelList[currentUI].activeSelf)
            panelList[currentUI].SetActive(false);
        else
        {
            temp = GameObject.Find("FactoryPanel").transform.Find("StoragePanel").gameObject;
            temp.SetActive(false);
            temp = null;
        }
        isOpen = false;
    }
    //
    //确认并检查是否完成设置
    public void Confirm()
    {
        buttonList[currentUI].transform.GetComponent<UIObject>().isConfirm = true;
        buttonList[currentUI].GetComponent<Outline>().enabled = false;
        temp = GameObject.Find(panelList[currentUI].name + "/Confirm");
        temp.SetActive(false);
        temp = GameObject.Find(panelList[currentUI].name + "/QuickSet");
        temp.SetActive(false);
        CloseUI();
        for(int i=0;i<num;i++)
        {
            if (buttonList[i].transform.GetComponent<UIObject>().isUse && !buttonList[i].transform.GetComponent<UIObject>().isConfirm) 
            {
                break;
            }
            if (i == 11)
                isLast = true;
        }
        if(isLast)
        {
            temp = GameObject.Find("FactoryPanel").transform.Find("NextDay").gameObject;
            temp.SetActive(true);
            temp = null;
        }
    }
    //
    //上一个/下一个panel
    public void Next()
    {
        panelList[currentUI].SetActive(false);
        currentUI++;
        if (currentUI > 11)
            currentUI = 0;
        while (!buttonList[currentUI].transform.GetComponent<UIObject>().isUse)
        {
            currentUI++;
        }
        OpenUI();
    }
    public void Previous()
    {
        panelList[currentUI].SetActive(false);
        currentUI--;
        if (currentUI < 0)
            currentUI = 11;
        while (!buttonList[currentUI].transform.GetComponent<UIObject>().isUse)
        {
            currentUI--;
        }
        OpenUI();
    }
    
    #region 初始化相关
    public void ResetState()///初始化工厂
    {
        for(int i=0;i<num;i++)
        {
            buttonList[i].GetComponent<UIObject>().isUse = false;
            buttonList[i].GetComponent<UIObject>().isConfirm = false;
        }
        for(int i=0;i< fragmentsOnDisc.Count; i++)
        {
            switch (fragmentsOnDisc[i].name)
            {
                case "原、辅料准备": 
                    SetState("Purchase");
                    break;
                case "粉碎、润料":
                    SetState("Smash");
                    break;
                case "配料":
                    SetState("Store");
                    break;
                case "蒸煮、摊凉":
                    SetState("Cook");
                    break;
                case "修窖": case "制曲、入曲": case "发酵":case "加原/辅料":
                    SetState("Pits");
                    break;
                case "上甑":
                    SetState("Steamer");
                    break;
                case "蒸馏": case "看花摘酒":
                    SetState("Distillation");
                    break;
                case "陈酿":
                    SetState("Cellar");
                    break;
                case "勾兑/勾调":
                    SetState("Mix");
                    break;
                case "离心杀菌":
                    SetState("Centrifugation");
                    break;
                case "灌装":
                    SetState("Bottle");
                    break;
                case "鉴酒":
                    SetState("Show");
                    break;
            }
        }
    }
    public void ResetOutline()///初始化高亮
    {
        for(int i=0;i<num;i++)
        {
            if (buttonList[i].GetComponent<UIObject>().isUse) buttonList[i].GetComponent<Outline>().enabled = true;
            else buttonList[i].GetComponent<Outline>().enabled = false;
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
        ResetState();
        ResetOutline();
        temp = GameObject.Find("NextDay");
        temp.SetActive(false);
        temp = null;
    }
    //
    void Start()
    {
        fragmentsOnDisc = GameObject.Find("Main Camera").GetComponent<GameManager>().fragmentOnDisc;
        int i;
        for (i = 1; i < num+1; i++)/// 初始化list
        {
            temp = GameObject.Find("FactoryPanel").transform.GetChild(i).gameObject;
            temp.GetComponent<UIObject>().isConfirm = false;
            var text = temp.transform.Find("Text");
            text.GetComponent<Text>().text = temp.name;
            buttonList.Add(temp);
            //
            temp = GameObject.Find("FactoryPanel").transform.GetChild(i + num +1).gameObject;
            text = temp.transform.Find("Title");
            text.GetComponent<Text>().text = temp.name;
            panelList.Add(temp);
        }
        ResetOutline();
        temp = null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CloseUI();
        if (status != null && !buttonList[currentUI].GetComponent<UIObject>().isConfirm) //连续变化
        {
            valueChange = statusSet.value - setValue;
            status.value = statusValue + parameter * valueChange;
        }
        if(gridList.Count>1 && !buttonList[currentUI].GetComponent<UIObject>().isConfirm)//离散变化
        {
            valueChange = statusSet.value - setValue2;
            for(int i=0;i<10;i++)
            {
                gridList[i].color = new Color(255, 255, 255);
            }
            for(int i=0;i<5+10*valueChange*0.4;i++)
            {
                gridList[i].color = new Color(0, 0, 0);
            }
        }
    }
}
