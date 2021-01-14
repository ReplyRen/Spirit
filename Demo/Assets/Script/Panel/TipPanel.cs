using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Text text;

    private Button button;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(Click);

    }
    public void Click()
    {
        Destroy(gameObject);
    }
    public void Show(string str)
    {
        text.text = str;
    }
}
