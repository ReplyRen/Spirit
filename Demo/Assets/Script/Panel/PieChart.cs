using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    public List<Color32> colors = new List<Color32>();
    public GameObject filling;
    List<GameObject> fillings=new List<GameObject>();
    public GameObject legend;
    List<GameObject> lengeds = new List<GameObject>();
    private void Start()
    {
        legend.SetActive(false);
        fillings.Add(filling);
        InitFilling();
        Test();
    }
    private void Update()
    {

    }
    public void Test() 
    {
        Inclusion a = new Inclusion("醇含量", 0.5f);
        Inclusion b = new Inclusion("2", 0.5f);
        Inclusion c = new Inclusion("3", 0.5f);
        Init(1.5f, a, b, c);
    }
    public void Init(Transform parent,Vector3 localPos,float total,params Inclusion[] inclusions)
    {
        transform.SetParent(parent);
        transform.localPosition = localPos;
        SetCount(total, inclusions);
    }
    public void Init(float total, params Inclusion[] inclusions)
    {
        SetCount(total, inclusions);
    }
    public void UpdateChart(float total, params Inclusion[] inclusions)
    {
        Clear();
        SetCount(total, inclusions);
    }
    private void SetCount(float total, params Inclusion[] inclusions)
    {
        List<int> list = new List<int>();
        int add = 0;
        foreach (var a in inclusions)
        {
            add += (int)(a.value / total * 24 + 0.5f);
            list.Add(add);
        }
        for (int i = 0; i < 24; i++)
        {
            for(int j = 0; j < list.Count; j++)
            {
                if (i < list[j])
                {
                    fillings[i].GetComponent<Image>().color = colors[j];
                    break;
                }
                else if (i == list[j])
                {
                    GameObject obj = Instantiate(legend, gameObject.transform);
                    obj.SetActive(true);
                    obj.transform.localPosition = new Vector3(-180 + 150 * j, -200);
                    obj.GetComponent<Image>().color = colors[j];
                    obj.GetComponentInChildren<Text>().text = inclusions[j].name;
                    lengeds.Add(obj);
                }
            }
        }
        GameObject obj1 = Instantiate(legend, gameObject.transform);
        obj1.SetActive(true);
        obj1.transform.localPosition = new Vector3(-180 + 150 * (list.Count-1), -200);
        obj1.GetComponent<Image>().color = colors[list.Count - 1];
        obj1.GetComponentInChildren<Text>().text = inclusions[list.Count - 1].name;
        lengeds.Add(obj1);
    }
    private void InitFilling()
    {
        for(int i = 1; i < 24; i++)
        {
            GameObject obj = Instantiate(filling,gameObject.transform);
            obj.transform.localPosition = new Vector2(85f * Mathf.Sin(Mathf.PI * i / 12), 85f * Mathf.Cos(Mathf.PI * i / 12)+3);
            obj.transform.localEulerAngles = new Vector3(0, 0, -15 * i);
            fillings.Add(obj);
        }
    }
    private void Clear()
    {
        foreach(var a in lengeds)
        {
            Destroy(a);
        }
        lengeds.Clear();
    }
}
