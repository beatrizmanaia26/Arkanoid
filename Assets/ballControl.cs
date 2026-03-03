using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ballControl : MonoBehaviour
{
    [Header("Configurações Gerais")]
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = 0f;

        startPosition = transform.position;
        Launch();
    }

    void Launch()
    {
        rb.linearVelocity = Vector2.down * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BottomWall"))
        {
            gameManager.instance.PerderVida(); // 🔥 perde vida
            ResetPuck();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            float offset = transform.position.x - col.transform.position.x;
            float width = col.collider.bounds.size.x;
            float hitFactor = offset / width;

            Vector2 dir = new Vector2(hitFactor * 2.5f, 1).normalized;
            rb.linearVelocity = dir * speed;
        }
        else
        {
            Vector2 dir = rb.linearVelocity.normalized;

            // Evita movimento muito horizontal
            if (Mathf.Abs(dir.y) < 0.3f)
            {
                dir.y = 0.3f * Mathf.Sign(dir.y == 0 ? 1 : dir.y);
                dir.Normalize();
            }

            rb.linearVelocity = dir * speed;
        }
    }

    void ResetPuck()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPosition;
        Launch();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }
}