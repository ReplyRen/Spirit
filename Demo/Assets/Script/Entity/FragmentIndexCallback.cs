using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragmentIndexCallback : MonoBehaviour
{
    public Image image;
    public Text text;
    //这里可以在加描述，图片之类的
    void ScrollCellIndex(int idx)
    {
        text.text = FragmentsManager.GetFragment(idx).name;
        //这里可以在加描述，图片之类的，然后之后调整在正确位置
    }
}
