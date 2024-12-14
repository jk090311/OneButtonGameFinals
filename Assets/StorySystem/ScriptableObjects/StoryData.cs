using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Story", menuName = "Story System/Story")]
public class StoryData : ScriptableObject
{
    public Sprite posterImage;
    public string storyTitle;
    public string genre;
    public string synopsis;

    public AudioClip storyMusic;
    public SceneData[] scenes; // Array to hold the scenes in the story
}
