using UnityEngine;

public class Brick : MonoBehaviour
{
    [Header("Configurações")]
    public int pontos = 1;   // Cada bloco vale 1 ponto

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (gameManager.instance != null)
            {
                gameManager.instance.BlocoDestruido(pontos);
            }
            Destroy(gameObject);
        }
    }
}