using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GuideInfo
{
    public int guideID;//引导ID

    public int dialogNeed;
    public string dialogText;
    public Rect dialogRect; //对话框的位置

    public int dialogCharacter;// 0-无,1-角色1,角色2
    public int mainCharacter;//主角是否在

    public int maskType;//none-0/rect-1/circle-2
    public string itemName;
    public Rect rectMask;
    public Rect circleMask;

    public int NextID;

}; 

