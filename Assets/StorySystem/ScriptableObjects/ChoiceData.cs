using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Choice", menuName = "Story System/Choice")]
public class ChoiceData : ScriptableObject
{
    public string choiceText;
    public VideoClip videoClip; // Video that plays for this choice
    public SceneData nextScene; // The scene that follows this choice
}
