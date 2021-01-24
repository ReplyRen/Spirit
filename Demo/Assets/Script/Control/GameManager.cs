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

    private void Start()
    {
        LoadData();
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
    }
    /// <summary>
    /// 加载游戏
    /// </summary>
    private void LoadGame()
    {

    }

    #region 流程逻辑
    /// <summary>
    /// 下一回合
    /// </summary>
    private void NextRound()
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
    private BaseObject FragmentToObject(BaseFragment fragment)
    {
        BaseObject baseObject = new BaseObject();
        baseObject.element = fragment.element + fragment.baseObject.element;

        return baseObject;
    }
    private BaseFragment ObjectToFragment(BaseObject baseObject)
    {
        BaseFragment fragment = new BaseFragment();
        string name;

        fragment.baseObject = baseObject;
        return fragment;
    }

    /// <summary>
    /// 更新仓库
    /// </summary>
    private void UpdateObject()
    {
        foreach (var a in fragmentOnDisc)//遍历圆盘上的碎片
        {
            a.DurationDecrease();//时间自减
            if (a.finished)//如果完成了
            {
                if (a.name == "鉴酒")
                {
                    WineTasting(a.baseObject);
                }
                else
                {
                    if (a.baseObject != null)//移除仓库中此碎片的酒基
                        baseList.Remove(a.baseObject);
                    baseList.Add(FragmentToObject(a));//添加新酒基
                }

                fragmentOnDisc.Remove(a);//移除圆盘上此碎片
            }
        }
    }
    /// <summary>
    /// 仓检
    /// </summary>
    private void UpdateFragment()
    {
        fragmentList.Clear();//清除碎片队列
        foreach (var a in baseList)
        {
            fragmentList.Add(ObjectToFragment(a));//添加碎片队列
        }
    }

    /// <summary>
    /// 鉴酒
    /// </summary>
    /// <param name="baseObject"></param>
    private void WineTasting(BaseObject baseObject)
    {

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
            case "单凸":
                fragment.model = FragmentModel.singleConvex;
                break;
            case "单凹":
                fragment.model = FragmentModel.singleConcave;
                break;
            case "双凸":
                fragment.model = FragmentModel.doubleConvex;
                break;
            case "半凹半凸":
                fragment.model = FragmentModel.halfConcaveAndConvex;
                break;
        }
        return fragment;
    }
    #endregion
}
public enum Weather
{

}
