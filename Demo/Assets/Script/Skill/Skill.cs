using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    /// <summary>
    /// 名称
    /// </summary>
    public string name;

    /// <summary>
    /// 描述
    /// </summary>
    public string description;

    /// <summary>
    /// 稀有度
    /// </summary>
    public int rare;

    /// <summary>
    /// 图片
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// 功能
    /// </summary>
    /// <param name="obj"></param>
    public ISkillFunction func;

    public Skill(string name,string description,int rare,ISkillFunction func, string spritePath = "")
    {
        this.name = name;
        this.description = description;
        this.rare = rare;
        this.func = func;
        if (spritePath != "")
            sprite = StaticMethod.LoadSprite(spritePath);
    }


}
