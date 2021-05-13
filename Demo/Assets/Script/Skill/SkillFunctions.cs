using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillFunction
{
    void Function(BaseObject baseObject,int rare);

}
public class IncDirSkill : ISkillFunction
{
    public void Function(BaseObject baseObject,int rare)
    {
        Debug.Log("直接加分,稀有度"+rare);
    }
}
public class DecDicSkill : ISkillFunction
{
    public void Function(BaseObject baseObject, int rare)
    {
        Debug.Log("直接减少，稀有度" + rare);
    }
}
