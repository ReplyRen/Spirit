using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticsFix : MonoBehaviour
{
    List<BaseFragment> fragmentsOnDisc = new List<BaseFragment>();
    public List<BaseObject> baseObj = new List<BaseObject>();
    public int b1;
    public int b2;
    public int b3;
    public int b4;
    public void InitStart(int i)//goumaibufenbatch
    {
        BaseObject temp = new BaseObject();
        temp.element = new Element();
        temp.batch = i;
        baseObj.Add(temp);
        Check();
    }
    public void Delete(int i)
    {
        baseObj.Remove(baseObj[i]);
        for(int j=i;j<baseObj.Count;j++)
        {
            baseObj[j].batch--;
        }
    }
    void Check()
    {
        for(int i=0;i<baseObj.Count;i++)
        {
            if (baseObj[i].batch == 1)
                b1 = i;
            if (baseObj[i].batch == 2)
                b2 = i;
            if (baseObj[i].batch == 3)
                b3 = i;
            if (baseObj[i].batch == 4)
                b4 = i;
        }
    }
    public void AddElement(List<Inclusion> inclusions,int index)
    {
        baseObj[index].element.acid = inclusions[0].value;
        baseObj[index].element.ester = inclusions[1].value;
        baseObj[index].element.alcohol = inclusions[2].value;
        baseObj[index].element.microbe = inclusions[3].value;
        baseObj[index].element.yield = inclusions[4].value;
        baseObj[index].element.taste = inclusions[5].value;
        baseObj[index].element.advancedAcid = inclusions[6].value;
        baseObj[index].element.advancedEster = inclusions[7].value;
        baseObj[index].element.advancedAlcohol = inclusions[8].value;
    }
    void Start()
    {
        fragmentsOnDisc = GameObject.Find("Main Camera").GetComponent<GameManager>().fragmentOnDisc;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
