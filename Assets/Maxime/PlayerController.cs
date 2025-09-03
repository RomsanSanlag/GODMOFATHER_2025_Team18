using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackRange = 0.8f;
    [SerializeField] private float attackRadius = 0.6f;
    [SerializeField] private float knockbackDistance = 2f;
    [SerializeField] private float knockbackDashTime = 0.12f;
    [SerializeField] private float stunDuration = 1f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private LayerMask hittableMask;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 facing = Vector2.right;
    private bool canMove = true;
    private bool isStunned;
    private float nextAttackTime;

    public float Cooldown01 => attackCooldown <= 0f ? 0f : Mathf.Clamp01((nextAttackTime - Time.time) / attackCooldown);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.sqrMagnitude > 0.0001f) facing = moveInput.normalized;
    }

    void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;
        if (isStunned) return;
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + attackCooldown;

        Vector2 origin = rb.position + facing * attackRange;
        var hits = Physics2D.OverlapCircleAll(origin, attackRadius, hittableMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].attachedRigidbody == rb) continue;
            var receiver = hits[i].GetComponent<StunReceiver>();
            if (receiver != null) receiver.TakeHit(facing, knockbackDistance, knockbackDashTime, stunDuration);
        }
    }

    void Onpickup(InputValue value)
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = canMove ? moveInput * moveSpeed : Vector2.zero;
    }

    public void SetStun(bool value)
    {
        isStunned = value;
        canMove = !value;
        if (value) rb.linearVelocity = Vector2.zero;
    }
}
