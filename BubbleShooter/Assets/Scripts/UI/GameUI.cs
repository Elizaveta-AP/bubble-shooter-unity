using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private GameObject _pauseWindow, _settings;
    private Gun _gun;
    private AudioController _audioController;

    private void Start()
    {
        _pauseWindow = transform.Find("PauseWindow").gameObject;
        _settings = transform.Find("Settings").gameObject;
        _gun = FindObjectOfType<Gun>();
        _audioController = FindObjectOfType<AudioController>();
    }
    
    public void ButtonPause()
    {
        _audioController.PlayClick();
        _pauseWindow.SetActive(true);
        _gun.enabled = false;
    }
    
    public void ButtonContinue()
    {
        _audioController.PlayClick();
        _pauseWindow.SetActive(false);
        _gun.enabled = true;
    }
    
    public void ButtonReply()
    {
        _audioController.PlayClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ButtonMenu()
    {
        _audioController.PlayClick();
        SceneManager.LoadScene(0);
    }
    
    public void ButtonSettings()
    {
        _audioController.PlayClick();
        _pauseWindow.SetActive(false);
        _settings.SetActive(true);
    }    

    public void ButtonBack()
    {
        _audioController.PlayClick();
        _settings.SetActive(false);
        _pauseWindow.SetActive(true);
    }
}