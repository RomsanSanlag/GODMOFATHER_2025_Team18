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

    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private float carrySpeedMultiplier = 0.5f;
    [SerializeField] private float pickupRadius = 1.2f;
    [SerializeField] private Transform carryAnchor;

    [SerializeField] private Animator animator;
    [SerializeField] private float rotationOffset = 0f;

    private int idBlend;
    private int idAnimMoveSpeed;
    private int idIsCarrying;
    private int idIsHitting;
    private int idIsStun;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 facing = Vector2.right;
    private bool canMove = true;
    private bool isStunned;
    private float nextAttackTime;

    private bool canAttack = true;
    private float baseMoveSpeed;
    private Rigidbody2D carriedRb;
    private bool isCarrying;
    private Transform carriedOriginalParent;
    private Collider2D carriedCol;
    private Collider2D playerCol;

    public float Cooldown01 => attackCooldown <= 0f ? 0f : Mathf.Clamp01((nextAttackTime - Time.time) / attackCooldown);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseMoveSpeed = moveSpeed;
        playerCol = GetComponent<Collider2D>();
        if (carryAnchor == null)
        {
            var go = new GameObject("CarryAnchor");
            carryAnchor = go.transform;
            carryAnchor.SetParent(transform);
            carryAnchor.localPosition = (Vector3)(facing.normalized * 1.5f);
        }
        if (animator == null) animator = GetComponent<Animator>();
        idBlend = Animator.StringToHash("Blend");
        idAnimMoveSpeed = Animator.StringToHash("AnimMovespeed");
        idIsCarrying = Animator.StringToHash("IsCarrying");
        idIsHitting = Animator.StringToHash("IsHitting");
        idIsStun = Animator.StringToHash("IsStun");
        if (animator != null)
        {
            animator.SetBool(idIsCarrying, false);
            animator.SetBool(idIsStun, false);
            animator.SetFloat(idBlend, 0f);
            animator.SetFloat(idAnimMoveSpeed, 0f);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.sqrMagnitude > 0.0001f) facing = moveInput.normalized;
    }

    void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;
        if (!canAttack) return;
        if (isStunned) return;
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + attackCooldown;
        if (animator != null) animator.SetTrigger(idIsHitting);

        Vector2 origin = rb.position + facing * attackRange;
        var hits = Physics2D.OverlapCircleAll(origin, attackRadius, hittableMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].attachedRigidbody == rb) continue;
            var receiver = hits[i].GetComponent<StunReceiver>();
            if (receiver != null) receiver.TakeHit(facing, knockbackDistance, knockbackDashTime, stunDuration);
        }
    }

    void OnPickup(InputValue value)
    {
        if (!value.isPressed) return;
        if (isStunned) return;
        if (isCarrying)
        {
            DropCarried();
            return;
        }

        Collider2D[] nearby = Physics2D.OverlapCircleAll(rb.position, pickupRadius, pickupMask);
        if (nearby.Length == 0) return;

        float bestDist = float.MaxValue;
        Rigidbody2D best = null;
        for (int i = 0; i < nearby.Length; i++)
        {
            var r = nearby[i].attachedRigidbody;
            if (r == null) continue;
            float d = Vector2.SqrMagnitude(r.position - rb.position);
            if (d < bestDist)
            {
                bestDist = d;
                best = r;
            }
        }
        if (best == null) return;

        carriedRb = best;
        carriedOriginalParent = carriedRb.transform.parent;
        carriedCol = carriedRb.GetComponent<Collider2D>();
        if (carriedCol != null && playerCol != null) Physics2D.IgnoreCollision(carriedCol, playerCol, true);
        carriedRb.simulated = false;
        carriedRb.transform.SetParent(carryAnchor);
        carriedRb.transform.localPosition = Vector3.zero;
        isCarrying = true;
        canAttack = false;
        moveSpeed = baseMoveSpeed * carrySpeedMultiplier;

        if (animator != null)
        {
            animator.SetBool(idIsCarrying, true);
            animator.SetBool(idIsStun, false);
            animator.SetFloat(idBlend, 0f);
            animator.SetFloat(idAnimMoveSpeed, 0f);
        }
    }

    void FixedUpdate()
    {
        Vector2 v = canMove ? moveInput * moveSpeed : Vector2.zero;
        rb.linearVelocity = v;

        if (v.sqrMagnitude > 0.0001f)
        {
            float angle = Vector2.SignedAngle(Vector2.up, v.normalized) + rotationOffset;
            rb.MoveRotation(angle);
        }

        if (carryAnchor != null) carryAnchor.localPosition = (Vector3)(facing.normalized * 1.5f);

        if (animator != null)
        {
            if (isStunned)
            {
                animator.SetBool(idIsStun, true);
                animator.SetBool(idIsCarrying, false);
                animator.SetFloat(idBlend, 0f);
                animator.SetFloat(idAnimMoveSpeed, 0f);
            }
            else if (isCarrying)
            {
                animator.SetBool(idIsStun, false);
                animator.SetBool(idIsCarrying, true);
                animator.SetFloat(idBlend, 0f);
                animator.SetFloat(idAnimMoveSpeed, 0f);
            }
            else
            {
                float speed = rb.linearVelocity.magnitude;
                animator.SetBool(idIsStun, false);
                animator.SetBool(idIsCarrying, false);
                animator.SetFloat(idAnimMoveSpeed, speed);
                animator.SetFloat(idBlend, speed);
            }
        }
    }

    public void SetStun(bool value)
    {
        isStunned = value;
        if (value) DropCarried();
        canMove = !value;
        if (value) rb.linearVelocity = Vector2.zero;
    }

    void DropCarried()
    {
        if (!isCarrying) return;
        if (carriedRb != null)
        {
            carriedRb.transform.SetParent(carriedOriginalParent);
            carriedRb.transform.position = carryAnchor.position;
            carriedRb.simulated = true;
            if (carriedCol != null && playerCol != null) Physics2D.IgnoreCollision(carriedCol, playerCol, false);
        }
        carriedRb = null;
        carriedCol = null;
        carriedOriginalParent = null;
        isCarrying = false;
        canAttack = true;
        moveSpeed = baseMoveSpeed;
        if (animator != null) animator.SetBool(idIsCarrying, false);
    }
}
