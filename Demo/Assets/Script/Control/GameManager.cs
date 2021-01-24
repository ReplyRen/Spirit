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
    /// 仓库物品队列
    /// </summary>
    public List<BaseObject> baseList = new List<BaseObject>();

    /// <summary>
    /// 碎片字典
    /// </summary>
    public Dictionary<string, BaseFragment> fragmentDic = new Dictionary<string, BaseFragment>();
    private void Start()
    {
        InitData();
        InitGame();
    }
    private void InitData()
    {
        TextAsset data = Resources.Load("Data/FragmentData") as TextAsset;

        string[] str = data.text.Split('\n');

        for (int i = 1; i < str.Length - 1; i++)
        {
            string[] ss = str[i].Split('|');
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
            fragmentDic.Add(fragment.name, fragment);
        }

    }

    /// <summary>
    /// 初始化游戏
    /// </summary>
    private void InitGame()
    {
        roundCount = 1;
        experience = 0;
        level = 1;
    }
    /// <summary>
    /// 加载游戏
    /// </summary>
    private void LoadGame()
    {

    }
    /// <summary>
    /// 下一回合
    /// </summary>
    private void NextRound()
    {
        roundCount++;
    }
}
public enum Weather
{

}
