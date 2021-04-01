using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideControl : MonoBehaviour
{
    GuideInfo guideInfo = new GuideInfo();
    public static int id;
    public bool newGamer = true;
    public GameObject fragmentLayout;
    private GameObject replace;
    private bool ifCan = true;
    // Start is called before the first frame update
    void Start()
    {
        if (newGamer)
            id = 101;
        else
        {
            id = 1001;
            return;
        }
        //replace = GameObject.Find("替身107");
        Invoke("SpecialEvent", 0.01f);
        Invoke("Run", 0.02f);
        ifCan = true;
    }
    public void Run()
    {
        Debug.Log(id);
        if ((id <= 18 && id >= 2 && id != 13 && id != 14 && id != 17) || id == 20)
            return;
        if(gameObject.GetComponent<GuideManager>().guideInfoDict.ContainsKey(id))
        {
            gameObject.GetComponent<GuideManager>().Show(id);
            switch (id)
            {
                case 103:
                    gameObject.GetComponent<GuideManager>().guideInfoDict[107].circleMask = fragmentLayout.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
                    break;
                case 107:
                    id = 0;
                    return;
                case 109:
                    id = 1;
                    return;
                case 111:
                    id = 2;
                    return;
                case 202:
                    id = 3;
                    return;
                case 204:
                    gameObject.GetComponent<ShakeCamera>().enabled = true;
                    break;
                case 220:
                    id = 4;
                    return;
                case 222:
                    id = 5;
                    return;
                case 223:
                    id = 6;
                    return;
                case 224:
                    id = 7;
                    return;
                case 225:
                    id = 8;
                    return;
                case 226:
                    id = 9;
                    return;
                case 227:
                    id = 10;
                    return;
                case 229:
                    id = 11;
                    return;
                case 304:
                    gameObject.GetComponent<ShakeCamera>().enabled = true;
                    break;
                case 307:
                    id = 20;
                    return;
                case 309:
                    id = 12;
                    return;
                case 314:
                    id = 13;
                    return;
                case 315:
                    id = 14;
                    return;
                case 410:
                    id = 15;
                    return;
                case 413:
                    id = 16;
                    return;
                case 415:
                    id = 17;
                    return;
                case 507:
                    id = 18;
                    return;
                case 602:
                    id = 19;
                    return;
            }
            id += 1;
            if (id == 510) newGamer = false;
        }
        else
            gameObject.GetComponent<GuideManager>().Hide();
    }
    private void Update()
    {
        if (Input.touchCount > 0 && ifCan)
        {
            Run();
            ifCan = false;
            Invoke("swift", 0.1f);
        }
        else if (Input.GetMouseButtonDown(0) && ifCan)
        {
            Run();
            ifCan = false;
            Invoke("swift", 0.1f);
        }
    }
    void swift()
    {
        ifCan = true;
    }
}
