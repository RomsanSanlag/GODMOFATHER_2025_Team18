using UnityEngine;

public class AttackCooldownBar : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform barTransform;
    [SerializeField] private SpriteRenderer barRenderer;

    private Vector3 baseScale;

    void Awake()
    {
        if (player == null) player = GetComponentInParent<PlayerController>();
        if (barTransform == null) barTransform = transform;
        if (barRenderer == null) barRenderer = GetComponent<SpriteRenderer>();
        baseScale = barTransform.localScale;
        barRenderer.enabled = false;
    }

    void Update()
    {
        float p = player != null ? player.Cooldown01 : 0f;
        if (p > 0f)
        {
            if (!barRenderer.enabled) barRenderer.enabled = true;
            barTransform.localScale = new Vector3(baseScale.x * p, baseScale.y, baseScale.z);
        }
        else
        {
            if (barRenderer.enabled) barRenderer.enabled = false;
            if (barTransform.localScale.x != baseScale.x) barTransform.localScale = baseScale;
        }
    }
}