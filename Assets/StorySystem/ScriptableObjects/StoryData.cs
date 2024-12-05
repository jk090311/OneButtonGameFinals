using UnityEngine;

[CreateAssetMenu(fileName = "New Story", menuName = "Story System/Story")]
public class StoryData : ScriptableObject
{
    public string storyTitle;
    public SceneData[] scenes; // Array to hold the scenes in the story
}
