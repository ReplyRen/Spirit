using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillManager : MonoBehaviour
{
    private void Start()
    {
        //Skill skill = new Skill("1", "1", 1, new DecDicSkill());
        //skill.func.Function(new BaseObject(),10);

        //skill.func = new IncDirSkill();
        //skill.func.Function(new BaseObject(), 5);
        var a =StaticMethod.LoadExcel("test.xlsx");
        foreach(var b in a)
        {
            foreach(var c in b)
            {
                Debug.Log(c);
            }
        }

    }
    public List<Skill> RandomSkill(int count)
    {
        List<Skill> res = new List<Skill>();




        return res;
    }
}
