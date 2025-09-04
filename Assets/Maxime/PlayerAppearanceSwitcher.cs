using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAppearanceSwitcher : MonoBehaviour
{
    public GameObject bodyP1;
    public GameObject bodyP2;
    public GameObject rameP1;
    public GameObject rameP2;

    void Start()
    {
        var pi = GetComponent<PlayerInput>();
        bool isP2 = pi != null && pi.playerIndex == 1;

        if (bodyP1) bodyP1.SetActive(!isP2);
        if (bodyP2) bodyP2.SetActive(isP2);

        if (rameP1) rameP1.SetActive(!isP2);
        if (rameP2) rameP2.SetActive(isP2);
    }
}