using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Unity VideoPlayer component
    public ChoiceController choiceController; // Reference to the ChoiceController
    public SceneManagerController sceneManagerController; // Reference to SceneManagerController

    public void PlayScene(SceneData scene)
    {
        if (scene.introVideo != null)
        {
            videoPlayer.clip = scene.introVideo;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("No intro video found for this scene.");
        }

        // Initialize the ChoiceController for this scene
        if (choiceController != null)
        {
            choiceController.Initialize(scene, sceneManagerController);
        }
        else
        {
            Debug.LogError("ChoiceController not assigned.");
        }

        // Handle video end (if needed to trigger other behaviors)
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    public void PlayChoice(ChoiceData choice)
    {
        // Unsubscribe previous event to avoid duplicate calls
        videoPlayer.loopPointReached -= OnVideoEnd;

        if (choice.videoClip != null)
        {
            Debug.Log("Has Transition Clip.");
            videoPlayer.clip = choice.videoClip;
            videoPlayer.Play();

            // Subscribe to event for when the video ends
            videoPlayer.loopPointReached += (VideoPlayer vp) =>
            {
                Debug.Log("Intro Video Finished");
                if (choice.nextScene != null)
                {
                    Debug.Log("Loading Next Scene.........");
                    sceneManagerController.LoadScene(choice.nextScene);
                }
                else
                {
                    Debug.LogWarning("No next scene linked to this choice.");
                }
            };

        }
        else
        {
            Debug.Log("No Transition Clip. Loading next scene directly.");
            if (choice.nextScene != null)
            {
                sceneManagerController.LoadScene(choice.nextScene);
            }
            else
            {
                Debug.LogWarning("No next scene linked to this choice.");
            }
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        choiceController.OnVideoEnd(); // Notify ChoiceController
        Debug.Log("Video ended.");
    }
}
