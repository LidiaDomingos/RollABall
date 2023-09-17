using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicPlayer; // the bgm audio source and/or whatever else you need
    public static MusicController instance { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad (musicPlayer);
        instance = this;
    }
    
    
}
