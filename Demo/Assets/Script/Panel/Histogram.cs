using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Histogram : MonoBehaviour
{
    GameObject pillar;
    List<GameObject> objs = new List<GameObject>();
    private void Start()
    {
        pillar = GameObject.Find("Pillar");
        pillar.SetActive(false);
    }
    private void Test()
    {
        Inclusion a = new Inclusion("1", 1);
        Inclusion b = new Inclusion("2", 0.5f);
        Inclusion c = new Inclusion("3", 0.01f);
        Init(a, b, c);
    }
    private void Clear()
    {
        foreach (var a in objs)
        {
            Destroy(a);
        }
        objs.Clear();
    }
    public void Init(params Inclusion[] inclusions)
    {
        SetLength(inclusions);
    }
    public void Init(Transform parent,Vector3 localPos, params Inclusion[] inclusions)
    {
        gameObject.transform.SetParent(parent);
        gameObject.transform.localPosition = localPos;
        SetLength(inclusions);
    }
    private void SetLength(params Inclusion[] inclusions)
    {
        for (int i = 0; i < inclusions.Length; i++)
        {
            GameObject obj = Instantiate(pillar, transform);
            obj.SetActive(true);
            obj.transform.localPosition = new Vector2(-145 + 60 * i, -190f);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(12, 365 * inclusions[i].value);
            obj.GetComponentInChildren<Text>().text = inclusions[i].name;
            objs.Add(obj);

        }
    }
    public void UpdateLength(params Inclusion[] inclusions)
    {
        Clear();
        SetLength(inclusions);
    }
}
