using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource scoreSESource;
    [SerializeField] AudioSource loseScoreSESource;

    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip scoreSE;
    [SerializeField] AudioClip loseScoreSE;

    void Start()
    {
        Debug.Log(gameObject.name);
        PlayBGM();
    }

    public void PlayBGM()
    {
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayWhenScored()
    {
        scoreSESource.PlayOneShot(scoreSE);
    }

    public void PlayWhenLost()
    {
        loseScoreSESource.PlayOneShot(loseScoreSE);
    }
}
