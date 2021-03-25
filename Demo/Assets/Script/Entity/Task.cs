using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    //任务名
    public string name;
    //任务描述
    public string description;
    //时间限制
    public int roundCount;
    //目标分数
    public float targetScore;
    //是否完成
    public bool isFinished = false;
    //完成奖励
    public string bonus;
    //未完成惩罚
    public string punishment;
    //
    public bool isTimeLimit = false;
    //
    public bool isScoreLimit = false;
}
