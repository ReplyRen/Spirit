using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCheck : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        this.InvokeRepeating("Scan", 0, 0.5f);
    }
    //定时器函数
    void Scan()
    {
        AudioManager.DisableOverAudio();
    }
}
