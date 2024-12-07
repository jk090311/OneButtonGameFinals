using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitySceneManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void StaticLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
