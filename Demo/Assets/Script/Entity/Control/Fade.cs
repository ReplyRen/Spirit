using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Image fade;
    static public bool isFadeIn = true;
    static public bool isFadeOut = false;
    Color fadeIn = new Color(0, 0, 0, 0);
    Color fadeOut = new Color(0, 0, 0, 1);
    public float speed = 1f;
    bool isFirst = true;
    void Start()
    {
        fade = GameObject.Find("FadeCanvas").transform.Find("Fade").GetComponent<Image>();
        fade.color = fadeOut;
        FadeIn();
    }
    IEnumerator Waitfor()
    {
        yield return new WaitForSeconds(0.5f);
        FadeIn();
    }
    void FadeIn()
    {
        isFadeIn = true;
        fade.color = new Color(0, 0, 0, fade.color.a - speed * Time.deltaTime);
        if (fade.color.a <= 0.01f)
        {
            fade.raycastTarget = false;
            fade.color = fadeIn;
            isFadeIn = false;
        }
    }
    void FadeOut()
    {
        fade.raycastTarget = true;
        isFadeOut = true;
        fade.color = new Color(0, 0, 0, fade.color.a + speed * Time.deltaTime);
        if (fade.color.a >= 0.99f)
        {
            fade.color = fadeOut;
            isFadeIn = true;
            isFadeOut = false;
        }
    }
    public void FadeTo()
    {
        isFirst = false;
        isFadeIn = false;
        isFadeOut = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFadeIn && !isFadeOut && isFirst) FadeIn();
        if (isFadeIn && !isFadeOut&&!isFirst) StartCoroutine(Waitfor());
        if (isFadeOut && !isFadeIn) FadeOut();
    }
}
