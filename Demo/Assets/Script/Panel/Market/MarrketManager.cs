using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarrketManager : MonoBehaviour
{
    [SerializeField]
    List<WineProgress> wineProgresses = new List<WineProgress>();
    [SerializeField]
    List<GameObject> bursts = new List<GameObject>();
    [SerializeField]
    List<Follow> trails = new List<Follow>();
    [SerializeField]
    Bazzar bazzar;
    [SerializeField]
    GameObject stop;
    bool isa = false;
    public List<float> scores = new List<float>();

    public void StartComptition()
    {
        for(int i=0;i<bazzar.wineScore.Count;i++)
        {
            float tmp = 0;
            for(int j=0;j<bazzar.wineScore[i].Count;j++)
            {
                for(int k=0;k<bazzar.wineScore[i][j].Count;k++)
                {
                    tmp += bazzar.wineScore[i][j][k].score;
                    Debug.Log("wineScore" + "[" + i + "]" + "[" + j + "]" + "[" + k + "]" + bazzar.wineScore[i][j][k].score);
                }
            }
            tmp /= 5;
            scores.Add(tmp);
        }

        for(int i=0;i<scores.Count;i++)
        {
            Debug.Log("scores" + i + ":" + scores[i]);
            wineProgresses[i].AddScore(scores[i]);
            isa = true;
        }
        scores.Clear();
    }
    public void Set()
    {
        for(int i=0;i<wineProgresses.Count;i++)
        {
            wineProgresses[i].SetStatus();
        }
    }
    public void Re()
    {
        for(int i=0;i<wineProgresses.Count;i++)
        {
            wineProgresses[i].Close();
        }
        t = 0.15f;
        t2 = 2;
        isa = false;
        scores.Clear();
        stop.SetActive(false);
        this.gameObject.SetActive(false);
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(0.3f);
    }

    void Start()
    {
        
    }
    float t = 0.5f;
    float t2 = 2;
    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        if(isa&&t<=0)
        {
            t = 0.15f;
            for(int i=0;i<4;i++)
            {
                bursts[i].SetActive ( true);
                trails[i].PlayeEffect();
            }

        }
        if(wineProgresses[0].isOver)
        {
            t2 -= Time.deltaTime;
            if (t2 <= 0)
                stop.SetActive(true);
        }
    }
}
