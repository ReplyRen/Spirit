using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticMethod : MonoBehaviour
{
    /// <summary>
    /// 提示窗口
    /// </summary>
    /// <param name="str"></param>
    public static void Tips(string str)
    {
        GameObject obj = Instantiate(LoadPrefab("Prefab/TipPanel"));
        obj.transform.SetParent(GameObject.FindWithTag("Canvas").transform,false);
        obj.GetComponent<TipPanel>().Show(str);
    }
    
    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="path"></param>
    public static Sprite LoadSprite(string path)
    {
        Object obj = Resources.Load(path, typeof(Sprite));
        Sprite sprite = null;
        try
        {
            sprite = Instantiate(obj) as Sprite;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.ToString());
            Debug.Log("Path: " + path);
        }

        return sprite;
    }

    /// <summary>
    /// 加载预制体
    /// </summary>
    /// <param name="path"></param>
    public static GameObject LoadPrefab(string path)
    {
        GameObject obj = (GameObject)Resources.Load(path);

        return obj;
    }
}
