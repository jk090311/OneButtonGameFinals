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
            StartCoroutine(StartStoryGame());
        });
    }


    // Coroutine to start the story gameplay
    IEnumerator StartStoryGame()
    {
        SceneManagerController.AssignStory(story);
        yield return new WaitForSeconds(2f);
        UnitySceneManager.StaticLoadScene("StoryGameplayScreen");
    }
}