using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    VideoPlayer videoPlayer;

    RawImage rawImage;
    private void Start()
    {
        //rawImage = GetComponentInChildren<RawImage>();
        //videoPlayer = GetComponentInChildren<VideoPlayer>();
        //VideoClip videoclip = (VideoClip)Resources.Load("Cover.mp4", typeof(VideoClip));
        //videoPlayer.clip = videoclip;
        //videoPlayer.Play();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }


}
