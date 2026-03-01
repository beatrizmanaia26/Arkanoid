using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ballControl : MonoBehaviour
{
    [Header("Configurações Gerais")]
    public float speed = 10f;

    public GameObject playerPaddle; // opcional, mas usamos tag

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
            ResetPuck();
        }
    }

   void OnCollisionEnter2D(Collision2D col)
{
    Debug.Log("Colidiu com: " + col.gameObject.name);

    if (col.gameObject.CompareTag("Player"))
    {
        // Lógica do paddle (igual)
        float offset = transform.position.x - col.transform.position.x;
        float width = col.collider.bounds.size.x;
        float hitFactor = offset / width;
        Vector2 dir = new Vector2(hitFactor * 2.5f, 1).normalized;
        rb.linearVelocity = dir * speed;
    }
    else
    {
        // 1. Calcula a média dos vetores normais de contato
        Vector2 normalMedia = Vector2.zero;
        int totalContatos = col.contactCount;

        for (int i = 0; i < totalContatos; i++)
        {
            ContactPoint2D contato = col.GetContact(i);
            normalMedia += contato.normal;
        }

        // 2. Normaliza a média para obter a direção normal efetiva
        normalMedia.Normalize();

        // 3. Calcula a nova direção refletida
        Vector2 newDirection = Vector2.Reflect(rb.linearVelocity.normalized, normalMedia).normalized;

        // 4. Aplica a velocidade mantendo a magnitude constante
        rb.linearVelocity = newDirection * speed;

        // 5. Pequeno deslocamento para evitar "grudar" na parede
        // Isso move a bola ligeiramente na direção da nova velocidade, garantindo que ela saia da superfície
        transform.position += (Vector3)(newDirection * 0.02f);
    }
}
    void ResetPuck()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPosition;
        Launch();
    }
}