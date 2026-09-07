using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSound;
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource breadSound;
    [SerializeField] private AudioSource takeDamageSound;
    [SerializeField] private AudioSource heartSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource powerupSound;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float musicVolume = 1f;
    private int musicPlaying;
    private int volumePlaying;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadAudioPreferences();
    }

    private void LoadAudioPreferences()
    {
        musicPlaying = PlayerPrefs.GetInt("MusicPlaying", 1);
        volumePlaying = PlayerPrefs.GetInt("VolumePlaying", 1);

        sfxVolume = PlayerPrefs.GetFloat("SfxVolume", volumePlaying == 1 ? 1f : 0f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicPlaying == 1 ? 1f : 0f);

        ApplyVolumes();

        if (musicPlaying == 1 && musicSound != null && !musicSound.isPlaying)
            musicSound.Play();
        if (musicPlaying == 0 && musicSound != null)
            musicSound.Pause();
    }

    private void SaveVolumePreference()
    {
        PlayerPrefs.SetInt("VolumePlaying", volumePlaying);
        PlayerPrefs.Save();
    }

    private void SaveMusicPreference()
    {
        PlayerPrefs.SetInt("MusicPlaying", musicPlaying);
        PlayerPrefs.Save();
    }

    public void PlayButtonSound()
    {
        if (volumePlaying == 1)
            buttonSound.PlayOneShot(buttonSound.clip);
    }
    public void PlayBreadSound()
    {
        if (volumePlaying == 1)
            breadSound.PlayOneShot(breadSound.clip);
    }
    public void PlayTakeDamageSound()
    {
        if (volumePlaying == 1)
            takeDamageSound.PlayOneShot(takeDamageSound.clip);
    }
    public void PlayHeartSound()
    {
        if (volumePlaying == 1)
            heartSound.PlayOneShot(heartSound.clip);
    }
    public void PlayPowerupSound()
    {
        if (volumePlaying == 1)
            powerupSound.PlayOneShot(powerupSound.clip);
    }
    public void PlayDeathSound()
    {
        if (volumePlaying == 1)
            deathSound.PlayOneShot(deathSound.clip);
    }

    public void MusicOn()
    {
        musicSound.Play();
        musicPlaying = 1;
        SaveMusicPreference();
        Debug.Log("Music On");
    }
    public void MusicOff()
    {
        musicSound.Pause();
        musicPlaying = 0;
        SaveMusicPreference();
        Debug.Log("Music Off");
    }
    public void VolumeOn()
    {
        buttonSound.Play();
        volumePlaying = 1;
        SaveVolumePreference();
        Debug.Log("Volume On");
    }
    public void VolumeOff()
    {
        buttonSound.Pause();
        volumePlaying = 0;
        SaveVolumePreference();
        Debug.Log("Volume Off");
    }

    public int GetMusicPlaying()
    {
        return musicPlaying;
    }
    public int GetVolumePlaying()
    {
        return volumePlaying;
    }
    public void SetSfxVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        ApplyVolumes();
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.Save();

        volumePlaying = (sfxVolume > 0.001f) ? 1 : 0;
        SaveVolumePreference();
    }

    public float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat("SfxVolume", 1f);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);
        ApplyVolumes();
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();

        musicPlaying = (musicVolume > 0.001f) ? 1 : 0;
        SaveMusicPreference();
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    private void ApplyVolumes()
    {
        if (musicSound != null) musicSound.volume = musicVolume;

        if (buttonSound != null) buttonSound.volume = sfxVolume;
        if (breadSound != null) breadSound.volume = sfxVolume;
        if (takeDamageSound != null) takeDamageSound.volume = sfxVolume;
        if (heartSound != null) heartSound.volume = sfxVolume;
        if (deathSound != null) deathSound.volume = sfxVolume;
        if (powerupSound != null) powerupSound.volume = sfxVolume;
    }

}