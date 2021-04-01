using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Guidenode : Node 
{
    [Input(backingValue = ShowBackingValue.Always)]
    public int guideID;//引导ID

    public bool dialogNeed;
    [TextArea(5,20)]
    public string dialogText;

    public int dialogCharacter;// 0-无,1-角色1,角色2
    public bool if_invisiable1; //true半隐
    public bool mainCharacter;//主角是否在
    public bool if_invisiable2;//主角

    public int maskType;//none-0/rect-1/circle-2/still-3
    public GameObject rectMask;
    public GameObject circleMask;
    [Output]
    public int NextID;
    // Use this for initialization
    protected override void Init() 
    {
		base.Init();
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
        int ID = GetInputValue<int>("guideID", this.guideID);
        return ID + 1; // Replace this
	}
}