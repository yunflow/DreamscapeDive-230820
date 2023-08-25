using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("themeVolume"))
            LoadVolume();
        else
        {
            SetThemeVolume();
            SetSFXVolume();
        }
    }

    public void SetThemeVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("theme", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("themeVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("themeVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetThemeVolume();
        SetSFXVolume();
    }
}
