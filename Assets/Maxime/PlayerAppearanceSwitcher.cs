using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAppearanceSwitcher : MonoBehaviour
{
    
    Transform FindPath(string path) => transform.Find(path);

    void Start()
    {
        var pi = GetComponent<PlayerInput>();
        bool isP2 = pi != null && pi.playerIndex == 1;

        var bodyP1 = GetComponentInChildren<BodyP1Tag>(true)?.gameObject;
        var bodyP2 = GetComponentInChildren<BodyP2Tag>(true)?.gameObject;
        var rameP1 = GetComponentInChildren<RameP1Tag>(true)?.gameObject;
        var rameP2 = GetComponentInChildren<RameP2Tag>(true)?.gameObject;

        if (bodyP1) bodyP1.SetActive(!isP2);
        if (bodyP2) bodyP2.SetActive(isP2);
        if (rameP1) rameP1.SetActive(!isP2);
        if (rameP2) rameP2.SetActive(isP2);
    }


}
