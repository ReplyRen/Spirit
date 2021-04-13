using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryPanel : MonoBehaviour
{
    GameObject currentBtn;
    GameObject currentText;
    Button button;
    public void SwitchBtn(GameObject obj)
    {
        if (currentBtn != null&&currentBtn!=obj)
            currentBtn.transform.Find("Image").gameObject.SetActive(false);
        obj.transform.Find("Image").gameObject.SetActive(true);
        currentBtn = obj;
    }
    public void SwitchText(GameObject text)
    {
        if (currentText != null&&currentText!=text)
            currentText.SetActive(false);
        text.SetActive(true);
        currentText = text;
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        if (currentText != null)
            currentText.SetActive(false);
        if (currentBtn != null)
            currentBtn.transform.Find("Image").gameObject.SetActive(false);
        currentBtn = null;
        currentText = null;
        gameObject.SetActive(false);
    }
}
