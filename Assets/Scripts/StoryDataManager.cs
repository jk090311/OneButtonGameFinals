using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoryDataManager : MonoBehaviour
{
    public StoryData story; // The story associated with this button

    void Start()
    {
        // Attach methods to buttons
        GetComponent<Button>().onClick.AddListener(() =>
        {
            // Bugs not fix this is for saving and loading scenes
            // if (TransitionManager.isNewGame)
            // {
            //     CheckNewGame();
            // }
            // else
            // {
            //     CheckLoadGame();
            // }
            StartCoroutine(StartStoryGame());
        });
    }

    // Check if a new game can be started
    void CheckNewGame()
    {
        if (SaveSceneManager.HasSaveData(story.name))
        {
            Debug.Log("This story has saved progress. Starting a new game will delete it. Continue?");
        }
        else
        {
            Debug.Log("Starting a new game...");
            StartNewGame(false);
        }
    }

    // Check if the game can be loaded
    void CheckLoadGame()
    {
        if (SaveSceneManager.HasSaveData(story.name))
        {
            LoadGame();
        }
        else
        {
            Debug.Log("No saved progress for this story. Cannot load game.");
        }
    }

    // Start a new game
    void StartNewGame(bool deleteSave)
    {
        if (deleteSave)
        {
            SaveSceneManager.ClearSavedData(story.name);
        }

        StartCoroutine(StartStoryGame());
    }

    // Load the saved game
    void LoadGame()
    {
        int savedSceneIndex = SaveSceneManager.LoadCurrentScene(story.name);
        SceneManagerController.AssignStory(story);
        SceneManagerController.currentSceneIndex = savedSceneIndex;
        UnitySceneManager.StaticLoadScene("StoryGameplayScreen");
    }

    // Coroutine to start the story gameplay
    IEnumerator StartStoryGame()
    {
        SceneManagerController.AssignStory(story);
        yield return new WaitForSeconds(2f);
        UnitySceneManager.StaticLoadScene("StoryGameplayScreen");
    }
}