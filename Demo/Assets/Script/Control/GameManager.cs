using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    /// <summary>
    /// 已过回合数
    /// </summary>
    public int roundCount;

    /// <summary>
    /// 经验
    /// </summary>
    public float experience;

    /// <summary>
    /// 等级
    /// </summary>
    public float level;

    /// <summary>
    /// 碎片队列
    /// </summary>
    public List<BaseFragment> fragmentList = new List<BaseFragment>();

    /// <summary>
    /// 圆盘上的碎片
    /// </summary>
    public List<BaseFragment> fragmentOnDisc = new List<BaseFragment>();

    /// <summary>
    /// 仓库物品队列
    /// </summary>
    public List<BaseObject> baseList = new List<BaseObject>();

    /// <summary>
    /// 圆盘
    /// </summary>
    public RoundPanel roundPanel;

    /// <summary>
    /// 工厂
    /// </summary>
    public UIManager uiManager;

    private void Awake()
    {
        roundPanel = GameObject.FindWithTag("RoundPanel").GetComponent<RoundPanel>();
        uiManager = GameObject.FindWithTag("FactoryPanel").GetComponent<UIManager>();
        LoadData();
        InitGame();
    }
    private void Start()
    {
        InitGame();
        Debug.Log(fragmentList.Count);
    }

    /// <summary>
    /// 初始化游戏
    /// </summary>
    private void InitGame()
    {
        roundCount = 0;
        experience = 0;
        level = 1;
        StartRound();
        foreach (var fragment in fragmentList)
            Debug.Log(fragment.name + ":" + fragment.model);
        roundPanel.InitialRoundPanel(fragmentList);
    }
    /// <summary>
    /// 加载游戏
    /// </summary>
    private void LoadGame()
    {

    }

    public void NextRoundClick()
    {
        EndRound();
        uiManager.NextDay();
        StartRound();
        roundPanel.InitialRoundPanel(fragmentList);
    }

    public void SwitchClick()
    {
        foreach (var fragment in roundPanel.HideRoundPanel())
            fragmentOnDisc.Add(fragment);
        uiManager.gameObject.SetActive(true);
        uiManager.InitializeFactory(fragmentOnDisc);
    }

    #region 流程逻辑
    /// <summary>
    /// 下一回合
    /// </summary>
    public void EndRound()
    {
        UpdateObject();
    }

    /// <summary>
    /// 回合开始
    /// </summary>
    private void StartRound()
    {
        roundCount++;
        WeatherData();
        UpdateFragment();
    }


    /// <summary>
    /// 更新仓库
    /// </summary>
    private void UpdateObject()
    {
        for (int i= 0;i < fragmentOnDisc.Count;i++)//遍历圆盘上的碎片
        {
            fragmentOnDisc[i].DurationDecrease();//时间自减
            if (fragmentOnDisc[i].finished)//如果完成了
            {
                if (fragmentOnDisc[i].name == "鉴酒")
                {
                    WineTasting(fragmentOnDisc[i].baseObject);
                }
                else
                {
                    if (fragmentOnDisc[i].baseObject != null)//移除仓库中此碎片的酒基
                        baseList.Remove(fragmentOnDisc[i].baseObject);
                    baseList.Add(FragmentToObject(fragmentOnDisc[i]));//添加新酒基
                }

                fragmentOnDisc.Remove(fragmentOnDisc[i]);//移除圆盘上此碎片
            }
        }
    } 
    private BaseObject FragmentToObject(BaseFragment fragment)
    {
        BaseObject baseObject = new BaseObject();
        if (fragment.name != "原、辅料准备")
            baseObject.element = fragment.element + fragment.baseObject.element;
        else
            baseObject.element = fragment.element;
        switch (fragment.name)
        {
            case "原、辅料准备":
                baseObject.name = "原、辅料";
                break;
            case "粉碎润料":
                baseObject.name = "原、辅料（碎）";
                break;
            case "蒸煮摊凉":
                baseObject.name = "原、辅料（煮）";
                break;
            case "配料":
                baseObject.name = "原、辅料（配）";
                break;
            case "修窖":
                baseObject.name = "原、辅料（窖）";
                break;
            case "制曲、入曲":
                baseObject.name = "原、辅料（加曲）";
                break;
            case "发酵":
                baseObject.name = "酒醅";
                break;
            case "加原辅料":
                baseObject.name = "酒醅（加料）";
                break;
            case "上甑":
                baseObject.name = "蒸馏产物（初）";
                break;
            case "蒸馏":
                baseObject.name = "蒸馏产物";
                break;
            case "看花摘酒":
                baseObject.name = "蒸馏产物（改）";
                break;
            case "陈酿":
                baseObject.name = "原酒";
                break;
            case "勾调":
                baseObject.name = "酒";
                break;
            case "灌装":
                baseObject.name = "酒（产物）";
                break;
            case "鉴酒":
                WineTasting(baseObject);
                break;
            default:
                Debug.LogError("碎片转物品错误");
                break;
        }
        return baseObject;
    }
    /// <summary>
    /// 仓检
    /// </summary>
    private void UpdateFragment()
    {
        fragmentList.Clear();//清除碎片队列
        foreach (var a in baseList)
        {
            List<BaseFragment> fragments = ObjectToFragment(a);
            foreach(var f in fragments)
            {
                fragmentList.Add(f);//添加碎片队列
            }
        }
        BaseFragment fragment = new BaseFragment();
        fragmentDic.TryGetValue("原、辅料准备", out fragment);
            fragmentList.Add(fragment);
    }
    private List<BaseFragment> ObjectToFragment(BaseObject baseObject)
    {
        List<BaseFragment> fragments = new List<BaseFragment>();
        List<string> name = new List<string>();
        switch (baseObject.name)
        {
            case "原、辅料":
                name.Add("粉碎润料");
                name.Add("蒸煮摊凉");
                name.Add("配料");
                break;
            case "原、辅料（碎）":
            case "原、辅料（煮）":
            case "原、辅料（配）":
                name.Add("修窖");
                break;
            case "原、辅料（窖）":
                name.Add("制曲、入曲");
                break;
            case "原、辅料（加曲）":
                name.Add("发酵");
                break;
            case "酒醅":
                name.Add("加原辅料");
                name.Add("上甑");
                name.Add("蒸馏");
                break;
            case "酒醅（加料）":
            case "蒸馏产物（初）":
                name.Add("蒸馏");
                break;
            case "蒸馏产物":
                name.Add("看花摘酒");
                name.Add("陈酿");
                break;
            case "蒸馏产物（改）":
                name.Add("陈酿");
                break;
            case "原酒":
                name.Add("勾调");
                break;
            case "酒":
                name.Add("灌装");
                break;
            case "酒（产物）":
                name.Add("鉴酒");
                break;
            default:
                Debug.LogError("产物转碎片错误");
                break;
        }
        foreach (var a in name)
        {
            BaseFragment fragment = new BaseFragment();
            fragmentDic.TryGetValue(a, out fragment);
            fragment.baseObject = baseObject;
            fragments.Add(fragment);
        }
        return fragments;
    }

    /// <summary>
    /// 鉴酒
    /// </summary>
    /// <param name="baseObject"></param>
    private void WineTasting(BaseObject baseObject)
    {
        Debug.Log("品酒会");
    }

    #endregion

    #region 天气相关
    /// <summary>
    /// 天气
    /// </summary>
    public Weather weather;

    /// <summary>
    /// 温度
    /// </summary>
    public float temperature;

    /// <summary>
    /// 湿度
    /// </summary>
    public float humidity;

    /// <summary>
    /// 天气系统更新
    /// </summary>
    private void WeatherData()
    {

    }
    #endregion

    #region 数据相关

    /// <summary>
    /// 碎片字典
    /// </summary>
    public Dictionary<string, BaseFragment> fragmentDic = new Dictionary<string, BaseFragment>();

    /// <summary>
    /// 加载数值
    /// </summary>
    private void LoadData()
    {
        TextAsset data = Resources.Load("Data/FragmentData") as TextAsset;

        string[] str = data.text.Split('\n');

        for (int i = 1; i < str.Length - 1; i++)
        {
            BaseFragment fragment = DataDecode(str[i]);
            fragmentDic.Add(fragment.name, fragment);
        }

    }

    /// <summary>
    /// 数据解码
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private BaseFragment DataDecode(string str)
    {
        string[] ss = str.Split('|');
        BaseFragment fragment = new BaseFragment();
        Element element = new Element(ElementKind.Fragment);
        element.acid = int.Parse(ss[1]);
        element.ester = int.Parse(ss[2]);
        element.alcohol = int.Parse(ss[3]);
        element.microbe = int.Parse(ss[4]);
        element.yield = int.Parse(ss[5]);
        element.taste = int.Parse(ss[6]);
        element.advancedAcid = int.Parse(ss[7]);
        element.advancedEster = int.Parse(ss[8]);
        element.advancedAlcohol = int.Parse(ss[9]);
        element.unnamed = int.Parse(ss[10]);
        fragment.name = ss[0];
        fragment.element = element;
        fragment.description = ss[11];
        switch (ss[12])
        {
            case "30":
                fragment.model = FragmentModel.thirty;
                break;
            case "60":
                fragment.model = FragmentModel.sixty;
                break;
            case "90":
                fragment.model = FragmentModel.ninety;
                break;
            case "120":
                fragment.model = FragmentModel.oneHundredAndTwenty;
                break;
        }
        return fragment;
    }
    #endregion
}
public enum Weather
{

}
