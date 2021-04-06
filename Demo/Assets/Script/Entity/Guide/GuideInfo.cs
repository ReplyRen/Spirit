using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GuideInfo
{
    public int guideID;//引导ID

    public int dialogNeed;
    public string dialogText;
    public RectTransform dialogRect; //对话框的位置

    public int dialogCharacter;// 0-无,1-角色1,角色2
    public bool if_invisiable1;
    public Character1Face Character1f;
    public Character2Face Character2f;
    public Character3Face Character3f;
    public int mainCharacter;//主角是否在
    public CharacterMainFace CharacterMainf;
    public bool if_invisiable2;

    public int maskType;//none-0/rect-1/circle-2
    public string itemName;
    public RectTransform rectMask;
    public RectTransform circleMask;

    public int NextID;

}; 

