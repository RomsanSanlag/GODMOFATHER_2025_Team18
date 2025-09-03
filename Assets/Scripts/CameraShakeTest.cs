using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    
    private Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition; // Save origin pos
    }

    void Start()
    {
        Shake();
    }
    
    public void Shake(float intensity = 0.2f, float duration = 0.3f)
    {
        if (!_gm.isSreenShakeEnable) return;
        StopAllCoroutines(); // if already exist kill it
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

        transform.localPosition = originalPos; // place back the cam to orign 
    }
}
