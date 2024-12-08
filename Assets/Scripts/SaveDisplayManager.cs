using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveDisplayManager : MonoBehaviour
{
    public RectTransform contentPanel; // Panel to hold save entries
    public GameObject saveEntryPrefab; // Prefab for save entries
    public List<StoryData> allStories; // All available stories
    public TextMeshProUGUI selectedTitleText; // Text to display the selected story title
    public Button deleteGameButton;
    public Button loadGameButton;
    private List<StoryData> savedStoryData;
    private string selectedSavedGameID;
    private StoryData currentLoadGame;
    private int currentLoadGameSceneIndex;

    void Start()
    {
        loadGameButton.onClick.AddListener(LoadSavedGame);
        deleteGameButton.onClick.AddListener(() => { DeleteSavedStory(selectedSavedGameID); });
        PopulateSavedStories();
        savedStoryData = ConvertToStoryData(SaveSceneManager.GetAllSavedStories(allStories), allStories);
    }

    // Populate saved stories dynamically
    void PopulateSavedStories()
    {
        Debug.Log("Populate Stories");
        // Clear existing entries
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Get all saved stories
        List<SavedStoryData> savedStories = SaveSceneManager.GetAllSavedStories(allStories);

        Debug.Log($"savedStories Length: {savedStories.Count}");

        foreach (var story in savedStories)
        {
            // Instantiate a new save entry
            GameObject saveEntry = Instantiate(saveEntryPrefab, contentPanel);

            // Set the text for the save entry
            TextMeshProUGUI saveText = saveEntry.GetComponentInChildren<TextMeshProUGUI>();
            if (saveText != null)
            {
                saveText.text = $"{story.Title} - Scene {story.SceneIndex}";
            }

            // Add functionality to the Load button
            Button savedGameButton = saveEntry.GetComponent<Button>();
            if (savedGameButton != null)
            {
                savedGameButton.onClick.AddListener(() =>
                {
                    Debug.Log($"{story.Title}");
                    UpdateSelectedDetails(story.Title, story.Title); // Update the title display
                });
            }

            // // Add functionality to the Delete button
            // Button deleteButton = saveEntry.transform.Find("DeleteGameProgressButton").GetComponent<Button>();
            // if (deleteButton != null)
            // {
            //     deleteButton.onClick.AddListener(() =>
            //     {
            //         Debug.Log($"Deleting {story.Title}");
            //         DeleteSavedStory(story);
            //     });
            // }
        }
    }

    List<StoryData> ConvertToStoryData(List<SavedStoryData> savedStories, List<StoryData> allStories)
    {
        List<StoryData> storyDataList = new List<StoryData>();

        foreach (var savedStory in savedStories)
        {
            // Find the matching StoryData by ID or Title
            StoryData matchingStory = allStories.Find(story => story.storyTitle == savedStory.StoryID);

            if (matchingStory != null)
            {
                storyDataList.Add(matchingStory);
            }
            else
            {
                Debug.LogWarning($"StoryData not found for StoryID: {savedStory.StoryID}");
            }
        }

        return storyDataList;
    }

    // Update the UI text to show the selected story title
    void UpdateSelectedDetails(string title, string storyID)
    {
        if (selectedTitleText != null)
        {
            selectedTitleText.text = $"Selected Story: {title}";
        }

        StoryData storyData = savedStoryData.Find(story => story.storyTitle == storyID);
        if (storyData == null)
        {
            Debug.Log("No Story Data Found");
        }
        currentLoadGame = storyData;
        currentLoadGameSceneIndex = SaveSceneManager.LoadStoryData(storyID).SceneIndex;
        selectedSavedGameID = storyID;
    }

    void LoadSavedGame()
    {
        StartCoroutine(StartStoryGame());
    }

    IEnumerator StartStoryGame()
    {
        SceneManagerController.AssignStory(currentLoadGame);
        SceneManagerController.currentSceneIndex = currentLoadGameSceneIndex;
        yield return new WaitForSeconds(2f);
        UnitySceneManager.StaticLoadScene("StoryGameplayScreen");
    }

    // Delete the saved story
    void DeleteSavedStory(string storyID)
    {
        SaveSceneManager.ClearSavedData(storyID);
        PopulateSavedStories(); // Refresh the list after deletion
    }
}
