using UnityEngine;

public class SceneManagerController : MonoBehaviour
{
    public StoryData currentStory; // The active story
    public VideoPlayerController videoPlayerController; // Reference to the VideoPlayerController
    public int currentSceneIndex = 0; // Tracks the current scene index

    void Start()
    {
        if (currentStory != null && currentStory.scenes.Length > 0)
        {
            LoadScene(currentStory.scenes[currentSceneIndex]);
        }
        else
        {
            Debug.LogError("No scenes available in the current story.");
        }
    }

    public void LoadScene(SceneData scene)
    {
        if (videoPlayerController != null)
        {
            videoPlayerController.PlayScene(scene);
        }
        else
        {
            Debug.LogError("VideoPlayerController not assigned.");
        }
    }

    public void LoadNextScene()
    {
        currentSceneIndex++;
        if (currentSceneIndex < currentStory.scenes.Length)
        {
            LoadScene(currentStory.scenes[currentSceneIndex]);
        }
        else
        {
            Debug.Log("End of story reached.");
        }
    }
}
