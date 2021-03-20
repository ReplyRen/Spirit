using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    public Dictionary<int, GuideInfo> guideInfoDict = new Dictionary<int, GuideInfo>();
    private GameObject[] guides = GameObject.FindGameObjectsWithTag("Guide");
    public GameObject guideCanvas=GameObject.Find("GuideCanvas");
    public GameObject mask;
    public GameObject rectMask;
    public GameObject circleMask;
    public GameObject dialogImage;
    public GameObject dialog;
    public GameObject character;
    public GameObject character1;
    public GameObject character2;
    public GameObject mainCharacter;
    public void Start()
    {
        LoadData();
        disfObject();
    }
    #region 数据处理
    /// <summary>
    /// 加载数值
    /// </summary>
    private void LoadData()
    {
        TextAsset data = Resources.Load("Data/FragmentData") as TextAsset;

        string[] str = data.text.Split('\n');

        for (int i = 1; i < str.Length - 1; i++)
        {
            GuideInfo guideInfo = DataDecode(str[i]);
            guideInfoDict.Add(guideInfo.guideID, guideInfo);
        }

    }

    /// <summary>
    /// 数据解码
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private GuideInfo DataDecode(string str)
    {
        string[] ss = str.Split('|');
        GuideInfo guideInfo = new GuideInfo();

        guideInfo.guideID = int.Parse(ss[0]);
        guideInfo.dialogNeed = int.Parse(ss[1]);
        guideInfo.dialogText = ss[2];
        //guideInfo.dialogRect = null;

        guideInfo.dialogCharacter = int.Parse(ss[4]);
        guideInfo.mainCharacter = int.Parse(ss[5]);

        guideInfo.maskType = int.Parse(ss[6]);
        guideInfo.itemName = ss[7];
        if(guideInfo.maskType!=0)
            foreach(var guideItem in guides)
            {
                if(guideItem.name==ss[7])
                {
                    if (guideInfo.maskType == 1)
                        guideInfo.rectMask = guideItem.GetComponent<Rect>();
                    else
                        guideInfo.circleMask = guideItem.GetComponent<Rect>();
                }
            }
        guideInfo.NextID = int.Parse(ss[8]);


        
        return guideInfo;
    }
    #endregion
    #region 操作
    private void disfObject()
    {
        mask = guideCanvas.transform.GetChild(0).gameObject;
        rectMask = mask.transform.GetChild(0).gameObject;
        circleMask = mask.transform.GetChild(1).gameObject;

        dialogImage = guideCanvas.transform.GetChild(1).gameObject;
        dialog = dialogImage.transform.GetChild(0).gameObject;

        character = guideCanvas.transform.GetChild(2).gameObject;
        character1 = character.transform.GetChild(0).gameObject;
        character2 = character.transform.GetChild(1).gameObject;
        mainCharacter = character.transform.GetChild(2).gameObject;
    }
    public void Show(int GuideID)
    {
        GuideInfo guideInfo = new GuideInfo();
        guideInfoDict.TryGetValue(GuideID, out guideInfo);
        if (guideInfo.maskType == 0)
            mask.SetActive(false);
        else if (guideInfo.maskType == 1)
        {
            mask.SetActive(true);
            rectMask.GetComponent<RectGuidanceController>().SetTarget(rectMask.GetComponent<RectTransform>());
        }   
        else if (guideInfo.maskType == 2)
        {
            mask.SetActive(true);
            circleMask.GetComponent<CircleGuidanceController>().SetTarget(circleMask.GetComponent<RectTransform>());
        }
        dialog.GetComponent<Text>().text = guideInfo.dialogText;
        if (guideInfo.dialogNeed == 0)
            dialogImage.SetActive(false);
        else
            dialogImage.SetActive(true);
        switch(guideInfo.dialogCharacter)
        {
            case 0:
                character1.SetActive(false);
                character2.SetActive(false);
                break;
            case 1:
                character1.SetActive(true);
                character2.SetActive(false);
                break;
            case 2:
                character1.SetActive(false);
                character2.SetActive(true);
                break;
        }
        if (guideInfo.mainCharacter == 0)
            mainCharacter.SetActive(false);
        else
            mainCharacter.SetActive(true);
    }

    #endregion
}
