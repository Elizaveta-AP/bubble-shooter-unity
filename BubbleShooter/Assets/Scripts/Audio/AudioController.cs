using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> Pops = new List<AudioClip>();
    [SerializeField] private Slider _musicSlider, _soundsSlider;
    private AudioSource _pop, _click, _backMelody;
    private List<AudioSource> Sounds = new List<AudioSource>();
    private System.Random _random = new System.Random();
    
    void Start()
    {
        _pop = transform.GetChild(0).GetComponent<AudioSource>();
        _click = transform.GetChild(1).GetComponent<AudioSource>();
        _backMelody = transform.GetChild(2).GetComponent<AudioSource>();
        Sounds.Add(_pop);
        Sounds.Add(_click);

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            PlayerPrefs.SetFloat("SoundsVolume", 0.5f);
        }

        _backMelody.volume = PlayerPrefs.GetFloat("MusicVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        
        foreach (AudioSource sound in Sounds)
        {
            sound.volume = PlayerPrefs.GetFloat("SoundsVolume");
        }
        _soundsSlider.value = PlayerPrefs.GetFloat("SoundsVolume");
        
    }

    public void PlayPop()
    {
        AudioClip pop = Pops[_random.Next(Pops.Count)];
        _pop.PlayOneShot(pop);
    }

    public void PlayClick()
    {
        _click.Play();
    }    

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        _backMelody.volume = volume;
    }

    public void SetSoundsVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundsVolume", volume);
        PlayerPrefs.Save();
        foreach (AudioSource sound in Sounds)
        {
            sound.volume = volume;
        }
    }
}
