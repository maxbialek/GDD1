using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource loopMusic;

    void Start()
    {
        loopMusic = GetComponent<AudioSource>();
        loopMusic.volume *= 0.4f;
        loopMusic.Play();
    }
}
