using UnityEngine;
using UnityEngine.UI;

public class SceneManagerController : MonoBehaviour
{
    public static StoryData currentStory; // The active story
    public VideoPlayerController videoPlayerController; // Reference to the VideoPlayerController

    /*
    * Remove value 0 to make it dynamic for loading
    */
    public static int currentSceneIndex = 0; // Tracks the current scene index
    public Animator endPromptPanelAnimator;
    public Button backOrSaveButton;

    void Start()
    {
        backOrSaveButton.onClick.AddListener(() => {
            // For Saving
            // SaveSceneManager.SaveCurrentScene(currentStory.name, currentSceneIndex);
            UnitySceneManager.StaticLoadScene("TitleScreen");
        });
        if (currentStory != null && currentStory.scenes.Length > 0)
        {
            LoadScene(currentStory.scenes[currentSceneIndex]);
        }
        else
        {
            Debug.LogError("No scenes available in the current story.");
        }
    }

    public static void AssignStory(StoryData story)
    {
        currentStory = story;
    }

    public void LoadScene(SceneData scene)
    {
        if (videoPlayerController != null)
        {
            currentSceneIndex++;
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
        Debug.Log($"Current Scene Index: ${currentSceneIndex}");
        if (currentSceneIndex < currentStory.scenes.Length)
        {
            LoadScene(currentStory.scenes[currentSceneIndex]);
        }
        else
        {
            Debug.Log("End of story reached.");
            ShowEndOfStoryPrompt();
        }
    }

    // New method to show the end of story prompt
    public void ShowEndOfStoryPrompt()
    {
        Debug.Log("The story has ended. Displaying the end of story prompt.");
        // Here you can enable your UI prompt for the end of the story.
        // For example, display a canvas with "The End" text or any other UI element.
        TransitionManager.EndingPromptOpen(endPromptPanelAnimator);
    }

    public void RestartStory()
    {
        TransitionManager.EndingPromptClose(endPromptPanelAnimator);
        currentSceneIndex = 0;
    }
}
