using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritController : MonoBehaviour
{
    public Dictionary<string,Process> spiritBlueprint;

    public void EndRound(List<BaseObject> objs)
    {
        foreach(var obj in objs)
        {
            if(obj.process==null)
            {
                SetProcess(obj);
            }
            else
            {
                obj.AddAttributes();
                obj.process.count--;
                obj.process = obj.process.targetProcess;
            }
        }
    }
    /// <summary>
    /// 设定酒的流程蓝图
    /// </summary>
    /// <param name="obj"></param>
    public void SetProcess(BaseObject obj)
    {
        obj.process = spiritBlueprint[obj.GetKind().ToString()].Clone() as Process;
    }
    public void StartRound(List<BaseObject> objs)
    {
    }

}
