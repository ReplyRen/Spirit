using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 碎片队列
    /// </summary>
    public List<BaseFragment> fragmentList = new List<BaseFragment>();

    /// <summary>
    /// 仓库物品队列
    /// </summary>
    public List<BaseObject> baseList = new List<BaseObject>();

    public Dictionary<string, BaseFragment> valuePairs = new Dictionary<string, BaseFragment>();
    private void Start()
    {
        
    }
}
