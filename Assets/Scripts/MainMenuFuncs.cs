using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuFuncs : MonoBehaviour
{
    [SerializeField] GameManager _gm;
    
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _settingsMenu;
    [SerializeField] Button _buttonScreenShake;
    bool _isMenuActive = true;
    
    public void PlayGame()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
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
}
