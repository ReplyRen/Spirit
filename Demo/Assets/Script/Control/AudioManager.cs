using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static GameObject soundRoot;    //根源

    static Dictionary<string, AudioSource> bkMusics = null; //背景音乐
    static string bkMusicPlaying = null;
    static Dictionary<string, AudioSource> effectMusics = null;//音效

    static bool bkMusicMute = false;
    static bool effectMusicsMute = false;

    public static void Init()
    {
        soundRoot = new GameObject("SoundRoot");
        soundRoot.AddComponent<AudioCheck>();
        GameObject.DontDestroyOnLoad(soundRoot);

        bkMusics = new Dictionary<string, AudioSource>();
        effectMusics = new Dictionary<string, AudioSource>();
        PlayBKMusic("BK1");
    }
    /// <summary>
    /// 背景音乐设置
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isLoop"></param>
    public static void PlayBKMusic(string name,bool isLoop=true)
    {
        AudioSource audioSource = null;
        if(bkMusics.ContainsKey(name))
        {
            audioSource = bkMusics[name];
        }
        else
        {
            GameObject sound = new GameObject(name);
            sound.transform.parent = soundRoot.transform;

            audioSource = sound.AddComponent<AudioSource>();
            AudioClip audioClip = Resources.Load<AudioClip>("Sound/" + name);
            audioSource.clip = audioClip;
            audioSource.loop = isLoop;
            audioSource.playOnAwake = true;
            audioSource.spatialBlend = 0.0f;

            bkMusics.Add(name, audioSource);
        }
        audioSource.mute = bkMusicMute;
        audioSource.enabled = true;
        audioSource.Play();
        if (bkMusicPlaying != null && bkMusicPlaying != name)
        {
            bkMusics[bkMusicPlaying].Stop();
            bkMusicPlaying = name;
        }
        else
            bkMusicPlaying = name;
    }
    public static void StopBKMusic(string name)
    {
        AudioSource audioSource = null;
        if(!bkMusics.ContainsKey(name))
        {
            return;
        }
        audioSource = bkMusics[name];
        bkMusicPlaying = null;
        audioSource.Stop();
    }
    public static void StopAllBKMusics()
    {
        foreach (AudioSource source in bkMusics.Values)
            source.Stop();
    }
    public static void ClearBKMusics(string name)
    {
        AudioSource audioSource = null;
        if(!bkMusics.ContainsKey(name))
        {
            return;
        }
        audioSource = bkMusics[name];
        bkMusics[name] = null;
        GameObject.Destroy(audioSource.gameObject);
    }
    public static void SwitchBKMusic()
    {
        bkMusicMute = !bkMusicMute;
        bkMusics[bkMusicPlaying].mute = bkMusicMute;
    }
    public static bool BKMusicStatus()
    {
        return bkMusicMute;
    }
    /// <summary>
    /// 音效设置
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isLoop"></param>
    public static void PlayEffectMusic(string name,bool isLoop=false)
    {
        AudioSource audioSource = null;
        if (effectMusics.ContainsKey(name))
            audioSource = effectMusics[name];
        else
        {
            GameObject sound = new GameObject(name);
            sound.transform.parent = soundRoot.transform;

            audioSource = sound.AddComponent<AudioSource>();
            AudioClip clip = Resources.Load<AudioClip>("Sound/" + name);
            audioSource.clip = clip;
            audioSource.loop = isLoop;
            audioSource.playOnAwake = true;
            audioSource.spatialBlend = 0.0f;

            effectMusics.Add(name, audioSource);
        }
        audioSource.mute = effectMusicsMute;
        audioSource.enabled = true;
        audioSource.Play();
    }

    public static void StopEffectMusic(string name)
    {
        AudioSource audioSource = null;
        if(!effectMusics.ContainsKey(name))
        {
            return;
        }
        audioSource = effectMusics[name];
        audioSource.Stop();
    }        
    public static void StopAllEffect()
    {
        foreach (AudioSource source in effectMusics.Values)
            source.Stop();
    }
    public static void ClearEffectMusic(string name)
    {
        AudioSource audioSource = null;
        if(!effectMusics.ContainsKey(name))
        {
            return;
        }
        audioSource = effectMusics[name];
        effectMusics[name] = null;
        GameObject.Destroy(audioSource.gameObject);
    }
    public static void SwitchEffectMusic()
    {
        effectMusicsMute = !effectMusicsMute;
        foreach(AudioSource sound in effectMusics.Values)
        {
            sound.mute = effectMusicsMute;
        }
    }
    public static bool EffectMusicStatus()
    {
        return effectMusicsMute;
    }
    /// <summary>
    /// 隐藏未播放物体，优化场景
    /// </summary>
    public static void DisableOverAudio()
    {
        foreach(AudioSource sound in bkMusics.Values)
        {
            if (!sound.isPlaying)
                sound.enabled = false;
        }
        foreach (AudioSource sound in effectMusics.Values)
            if (!sound.isPlaying)
                sound.enabled = false;
    }
}
