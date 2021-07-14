using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour
{
    [SerializeField]
    Text time;
    [SerializeField]
    Text prefer;
    [SerializeField]
    GameObject join;
    [SerializeField]
    Bazzar bazzar;
    public bool isJoin = false;
    bool isCreate = false;
    public int dateRange;
    public List<Wine> wines = new List<Wine>();
    public List<Judge> judges = new List<Judge>();

    public void GetPrefers(List<Judge> judges1)
    {
        int[] a = { 0, 0, 0, 0, 0 };
        for (int i=0;i<judges1.Count;i++)
        {
            for(int j=0;j<judges1[i].prefers.Count;j++)
            {
                a[j] += judges1[i].prefers[j];
            }
        }
        int max = a[0];
        int[] index = { -1, -1, -1 };
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (max <= a[i])
                {
                    max = a[i];
                    index[j] = i;
                }
            }
            a[index[j]] = -1;
            max = 0;
        }
        prefer.text = null;
        for(int i=0;i<3;i++)
        {
            switch(index[i])
            {
                case 0:
                    prefer.text += "<color=red>" + "强度" + "</color>" + "\n";
                    break;
                case 1:
                    prefer.text += "<color=red>" + "浓郁度" + "</color>" + "\n";
                    break;
                case 2:
                    prefer.text += "<color=red>" + "连绵度" + "</color>" + "\n";
                    break;
                case 3:
                    prefer.text += "<color=red>" + "陈敛细腻度" + "</color>" + "\n";
                    break;
                case 4:
                    prefer.text += "<color=red>" + "风味" + "</color>" + "\n";
                    break;

            }
        }
    }
    public void Dating()
    {
        dateRange--;
        if (dateRange > 0)
        {
            time.text = "距离开市还有" + "<color=red>" + dateRange + "</color>" + "月";
            if(dateRange<=8&&!isCreate)
            {
                isCreate = true;
                bazzar.CreateMatch();
                GetPrefers(bazzar.judges);
                for (int i = 0; i < bazzar.wines.Count; i++)
                    wines.Add(bazzar.wines[i]);
                for (int i = 0; i < bazzar.judges.Count; i++)
                    judges.Add(bazzar.judges[i]);
            }
        }
        else if (dateRange > -3)
        {
            if (!isJoin && bazzar.winesP.Count != 0) join.SetActive(true);
            if (!isJoin) time.text = "集市中";
            else time.text = "已参加";
        }
        else if (dateRange > -5)
        {
            join.SetActive(false);
            isCreate = false;
            time.text = "休市中";
        }
        else
        {
            dateRange = 13;
            prefer.text = "选派评委中";
        }
    }
    void Start()
    {
        join.SetActive(false);
        time.text = "距离开市还有" + "<color=red>" + dateRange + "</color>" + "月";
        prefer.text = "选派评委中";
    }

    
    void Update()
    {
        
    }
}
