using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryDetails : MonoBehaviour
{

    public TextMeshProUGUI title;
    public TextMeshProUGUI synopsis;
    public Image posterImage;
    public Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => { StartCoroutine(StartStoryGame()); });
    }
    
    public void UpdateStoryDetailsPanel()
    {
        title.text = SceneManagerController.currentStory.storyTitle;
        synopsis.text = SceneManagerController.currentStory.synopsis;
        posterImage.sprite = SceneManagerController.currentStory.posterImage;
    }

    IEnumerator StartStoryGame()
    {
        yield return new WaitForSeconds(2f);
        UnitySceneManager.StaticLoadScene("StoryGameplayScreen");
    }
}
