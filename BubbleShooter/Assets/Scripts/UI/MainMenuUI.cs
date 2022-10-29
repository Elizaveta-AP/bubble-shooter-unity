using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private GameObject _classicLevels, _randomLevels, _settings;
    private AudioController _audioController;
    
    void Start()
    {
        _classicLevels = transform.Find("ClassicLevels").gameObject;
        _randomLevels = transform.Find("RandomLevels").gameObject;
        _settings = transform.Find("Settings").gameObject;
        _audioController = FindObjectOfType<AudioController>();
    }

    public void ButtonClassicGame()
    {
        _audioController.PlayClick();
        _classicLevels.SetActive(true);
    }

    public void ButtonRandomGame()
    {
        _audioController.PlayClick();
        _randomLevels.SetActive(true);
    }

    public void ButtonStartLevel(string nameScene)
    {
        _audioController.PlayClick();
        SceneManager.LoadScene(nameScene);
    }
    
    public void ButtonBack(GameObject window)
    {
        _audioController.PlayClick();
        window.SetActive(false);
    }

    public void ButtonExit()
    {
        _audioController.PlayClick();
        Application.Quit();
    }
    
    public void ButtonSettings()
    {
        _audioController.PlayClick();
        _settings.SetActive(true);
    }
}