using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Story", menuName = "Story System/Story")]
public class StoryData : ScriptableObject
{
    public Sprite posterImage;
    public string storyTitle;
    public string synopsis;
    public SceneData[] scenes; // Array to hold the scenes in the story
}
