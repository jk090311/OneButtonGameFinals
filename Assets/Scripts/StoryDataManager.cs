using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryDataManager : MonoBehaviour
{
    StoryData story; // The story associated with this button
    Image posterImage;
    TextMeshProUGUI title;
    StoryDetails storyDetails;

    // Method to initialize the prefab with dynamic data
    public void Initialize(StoryData storyData, StoryDetails details)
    {
        story = storyData;
        posterImage = transform.Find("PosterImage").GetComponent<Image>();
        title = GetComponentInChildren<TextMeshProUGUI>();
        storyDetails = details;
        
        // Update the visual elements
        posterImage.sprite = story.posterImage;
        title.text = story.storyTitle;
        
        // Attach the button's functionality dynamically
        GetComponent<Button>().onClick.RemoveAllListeners(); // Clear previous listeners if any
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManagerController.AssignStory(story);
            storyDetails.UpdateStoryDetailsPanel();
        });
    }
}
