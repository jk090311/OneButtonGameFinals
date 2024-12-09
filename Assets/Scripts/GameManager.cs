using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer from the Title Panel
    public AudioSource musicSource; // Assign your AudioSource from the Settings Panel
    private bool wasMusicPlayingBeforeVideo; // Track if the music was playing

    void Start()
    {
        videoPlayer.started += OnVideoStarted;
    }

    void Update()
    {
        // Detect screen click or touch
        if (Input.anyKeyDown || Input.GetMouseButton(0)) 
        {
            if (videoPlayer.isPlaying)
            {
                StopVideoAndResumeMusic();
            }
        }
    }

    void OnVideoStarted(VideoPlayer vp)
    {
        // Pause the music if it's playing
        if (musicSource != null && musicSource.isPlaying)
        {
            wasMusicPlayingBeforeVideo = true; // Remember the music state
            musicSource.Pause();
        }
    }

    void StopVideoAndResumeMusic()
    {
        // Stop the video
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        // Resume the music if it was playing before
        if (wasMusicPlayingBeforeVideo && musicSource != null)
        {
            Debug.Log("BGM UNPAUSE");
            musicSource.UnPause();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to avoid memory leaks
        if (videoPlayer != null)
        {
            videoPlayer.started -= OnVideoStarted;
        }
    }
}
