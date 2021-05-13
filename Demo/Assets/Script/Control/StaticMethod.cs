using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using Excel;

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
    public static List<List<string>> LoadExcel(string fileName)
    {
        FileStream fileStream = File.Open(Application.dataPath + "/Resources/Data/"+ fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
        DataSet result = excelDataReader.AsDataSet();

        if (result == null)
            Debug.LogError("读取Excel错误");

        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;

        List<List<string>> res = new List<List<string>>();
        for (int i = 0; i < rows; i++)
        {
            List<string> list = new List<string>();
            for (int j = 0; j < columns; j++)
            {
                // 获取表格中指定行指定列的数据
                string value = result.Tables[0].Rows[i][j].ToString();
                list.Add(value);
            }
            res.Add(list);
        }
        return res;
    }
}
