using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("AudioSource")]
    public AudioSource bgmSource;
    public List<AudioSource> effectSourceList;

    [Header("Volume")]
    [SerializeField][Range(0f, 1.0f)] private float bgmVolume = 0.1f;
    [SerializeField][Range(0f, 1.0f)] private float effectVolume = 0.3f;

    public SoundDB soundDB;

    private void Start()
    {
        soundDB = GetComponent<SoundDB>();

        Init();
    }

    public void Init()
    {
        bgmSource.volume = bgmVolume;
        ChangeMusic(soundDB.mainSceneClip);

        for (int i = 0; i < effectSourceList.Count; i++)
        {
            effectSourceList[i].clip = null;
            effectSourceList[i].loop = false;
            effectSourceList[i].volume = effectVolume;
        }
    }

    public void ChangeMusic(AudioClip clip)
    {
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlayEffect(AudioClip clip, float volume, bool isLoop)
    {
        AudioSource nullSource = effectSourceList[GetNullIndex()];

        if (isLoop == true)
            nullSource.loop = true;
        else
            nullSource.loop = false;

        nullSource.volume = volume;
        nullSource.PlayOneShot(clip);
    }

    public void StopAllSounds()
    {
        bgmSource.Stop();
        for (int i = 0; i < effectSourceList.Count; i++)
        {
            effectSourceList[i].Stop();
        }
    }

    public void StopLoopEffect()
    {
        for (int i = 0; i < effectSourceList.Count; i++)
        {
            effectSourceList[i].Stop();
            effectSourceList[i].clip = null;
        }
    }

    private int GetNullIndex()
    {
        int nullIndex = 0;

        for (int i = 0; i < effectSourceList.Count; i++)
        {
            if (effectSourceList[i].clip == null)
            {
                nullIndex = i;
                break;
            }
        }
        return nullIndex;
    }

    public void playSoundByname(string name)
    {
        switch(name)
        {
            case "안보연":
                PlayEffect(soundDB.obeyABY, 0.8f, false);
                break;
            case "권유리":
                PlayEffect(soundDB.obeyKYR, 0.8f, false);
                break;
            case "최세은":
                PlayEffect(soundDB.obeyCSE, 0.5f, false);
                break;
            case "이유신":
                PlayEffect(soundDB.obeyLYS, 0.5f, false);
                break;
            case "윤세나":
                PlayEffect(soundDB.obeyYSN, 0.5f, false);
                break;
            
        }
    }
}