using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    public Slider masterVol,musicVol,sfxVol;
    public AudioMixer mainAudioMixer;
    // Start is called before the first frame update

    public void ChangeMasterVolume()
    {
        float mappedVolume = Mathf.Lerp(-80f, 0f, masterVol.value);
        mainAudioMixer.SetFloat("MasterVol",masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        float mappedVolume = Mathf.Lerp(-80f, 0f, musicVol.value);
        mainAudioMixer.SetFloat("MusicVol",musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        float mappedVolume = Mathf.Lerp(-80f, 0f, sfxVol.value);
        mainAudioMixer.SetFloat("SFXVol",sfxVol.value);
    }
}
