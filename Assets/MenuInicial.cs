using UnityEngine;
using UnityEngine.SceneManagement; 
public class MenuInicial : MonoBehaviour
{
    // Método público para ser chamado pelo botão
    public void Jogar()
    {
        Debug.Log("Botão clicado! Carregando cena...");
        SceneManager.LoadScene("fase1");
    }
}