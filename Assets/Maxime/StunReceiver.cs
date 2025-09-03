using System.Collections;
using UnityEngine;

public class StunReceiver : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerController controller;
    private Color baseColor;
    private Coroutine co;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
        baseColor = sr.color;
    }

    public void TakeHit(Vector2 dir, float distance, float dashTime, float stunTime)
    {
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(KnockAndStun(dir.normalized, distance, dashTime, stunTime));
    }

    IEnumerator KnockAndStun(Vector2 dir, float distance, float dashTime, float stunTime)
    {
        controller?.SetStun(true);
        sr.color = new Color(baseColor.r * 0.5f, baseColor.g * 0.5f, baseColor.b * 0.5f, baseColor.a);

        Vector2 start = rb.position;
        Vector2 end = start + dir * distance;
        float t = 0f;
        while (t < dashTime)
        {
            t += Time.fixedDeltaTime;
            float a = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / dashTime));
            rb.MovePosition(Vector2.Lerp(start, end, a));
            yield return new WaitForFixedUpdate();
        }

        rb.linearVelocity = Vector2.zero;

        if (stunTime > 0f) yield return new WaitForSeconds(stunTime);

        sr.color = baseColor;
        controller?.SetStun(false);
        co = null;
    }
}