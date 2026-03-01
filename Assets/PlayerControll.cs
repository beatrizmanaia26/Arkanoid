using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float velocidade = 10f;

    // Agora são referências para objetos na cena (paredes)
    public Transform leftWall;      // Arraste a parede esquerda aqui
    public Transform rightWall;     // Arraste a parede direita aqui

    void Update()
    {
        float movimento = 0f;

        if (Input.GetKey(moveLeft))
            movimento = -1f;
        if (Input.GetKey(moveRight))
            movimento = 1f;

        // Move o paddle
        Vector3 deslocamento = Vector3.right * movimento * velocidade * Time.deltaTime;
        transform.Translate(deslocamento);

        // Aplica os limites baseados na posição das paredes
        if (leftWall != null && rightWall != null)
        {
            Vector3 posicaoAtual = transform.position;
            // Usa a posição X das paredes como mínimo e máximo
            posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, leftWall.position.x, rightWall.position.x);
            transform.position = posicaoAtual;
        }
    }
}