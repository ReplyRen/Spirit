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

    private Calendar calendar;

    private void Awake()
    {
        roundPanel = GameObject.FindWithTag("RoundPanel").GetComponent<RoundPanel>();
        uiManager = GameObject.FindWithTag("FactoryPanel").GetComponent<UIManager>();
        calendar = GameObject.FindWithTag("Calendar").GetComponent<Calendar>();
        LoadData();

    }
    private void Start()
    {
        InitGame();
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
        roundPanel.InitialRoundPanel(fragmentList);
        AudioManager.Init();
    }
    /// <summary>
    /// 加载游戏
    /// </summary>
    private void LoadGame()
    {

    }

    IEnumerator WaitFor1()
    {
        yield return new WaitForSeconds(1.2f);
        foreach (var fragment in roundPanel.HideRoundPanel())
            fragmentOnDisc.Add(fragment);
        uiManager.gameObject.SetActive(true);
        uiManager.InitializeFactory(fragmentOnDisc);
    }

    IEnumerator WaitFor2()
    {
        yield return new WaitForSeconds(1.2f);
        EndRound();
        uiManager.NextDay();
        StartRound();
        roundPanel.InitialRoundPanel(fragmentList);
    }

    public void NextRoundClick()
    {
        /*EndRound();
        uiManager.NextDay();
        StartRound();
        roundPanel.InitialRoundPanel(fragmentList);*/
        StartCoroutine(WaitFor2());
    }

    public void SwitchClick()
    {
        /*foreach (var fragment in roundPanel.HideRoundPanel())
            fragmentOnDisc.Add(fragment);
        uiManager.gameObject.SetActive(true);
        uiManager.InitializeFactory(fragmentOnDisc);*/
        StartCoroutine(WaitFor1());
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
        for (int i = fragmentOnDisc.Count - 1; i >= 0; i--) //遍历圆盘上的碎片
        {
            fragmentOnDisc[i].DurationDecrease();//时间自减
            if (fragmentOnDisc[i].finished)//如果完成了
            {
                if (fragmentOnDisc[i].name == "鉴酒")
                {

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
        if (fragment.name == "原、辅料准备")
        {
            baseObject.element = fragment.element;
            baseObject.evaluation = fragment.evaluation;
            baseObject.mains = fragment.mains;
            baseObject.minors = fragment.minors;
        }
        else
        {
            baseObject.element = fragment.element + fragment.baseObject.element;
            baseObject.evaluation = fragment.evaluation + fragment.baseObject.evaluation;
            baseObject.alcoholQueue = fragment.baseObject.alcoholQueue;
            baseObject.mains = fragment.baseObject.mains;
            baseObject.minors = fragment.baseObject.minors;
            if (baseObject.alcoholQueue.Count >= 6)
            {
                baseObject.alcoholQueue.Dequeue();
            }
            baseObject.alcoholQueue.Enqueue(baseObject.element.alcohol);
        }

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
            case "勾兑勾调":
                baseObject.name = "酒";
                break;
            case "灌装":
                baseObject.name = "酒（产物）";
                break;
            case "鉴酒":
                break;
            default:
                Debug.LogError("碎片转物品错误");
                break;
        }
        return baseObject;
    }
    private BaseFragment SetFragment(BaseFragment fragment)
    {
        BaseFragment baseFragment = new BaseFragment();
        baseFragment.name = fragment.name;
        baseFragment.element = fragment.element;
        baseFragment.evaluation = fragment.evaluation;
        baseFragment.description = fragment.description;
        baseFragment.model = fragment.model;
        baseFragment.facility = fragment.facility;
        return baseFragment;
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
            foreach (var f in fragments)
            {
                fragmentList.Add(f);//添加碎片队列
            }
        }
        BaseFragment fragment = SetFragment(fragmentDic["原、辅料准备"]);
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
                name.Add("勾兑勾调");
                break;
            case "酒":
                name.Add("灌装");
                break;
            case "酒（产物）":
                name.Add("鉴酒");
                break;
            default:
                Debug.LogError("产物转碎片错误 :" + baseObject.name);
                break;
        }
        foreach (var a in name)
        {
            BaseFragment fragment = SetFragment(fragmentDic[a]);
            fragment.baseObject = baseObject;
            fragments.Add(fragment);
        }
        return fragments;
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
    /// 月份
    /// </summary>
    public Month month = Month.建卯;

    /// <summary>
    /// 季节
    /// </summary>
    public Season season;

    /// <summary>
    /// 湿度
    /// </summary>
    public float humidity;

    /// <summary>
    /// 天气系统更新
    /// </summary>
    private void WeatherData()
    {
        MonthPast();
        SetSeason();
        float temp = SetHumidity();
        SetTemperature(temp);
        calendar.Set(season, weather, temperature, humidity);
    }

    private void MonthPast()
    {
        if (month == Month.建子)
            month = Month.建丑;
        else
            month++;
    }
    private void SetSeason()
    {
        int num = roundCount % 12;
        if (0 < num && num <= 3)
        {
            season = Season.春;
        }
        else if (num > 3 && num <= 6)
        {
            season = Season.夏;
        }
        else if (num > 6 && num <= 9)
        {
            season = Season.秋;
        }
        else if ((num > 9 && num <= 11)||num==0)
        {
            season = Season.冬;
        }
    }
    private void SetTemperature(float htp)
    {
        float normal = 0;
        switch (month)
        {
            case Month.建丑:
                normal = 5f;
                break;
            case Month.建寅:
                normal = 7f;
                break;
            case Month.建卯:
                normal = 12f;
                break;
            case Month.建辰:
                normal = 17f;
                break;
            case Month.建巳:
                normal = 20f;
                break;
            case Month.建午:
                normal = 23f;
                break;
            case Month.建未:
                normal = 26f;
                break;
            case Month.建申:
                normal = 22f;
                break;
            case Month.建酉:
                normal = 19f;
                break;
            case Month.建戌:
                normal = 15f;
                break;
            case Month.建亥:
                normal = 10f;
                break;
            case Month.建子:
                normal = 5f;
                break;
        }
        float temp = Random.Range(-3, 5);
        temperature = normal + temp - htp / 3;
    }
    private float SetHumidity()
    {
        float normal = 0f;
        switch (month)
        {
            case Month.建丑:
                normal = 79f;
                break;
            case Month.建寅:
                normal = 78f;
                break;
            case Month.建卯:
                normal = 77f;
                break;
            case Month.建辰:
                normal = 77f;
                break;
            case Month.建巳:
                normal = 76f;
                break;
            case Month.建午:
                normal = 80f;
                break;
            case Month.建未:
                normal = 72f;
                break;
            case Month.建申:
                normal = 70f;
                break;
            case Month.建酉:
                normal = 75f;
                break;
            case Month.建戌:
                normal = 81f;
                break;
            case Month.建亥:
                normal = 82f;
                break;
            case Month.建子:
                normal = 79f;
                break;
        }
        float temp = Random.Range(-15f, 15f);
        if (temp > 6)
            weather = Weather.雨;
        else if (temp > 0 && temp <= 6)
            weather = Weather.阴;
        else
            weather = Weather.晴;
        humidity = normal + temp;
        return temp;
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
        Element element = new Element();
        element.acid = float.Parse(ss[1]);
        element.ester = float.Parse(ss[2]);
        element.alcohol = float.Parse(ss[3]);
        element.microbe = float.Parse(ss[4]);
        element.yield = float.Parse(ss[5]);
        element.taste = float.Parse(ss[6]);
        element.advancedAcid = float.Parse(ss[7]);
        element.advancedEster = float.Parse(ss[8]);
        element.advancedAlcohol = float.Parse(ss[9]);
        element.unnamed = float.Parse(ss[10]);

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
        fragment.facility = ss[18];          

        Evaluation evaluation = new Evaluation();
        evaluation.intensity = float.Parse(ss[13]);
        evaluation.rich = float.Parse(ss[14]);
        evaluation.continuity = float.Parse(ss[15]);
        evaluation.fineness = float.Parse(ss[16]);
        evaluation.flavor = float.Parse(ss[17]);

        fragment.evaluation = evaluation;
        return fragment;
    }
    #endregion
}
public enum Weather
{
    晴, 阴, 雨
}
public enum Month
{
    建丑, 建寅, 建卯, 建辰, 建巳, 建午, 建未, 建申, 建酉, 建戌, 建亥, 建子
}

public enum Season
{
    春, 夏, 秋, 冬
}
