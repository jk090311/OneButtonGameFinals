using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameplayManager : MonoBehaviour
{
    public SceneManagerController sceneManagerController;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI pauseMenuStoryTitle;
    public TextMeshProUGUI saveProgressText;
    public Button pauseMenuButton;
    public Button resumeButton;
    public Button saveProgressButton;
    public Button backToMainMenu;
    public TextMeshProUGUI quitQuestion;
    public bool isCurrentSaved = false;
    void Start()
    {
        pauseMenuStoryTitle.text = SceneManagerController.currentStory.storyTitle;
        pauseMenuButton.onClick.AddListener(() => { videoPlayer.Pause(); });
        resumeButton.onClick.AddListener(() =>
        {
            if (videoPlayer.isPaused)
            {
                if (ChoiceController.isVideoPlaying)
                {
                    videoPlayer.Play();
                }
            }
        });
        saveProgressButton.onClick.AddListener(() =>
        {
            // For Saving
            SaveSceneManager.SaveStoryData(SceneManagerController.currentStory, SceneManagerController.currentSceneIndex);
            isCurrentSaved = true;
            SaveButtonState();
            Debug.Log("Saved Progress");
            // UnitySceneManager.StaticLoadScene("TitleScreen");
        });
        backToMainMenu.onClick.AddListener(() => {
            quitQuestion.text = isCurrentSaved ? "Are you sure you want to quit the game?" : "Are you sure you want to quit the game without saving??";
        });
    }

    public void SaveButtonState()
    {
        saveProgressButton.interactable = isCurrentSaved ? false : true;
        saveProgressText.text = isCurrentSaved ? "Progress Saved!" : "Save Progress";
    }
}
