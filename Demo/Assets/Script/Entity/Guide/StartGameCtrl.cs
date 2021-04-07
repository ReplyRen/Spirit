using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartGameCtrl : MonoBehaviour
{
    //GameObject btn;
    /// <summary>
    /// 要显示文本的组件
    /// </summary>
    public Text text;
    public GameObject Camera;
    //要显示的所有字符串
    string str;
    //每次显示的字符串
    string s = "";

    void Start()
    {
        text = gameObject.GetComponent<Text>();   
        //PlayText();
    }
    /// <summary>
    /// 调用协成实现一个字一个字冒出的效果
    /// </summary>
    public void PlayText(string str1)
    {
        str = str1;
        //Debug.Log(str.Length);
        StopAllCoroutines();
        StartCoroutine(ShowText(str.Length));
    }
    IEnumerator ShowText(int strLength)
    {
        s = "";
        int i = 0;
        while (i < strLength)
        {
            yield return new WaitForSeconds(0.05f);
            s += str[i].ToString();
            text.text = s;
            i += 1;
        }
        //显示完成，停止所有协成
        StopAllCoroutines();
        Camera.GetComponent<GuideControl>().ifCan = true;
    }
    /// <summary>
    /// 显示所有字符串，暂停协成
    /// </summary>
    /// <param name="go"></param>
    public void ShowAllText()
    {
        if (s == str)
        {
            //写显示完之后的逻辑
        }
        else
        {
            //停止所有协成，直接显示所有字符串
            StopAllCoroutines();
            //没显示完，点击之后就显示完了
        }
        s = str;
        text.text = s;
        Camera.GetComponent<GuideControl>().ifCan = true;
    }
}