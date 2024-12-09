using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class TitleScreenIdle : MonoBehaviour
{
    public float inactivityDuration = 10f;
    public VideoPlayer videoPlayer;
    public GameManager gameManager;
    public RawImage rawImage;
    private float  inactivityTimer = 0f;
    public bool isPlayingVideo = false;

    void Start()
    {
        videoPlayer.gameObject.SetActive(false);
        rawImage.enabled = false;
    }

    void Update()
    {
        if(Input.anyKeyDown || Input.GetMouseButton(0))
        {
            ResetInactivityTimer();
        }
        if(!isPlayingVideo)
        {
            inactivityTimer += Time.deltaTime;
            if(inactivityTimer >= inactivityDuration)
            {
                PlayInactivityVideo();
            }
        }     
    }
    void ResetInactivityTimer()
        {
            inactivityTimer = 0f;
            if(isPlayingVideo)
            {
                StopInactivityVideo();
            }
        }

        void PlayInactivityVideo()
        {
            isPlayingVideo = true;
            videoPlayer.gameObject.SetActive(true);
            rawImage.enabled = true;
            videoPlayer.Play();
        }

        void StopInactivityVideo()
        {
            isPlayingVideo = false;
            videoPlayer.Stop();
            videoPlayer.gameObject.SetActive(false);
            rawImage.enabled = false;
            Debug.Log("HAHA");
            gameManager.musicSource.UnPause();
        }
}
