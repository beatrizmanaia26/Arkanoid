using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // <-- Adicione esta linha

public class TelaFinal : MonoBehaviour
{
    public TextMeshProUGUI mensagem;

    void Start()
    {
        if (mensagem == null)
        {
            Debug.LogError("❌ A referência 'mensagem' está nula! Atribua o Text (TMP) no Inspector.");
            return;
        }
        else
        {
            Debug.Log("✅ Texto conectado: " + mensagem.name);
        }

        if (gameManager.instance == null)
        {
            Debug.LogError("❌ GameManager.instance é nulo! Ele pode ter sido destruído.");
            return;
        }
        else
        {
            Debug.Log("✅ GameManager encontrado. venceu = " + gameManager.instance.venceu);
        }

        if (gameManager.instance.venceu)
        {
            mensagem.text = "VOCÊ VENCEU!";
            mensagem.color = Color.green;
        }
        else
        {
            mensagem.text = "VOCÊ PERDEU!";
            mensagem.color = Color.red;
        }
    }
public void ReiniciarJogo()
{
    Debug.Log("🔄 ReiniciarJogo foi chamado! Carregando fase1...");
    SceneManager.LoadScene("fase1");
}
}