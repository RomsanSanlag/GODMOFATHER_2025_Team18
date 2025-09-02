using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    public bool cameraShakeEnabled = true; // Toggle d'accessibilité

    private Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition; // Sauvegarde la position d'origine
    }

    void Start()
    {
        Shake();
    }
    
    public void Shake(float intensity = 0.2f, float duration = 0.3f)
    {
        if (!cameraShakeEnabled) return;
        StopAllCoroutines(); // stop si un shake est déjà en cours
        StartCoroutine(ShakeRoutine(intensity, duration));
    }

    private IEnumerator ShakeRoutine(float intensity, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos; // Remet la caméra à sa position initiale
    }
}
