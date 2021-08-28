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
    [SerializeField]
    GameObject dialog;
    [SerializeField]
    GameObject tt;
    bool isa = false;
    public List<float> scores = new List<float>();

    public void StartComptition()
    {
        /*for(int i=0;i<bazzar.wineScore.Count;i++)
        {
            float tmp = 0;
            for(int j=0;j<bazzar.wineScore[i].Count;j++)
            {
                for(int k=0;k<bazzar.wineScore[i][j].Count;k++)
                {
                    tmp += bazzar.wineScore[i][j][k].score;
                    //Debug.Log("wineScore" + "[" + i + "]" + "[" + j + "]" + "[" + k + "]" + bazzar.wineScore[i][j][k].score);
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
        scores.Clear();*/
        isa = true;
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
        t = 0.007f;
        t2 = 2;
        isa = false;
        scores.Clear();
        stop.SetActive(false);
        this.gameObject.SetActive(false);
        ii = 0;
        jj = 0;
        kk = 0;
    }

    void CreateDialog()
    {
        //Object a = Resources.Load("/Prefab/对话") as GameObject;
        GameObject d = Instantiate(tt) as GameObject;
        d.transform.SetParent(dialog.transform);
        d.transform.localScale = new Vector3(1, 1, 1);
        d.transform.SetSiblingIndex(0);
        d.GetComponent<MarketDialog>().RandomScore();
    }

    void Awake()
    {
        Re();
        for(int i=0;i<bursts.Count;i++)
        {
            bursts[i].GetComponent<ParticleSystem>().Stop();
        }
    }
    float t = 0.1f;
    float t2 = 2;
    int ii = 0, jj = 0, kk = 0;
    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        if (kk < 35 && isa && t <= 0) 
        {
            t = 0.1f;
            wineProgresses[ii].AddScore(bazzar.wineScore[ii][jj][kk].score/5);
            trails[ii].PlayeEffect(()=> {
                bursts[ii].GetComponent<ParticleSystem>().Play();
            });
            ii++;
            if (ii == 4)
            {
                ii = 0;
                jj++;
                CreateDialog();
            }
            if(jj==5)
            {
                jj = 0;
                kk++;

            }
            if (kk == 34)
                stop.SetActive(true);
        }
    }
}
