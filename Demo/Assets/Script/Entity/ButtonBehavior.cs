using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public Camera camera;
    public GameObject button;
    public GameObject userinterface;
    GameObject temp;
    public Outline outline;
    public Text text1, text2,text3;
    public static bool isOn;
    float width, height;
    float posx, posy;
    public void openInterface()
    {
        if (!isOn)
        {
            //userinterface=GameObject.Find("")
            userinterface.SetActive(true);
            isOn = true;
        }
    }
    public void closeInterface()
    {
        if (isOn)
        {
            //userinterface=GameObject.Find("")
            userinterface.SetActive(false);
            isOn = false;
        }
    }
    private void Start()
    {
        temp = GameObject.Find("Main Camera");
        camera = temp.GetComponent<Camera>();
        temp = null;
        width = button.GetComponent<RectTransform>().rect.width;
        height = button.GetComponent<RectTransform>().rect.height;
        posx = camera.WorldToScreenPoint(button.transform.position).x;
        posy = camera.WorldToScreenPoint(button.transform.position).y;
        outline = button.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            if (Input.mousePosition.x < posx + width / 2 && Input.mousePosition.x > posx - width / 2 && Input.mousePosition.y < posy + height / 2 && Input.mousePosition.y > posy - height / 2)
                outline.enabled = true;
            else outline.enabled = false;
            float a = posx + width / 2;
            text1.text = Input.mousePosition.ToString();
            text2.text = width.ToString();
            text3.text = camera.WorldToScreenPoint(button.transform.position).ToString();
            Debug.Log(Input.mousePosition + "  " + isOn);
        }
        else if (Input.GetKey(KeyCode.Escape))
            closeInterface();
        Debug.Log(isOn);
    }
}
