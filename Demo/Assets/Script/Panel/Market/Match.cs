using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    public int dateRange;
    int[] a = { 0, 0, 0, 0, 0 };
    public void GetPrefers(List<Judge> judges)
    {
        for(int i=0;i<judges.Count;i++)
        {
            for(int j=0;j<judges[i].prefers.Count;j++)
            {
                a[j] += judges[i].prefers[j];
            }
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
