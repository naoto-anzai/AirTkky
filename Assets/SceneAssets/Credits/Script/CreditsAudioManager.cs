using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAudioManager : MonoBehaviour
{
    [SerializeField] AudioClip click;

    AudioSource audiosource;

    public static CreditsAudioManager Instance 
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void PlayWhenClick()
    {
        audiosource.PlayOneShot(click);
    } 
}
