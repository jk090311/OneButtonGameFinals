using UnityEngine;

public class SaveSceneManager : MonoBehaviour
{
    private const string CurrentSceneKey = "CurrentSceneIndex";
    private const string CurrentStoryKey = "CurrentStoryID";

    // Save the current scene and story
    public static void SaveCurrentScene(string storyID, int sceneIndex)
    {
        PlayerPrefs.SetString(CurrentStoryKey, storyID); // Save story ID
        PlayerPrefs.SetInt(CurrentSceneKey, sceneIndex); // Save scene index
        PlayerPrefs.Save(); // Write changes to disk
        Debug.Log($"Saved Story ID: {storyID}, Scene Index: {sceneIndex}");
    }

    // Load the saved scene index for a given story
    public static int LoadCurrentScene(string storyID)
    {
        if (PlayerPrefs.HasKey(CurrentStoryKey) && PlayerPrefs.HasKey(CurrentSceneKey))
        {
            string savedStoryID = PlayerPrefs.GetString(CurrentStoryKey);
            if (savedStoryID == storyID)
            {
                return PlayerPrefs.GetInt(CurrentSceneKey);
            }
        }

        // If no saved data exists or story ID doesn't match, return 0 (start from the first scene)
        return 0;
    }

    // Check if there is saved data for a particular story
    public static bool HasSaveData(string storyID)
    {
        return PlayerPrefs.HasKey(CurrentStoryKey) && PlayerPrefs.HasKey(CurrentSceneKey) && PlayerPrefs.GetString(CurrentStoryKey) == storyID;
    }

    // Clear saved data for the current story
    public static void ClearSavedData(string storyID)
    {
        PlayerPrefs.DeleteKey(storyID);
        PlayerPrefs.DeleteKey(CurrentSceneKey);
        PlayerPrefs.Save();
        Debug.Log("Cleared saved story and scene data.");
    }
}
