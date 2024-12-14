using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip clickBtn, backquitendBtn;

    public void ClickBtn()
    {
        sfxSource.clip = clickBtn;
        sfxSource.Play();
    }

    public void BackQuitEndBtn()
    {
        sfxSource.clip = backquitendBtn;
        sfxSource.Play();
    }
}
