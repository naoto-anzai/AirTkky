using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSEManager : MonoBehaviour
{
    [SerializeField] AudioClip click;

    AudioSource audiosource;

    public static ButtonSEManager Instance
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

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void PlayWhenClick()
    {
        audiosource.PlayOneShot(click);
    }
}
