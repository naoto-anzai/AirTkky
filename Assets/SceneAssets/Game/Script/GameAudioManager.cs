using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip scoreSE;
    [SerializeField] AudioClip loseScoreSE;
    [SerializeField] AudioClip clack;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBGM();
    }

    public void PlayBGM()
    {
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayWhenScored()
    {
        audioSource.PlayOneShot(scoreSE);
    }

    public void PlayWhenLost()
    {
        audioSource.PlayOneShot(loseScoreSE);
    }

    public void PlayClackSound()
    {
        audioSource.PlayOneShot(clack);
    }
}
