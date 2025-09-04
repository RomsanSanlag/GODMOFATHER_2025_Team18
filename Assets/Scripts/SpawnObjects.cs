using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] Transform _center;
    
    [FormerlySerializedAs("_prefabsToSpawn")]
    [Header("Spawn Objects B Tier")]
    [SerializeField] List<GameObject> _BTierObjects;
    [SerializeField] float _spawnIntervalBTier = 0.5f;
    [SerializeField] float _radiusB = 5f;
    private float _timerBTier;
    
    [Header("Spawn Objects A Tier")]
    [SerializeField] List<GameObject> _ATierObjects;
    [SerializeField] float _spawnIntervalATier = 3f;
    [SerializeField] float _radiusA = 5f;
    private float _timerATier;
    
    [Header("Spawn Objects S Tier")]
    [SerializeField] List<GameObject> _STierObjects;
    [SerializeField] float _spawnIntervalSTier = 6f;
    [SerializeField] float _radiusS = 5f;
    private float _timerSTier;
    
    [Header("Angle of Spawning")]
    [Range(0f, Mathf.PI / 2f)]
    [SerializeField] float _angleOffset = 0.2f; // float to ajust angle for spawning

    void Update()
    {
        #region Timers

        // B Tier
        _timerBTier += Time.deltaTime;
        
        if (_timerBTier >= _spawnIntervalBTier)
        {
            SpawnObjectBTier();
            _timerBTier = 0f;
        }
        //
        
        // A Tier
        _timerATier += Time.deltaTime;
        
        if (_timerATier >= _spawnIntervalATier)
        {
            SpawnObjectATier();
            _timerATier = 0f;
        }
        //
        
        // S Tier
        _timerSTier += Time.deltaTime;
        
        if (_timerSTier >= _spawnIntervalSTier)
        {
            SpawnObjectSTier();
            _timerSTier = 0f;
        }
        //

        #endregion
    }

    private void SpawnObjectSTier()
    {
        if (_STierObjects.Count == 0) return; // if empty return
        
        GameObject prefab = _STierObjects[Random.Range(0, _STierObjects.Count)];

        // angle radius clamp π → 2π
        float angle = Random.Range(Mathf.PI / 2 + _angleOffset, 3 * Mathf.PI / 2 - _angleOffset);

        // 2D position
        Vector2 pos = new Vector2(
            _center.position.x + Mathf.Cos(angle) * _radiusS,
            _center.position.y + Mathf.Sin(angle) * _radiusS
        );
        
        Instantiate(prefab, pos, Quaternion.identity);
    }

    private void SpawnObjectATier()
    {
        if (_ATierObjects.Count == 0) return; // if empty return
        
        GameObject prefab = _ATierObjects[Random.Range(0, _ATierObjects.Count)];

        // angle radius clamp π → 2π
        float angle = Random.Range(Mathf.PI / 2 + _angleOffset, 3 * Mathf.PI / 2 - _angleOffset);
        
        // 2D position
        Vector2 pos = new Vector2(
            _center.position.x + Mathf.Cos(angle) * _radiusA,
            _center.position.y + Mathf.Sin(angle) * _radiusA
        );

        Instantiate(prefab, pos, Quaternion.identity);
    }

    void SpawnObjectBTier()
    {
        if (_BTierObjects.Count == 0) return; // if empty return
        
        GameObject prefab = _BTierObjects[Random.Range(0, _BTierObjects.Count)];

        // angle radius clamp π → 2π
        float angle = Random.Range(Mathf.PI / 2 + _angleOffset, 3 * Mathf.PI / 2 - _angleOffset);
        
        // 2D position
        Vector2 pos = new Vector2(
            _center.position.x + Mathf.Cos(angle) * _radiusB,
            _center.position.y + Mathf.Sin(angle) * _radiusB
        );
        
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
