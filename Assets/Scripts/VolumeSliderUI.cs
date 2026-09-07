using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider; // optional

    private void Start()
    {
        if (SoundManager.Instance == null) return;

        if (sfxSlider != null)
        {
            sfxSlider.SetValueWithoutNotify(SoundManager.Instance.GetSfxVolume());
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSfxVolume);
        }

        if (musicSlider != null)
        {
            musicSlider.SetValueWithoutNotify(SoundManager.Instance.GetMusicVolume());
            musicSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        }
    }
}
