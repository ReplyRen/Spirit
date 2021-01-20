using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPanel : BasePanel
{
    private static GameObject roundPanel;
    // Start is called before the first frame update
    void Start()
    {
        roundPanel = GameObject.Find("RoundPanel");
    }

    // Update is called once per frame
    private static void UpdatePanel()
    {
        //FragmentsManager.UpdateFragments(GameManager.fragmentList);//更新碎片
    }
    public static void Hide()
    {
        roundPanel.SetActive(false);
    }
    public static void Active()
    {
        roundPanel.SetActive(true);
        UpdatePanel();
    }
}
