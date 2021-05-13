using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideControl : MonoBehaviour
{
    GuideInfo guideInfo = new GuideInfo();
    public static int id;
    public bool newGamer = true;
    public GameObject fragmentLayout;
    public GameObject dialog;
    public GameObject dialogImage;
    public GameObject skipObject;
    private GameObject replace;
    
    public bool ifCan = true;
    private bool timer = true;
    // Start is called before the first frame update
    void Start()
    {
        if (newGamer)
            id = 101;
        else
        {
            id = 702;
            return;
        }
        //replace = GameObject.Find("替身107");
        Invoke("SpecialEvent", 0.01f);
        Invoke("Run", 0.1f);
        ifCan = true;
        skipObject.SetActive(true);
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
                case 205:
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
            if (id == 509)
            {
                id = 605;
                newGamer = false;
                gameObject.GetComponent<GuideManager>().Hide();
                return;
            }
            id += 1;
            
        }
        else
            gameObject.GetComponent<GuideManager>().Hide();
    }
    private void Update()
    {
        if (!timer || id == 10 || id == 229)  
            return;
        if (Input.touchCount > 0 && !ifCan)
        {
            if(dialogImage.activeSelf)
                dialog.GetComponent<StartGameCtrl>().ShowAllText();
            ifCan = true;
            timer = false;
            Invoke("swift", 0.5f);
        }
        else if (Input.GetMouseButtonDown(0) && !ifCan)
        {
            if (dialogImage.activeSelf)
                dialog.GetComponent<StartGameCtrl>().ShowAllText();
            ifCan = true;
            timer = false;
            Invoke("swift", 0.5f);
        }
        else if (Input.touchCount > 0 && ifCan)
        {
            Run();
            ifCan = false;
        }
        else if (Input.GetMouseButtonDown(0) && ifCan)
        {
            Run();
            ifCan = false;
        }
    }
    void swift()
    {
        timer = true;
    }
    public void skip()
    {
        if (id >= 101 && id <= 111)
        {
            id = 2;
            gameObject.GetComponent<GuideManager>().Hide();
        }
        else if (id >= 201 && id <= 229)
        {
            id = 11;
            gameObject.GetComponent<GuideManager>().Hide();
        }
        else if (id >= 301 && id <= 315)
        {
            id = 14;
            gameObject.GetComponent<GuideManager>().Hide();
        }
        else if (id >= 401 && id <= 415)
        {
            id = 17;
            gameObject.GetComponent<GuideManager>().Hide();
        }
        else
        {
            id = 702;
            Run();
            newGamer = false;
        }
    }
}
