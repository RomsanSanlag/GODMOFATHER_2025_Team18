using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] Transform _center;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    
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
    [Range(0f, Mathf.PI)]
    public float spread = 0.2f; // float to ajust angle for spawning
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
        float angle = Random.Range(Mathf.PI - spread, Mathf.PI + spread);

        // 2D position
        Vector2 pos = new Vector2(
            _center.position.x + Mathf.Cos(angle) * _radiusS + Random.Range(-0.5f, 0.5f),
            _center.position.y + Mathf.Sin(angle) * _radiusS + Random.Range(-0.5f, 0.5f)
        );
        
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        spawnedObjects.Add(obj);
    }

    private void SpawnObjectATier()
    {
        if (_ATierObjects.Count == 0) return; // if empty return
        
        GameObject prefab = _ATierObjects[Random.Range(0, _ATierObjects.Count)];

        // angle radius clamp π → 2π
        float angle = Random.Range(Mathf.PI - spread, Mathf.PI + spread);        
        // 2D position
        Vector2 pos = new Vector2(
            _center.position.x + Mathf.Cos(angle) * _radiusA + Random.Range(-0.5f, 0.5f),
            _center.position.y + Mathf.Sin(angle) * _radiusA + Random.Range(-0.5f, 0.5f)
        );

        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        spawnedObjects.Add(obj);
    }

    void SpawnObjectBTier()
    {
        if (_BTierObjects.Count == 0) return; // if empty return
        
        GameObject prefab = _BTierObjects[Random.Range(0, _BTierObjects.Count)];

        // angle radius clamp π → 2π
        float angle = Random.Range(Mathf.PI - spread, Mathf.PI + spread);        
        // 2D position
        Vector2 pos = new Vector2(
            _center.position.x + Mathf.Cos(angle) * _radiusB + Random.Range(-0.5f, 0.5f),
            _center.position.y + Mathf.Sin(angle) * _radiusB + Random.Range(-0.5f, 0.5f)
        );
        
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        spawnedObjects.Add(obj);
    }
    
    public void DestroyAllSpawned()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
