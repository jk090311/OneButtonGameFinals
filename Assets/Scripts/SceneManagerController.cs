using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerController : MonoBehaviour
{
    public static StoryData currentStory; // The active story
    public VideoPlayerController videoPlayerController; // Reference to the VideoPlayerController
    public static int currentSceneIndex; // Tracks the current scene index
    public Animator endPromptPanelAnimator;
    public Button backOrSaveButton;

    void Start()
    {
        backOrSaveButton.onClick.AddListener(() =>
        {
            // For Saving
            SaveSceneManager.SaveStoryData(currentStory, currentSceneIndex);
            UnitySceneManager.StaticLoadScene("TitleScreen");
        });
        if (currentStory != null && currentStory.scenes.Length > 0)
        {
            if (TransitionManager.isNewGame)
            {
                currentSceneIndex = 0;
                LoadScene(currentStory.scenes[currentSceneIndex]);
            }
            else
            {
                LoadScene(currentStory.scenes[currentSceneIndex]);
            }
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
        Debug.Log($"Current Scene Index: {currentSceneIndex}");
        currentSceneIndex = System.Array.IndexOf(currentStory.scenes, scene);
        if (videoPlayerController != null)
        {
            videoPlayerController.PlayScene(scene);
        }
        else
        {
            Debug.LogError("VideoPlayerController not assigned.");
        }
    }

    // New method to show the end of story prompt
    public void ShowEndOfStoryPrompt()
    {
        Debug.Log("The story has ended. Displaying the end of story prompt.");
        TransitionManager.EndingPromptOpen(endPromptPanelAnimator);
    }

    public void RestartStory()
    {
        TransitionManager.EndingPromptClose(endPromptPanelAnimator);
        currentSceneIndex = 0;
    }
}
