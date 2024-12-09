using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryDataManager : MonoBehaviour
{
    public StoryData story; // The story associated with this button
    public Image posterImage;
    public TextMeshProUGUI title;
    public StoryDetails storyDetails;

    void Start()
    {
        posterImage.sprite = story.posterImage;
        title.text = story.storyTitle;
        // Attach methods to buttons
        GetComponent<Button>().onClick.AddListener(() =>
        {
            // StartCoroutine(StartStoryGame());
            SceneManagerController.AssignStory(story);
            storyDetails.UpdateStoryDetailsPanel();
        });
    }
}