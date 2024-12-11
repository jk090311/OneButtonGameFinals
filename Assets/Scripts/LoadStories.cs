using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStories : MonoBehaviour
{
    public GameObject storyPrefab; // The prefab to instantiate
    public Transform parentPanel; // The parent panel for the prefabs
    public StoryData[] stories; // Array of story data to assign
    public StoryDetails storyDetails; // Shared StoryDetails object

    // Method to refresh the prefab list
    public void RefreshPrefabs()
    {
        // Step 1: Delete all existing prefabs in the parent panel
        foreach (Transform child in parentPanel)
        {
            Destroy(child.gameObject); // Destroy each child GameObject
        }

        // Step 2: Reinstantiate prefabs with updated data
        foreach (var story in stories)
        {
            // Instantiate the prefab
            GameObject instance = Instantiate(storyPrefab, parentPanel);

            // Initialize it with the story data
            StoryDataManager dataManager = instance.GetComponent<StoryDataManager>();
            if (dataManager != null)
            {
                dataManager.Initialize(story, storyDetails);
            }
        }
    }

    // Example: Trigger refresh at runtime
    void Start()
    {
        RefreshPrefabs();
    }

}
