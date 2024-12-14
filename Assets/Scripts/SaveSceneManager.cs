using System;
using System.Collections.Generic;

using UnityEngine;

public class SaveSceneManager : MonoBehaviour
{
    // Save a story's progress
    public static void SaveStoryData(StoryData story, int sceneIndex)
    {
        string storyKey = $"{story.storyTitle}_Data";

        Debug.Log(storyKey);

        SavedStoryData data = new SavedStoryData
        {
            StoryImage = story.posterImage,
            StoryID = story.storyTitle,
            SceneIndex = sceneIndex,
            Title = story.storyTitle,
            Genre = story.genre,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(storyKey, json);
        PlayerPrefs.Save();

        Debug.Log($"Saved Story: {story.storyTitle}, Scene Index: {sceneIndex}");
    }

    // Load a story's saved data
    public static SavedStoryData LoadStoryData(string storyID)
    {
        string storyKey = $"{storyID}_Data";

        if (PlayerPrefs.HasKey(storyKey))
        {
            string json = PlayerPrefs.GetString(storyKey);
            return JsonUtility.FromJson<SavedStoryData>(json);
        }

        // Return default if no data exists
        return default;
    }

    // Get all saved stories
    public static List<SavedStoryData> GetAllSavedStories(List<StoryData> stories)
    {
        List<SavedStoryData> savedStories = new List<SavedStoryData>();

        foreach (var story in stories)
        {
            string storyKey = $"{story.storyTitle}_Data";

            Debug.Log("Get All Saves "+ storyKey);

            if (PlayerPrefs.HasKey(storyKey))
            {
                string json = PlayerPrefs.GetString(storyKey);
                SavedStoryData data = JsonUtility.FromJson<SavedStoryData>(json);
                savedStories.Add(data);
                Debug.Log(true);
            }
        }

        savedStories.Sort((a, b) => b.Timestamp.CompareTo(a.Timestamp));

        return savedStories;
    }

    // Clear saved data for a specific story
    public static void ClearSavedData(string storyID)
    {
        string storyKey = $"{storyID}_Data";
        PlayerPrefs.DeleteKey(storyKey);
        PlayerPrefs.Save();
        Debug.Log($"Cleared saved data for story: {storyID}");
    }
}

// Struct to store saved data
[System.Serializable]
public struct SavedStoryData
{
    public Sprite StoryImage;
    public string StoryID;
    public int SceneIndex;
    public string Title;
    public string Genre;
    public long Timestamp; // Use Unix time for easy sorting
}
