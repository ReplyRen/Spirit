using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pm : MonoBehaviour
{
    int n=3;
    GameObject temp;
    public List<Image> image = new List<Image>();
    public Text text;
    void Awake()
    {
        
        for(int i=0;i<n;i++)
        {
            image[i].color = new Color(0, 0, 0);
        }
        text.text = n.ToString();
    }
    public void plus()
    {
        n++;
        if (n > 5) n = 5;
        for (int i = 0; i < n; i++)
        {
            image[i].color = new Color(0, 0, 0);
        }
        text.text = n.ToString();
    }
    public void minus()
    {
        n--;
        if (n < 0) n = 0;
        for (int i = 0; i < 5; i++)
        {
            image[i].color = new Color(255, 255, 255);
        }
        for (int i = 0; i < n; i++)
        {
            image[i].color = new Color(0, 0, 0);
        }
        text.text = n.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
