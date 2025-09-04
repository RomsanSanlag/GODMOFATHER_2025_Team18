using UnityEngine;

public class PaddleStats : MonoBehaviour
{
    public int baseSpeed = 0;
    public int currentSpeed = 0;
    public GameObject[] visualsByIndex;

    void Awake()
    {
        currentSpeed = baseSpeed;
        if (visualsByIndex != null)
            for (int i = 0; i < visualsByIndex.Length; i++)
                if (visualsByIndex[i] != null) visualsByIndex[i].SetActive(false);
    }

    public void ApplyItem(Objects obj)
    {
        if (obj == null) return;
        currentSpeed += obj.value;
        if (visualsByIndex != null && obj.index >= 0 && obj.index < visualsByIndex.Length)
        {
            var go = visualsByIndex[obj.index];
            if (go != null) go.SetActive(true);
        }
    }
}