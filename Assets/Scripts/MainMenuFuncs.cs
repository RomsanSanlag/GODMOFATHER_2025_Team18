using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuFuncs : MonoBehaviour
{
    [SerializeField] GameManager _gm;
    
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _settingsMenu;
    [SerializeField] GameObject _gameMenu;
    [SerializeField] GameObject _scoreMenu;
    [SerializeField] GameObject _raceMenu;
    
    [SerializeField] GameObject _paddle1;
    [SerializeField] GameObject _paddle2;
    
    public CameraFollow cameraFollow;
    public SpawnObjects spawnObjects;
    public Score score;
    
    
    [SerializeField] Button _buttonScreenShake;
    bool _isMenuActive = true;
    
    public void PlayGame()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _gameMenu.SetActive(true);
        
        _paddle1.SetActive(true);
        _paddle2.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Settings()
    {
        if (_isMenuActive)
        {
            _mainMenu.SetActive(false);
            _settingsMenu.SetActive(true);
            _isMenuActive = false;
        }
        else
        {
            _mainMenu.SetActive(true);
            _settingsMenu.SetActive(false);
            _isMenuActive = true;
        }
    }

    public void ToggleScreenShake()
    {
        _gm.isSreenShakeEnable = !_gm.isSreenShakeEnable;

        if (_gm.isSreenShakeEnable)
        {
            _buttonScreenShake.image.color = Color.green;
        }
        else
        {
            _buttonScreenShake.image.color = Color.red;
        }
    }

    public void ScoreScreen()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _gameMenu.SetActive(false);
        _scoreMenu.SetActive(true);
        
        _paddle1.SetActive(false);
        _paddle2.SetActive(false);
        
        score.printScore();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenuGA");
    }

    public void StartRace()
    {
        _gameMenu.SetActive(false);
        _raceMenu.SetActive(true);
        
        cameraFollow.enabled = true;
        spawnObjects.DestroyAllSpawned();
        
        Transform child = _paddle1.transform.GetChild(0);
        child.gameObject.SetActive(true);
        
        Transform child2 = _paddle2.transform.GetChild(0);
        child.gameObject.SetActive(true);
    }
}
