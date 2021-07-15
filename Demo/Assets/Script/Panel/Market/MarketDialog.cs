using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketDialog : MonoBehaviour
{
    [SerializeField]
    Text dialog;
    [SerializeField]
    Text score;
    [SerializeField]
    Image bg;
    int a = 0;

    string[,] ss = new string[4, 4]{ {"无合乎口","糟酿无心","寡淡如水","甚烈难咽" },
                                    { "色味庸足","但止口馋","弃之可惜","尚可品鉴"},
                                    { "浓厚醇香","回味无穷","匠心之作","琼宴佳肴" },
                                    { "天上琼浆","实为冠绝","千觞难停","杯中神秀" } };

    public void RandomScore()
    {
        a = UnityEngine.Random.Range(0, 8);
        score.text = "+" + a;
        GetDialog();
    }
    void GetDialog()
    {
        switch(a)
        {
            case 0:
            case 1:
                int b = UnityEngine.Random.Range(0, 4);
                dialog.text = ss[0,b];
                break;
            case 2:
            case 3:
                int c = UnityEngine.Random.Range(0, 4);
                dialog.text = ss[1, c];
                break;
            case 4:
            case 5:
                int d = UnityEngine.Random.Range(0, 4);
                dialog.text = ss[2, d];
                break;
            case 6:
            case 7:
                int e = UnityEngine.Random.Range(0, 4);
                dialog.text = ss[3, e];
                break;
            /*case 8:
            case 9:
                int f = UnityEngine.Random.Range(0, 4);
                dialog.text = ss[0, f];
                break;*/
        }
    }
    void Start()
    {
        
    }
    float t1 = 3, t2 = 4;
    float s1, s2;
    // Update is called once per frame
    void Update()
    {
        s1 = Time.deltaTime ;
        if (score != null)
        {
            score.color = new Color(0.5283019f, 0.3555209f, 0.2267711f, score.color.a - 0.8f*Time.deltaTime);
            //score.transform.position = new Vector3(score.transform.position.x, score.transform.position.y + 270 * s1* Time.deltaTime, score.transform.position.z);
        }
        t1 -= Time.deltaTime;
        if(score!=null&&t1<=0)Destroy(score.gameObject);
        if (this.gameObject != null)
        {
            bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, bg.color.a - 0.3f*Time.deltaTime);
            dialog.color = new Color(dialog.color.r, dialog.color.g, dialog.color.b, dialog.color.a -  0.3f*Time.deltaTime);
            t2 -= Time.deltaTime;
            if (t2 <= 0) Destroy(this.gameObject);
        }
        
    }
}
