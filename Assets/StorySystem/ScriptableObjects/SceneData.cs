using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Scene", menuName = "Story System/Scene")]
public class SceneData : ScriptableObject
{
    public string sceneName;
    public VideoClip introVideo;
    public ChoiceData[] choices; // Choices the player can make in this scene
}
