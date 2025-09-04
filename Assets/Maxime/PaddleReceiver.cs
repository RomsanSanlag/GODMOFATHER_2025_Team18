using UnityEngine;

public class PaddleReceiver : MonoBehaviour
{
    public PaddleStats stats;

    void Awake()
    {
        if (stats == null) stats = GetComponentInParent<PaddleStats>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null) player = other.GetComponentInParent<PlayerController>();
        if (player == null || stats == null) return;
        player.DepositTo(stats);
    }
}