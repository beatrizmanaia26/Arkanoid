using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public static int PlayerScore = 0; // (opcional, não usado)

    public GUISkin layout;
    [Header("Pontuação")]
    public int pontos = 0;
    public int recordePessoal = 0;

    [Header("Progresso da Fase")]
    public int blocosRestantes;
    public GameObject vitoriaUI; // Pode ser usado para mostrar algo, mas vamos trocar de cena

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        blocosRestantes = FindObjectsOfType<Brick>().Length;
        recordePessoal = PlayerPrefs.GetInt("Recorde", 0);
        Debug.Log("Blocos na fase: " + blocosRestantes);
    }

    public void GanharPonto(int quantidade)
    {
        pontos += quantidade;
        Debug.Log("Pontos: " + pontos);

        if (pontos > recordePessoal)
        {
            recordePessoal = pontos;
            PlayerPrefs.SetInt("Recorde", recordePessoal);
            PlayerPrefs.Save();
            Debug.Log("Novo recorde: " + recordePessoal);
        }
    }

    public void BlocoDestruido(int pontosGanhos)
    {
        GanharPonto(pontosGanhos);
        blocosRestantes--;
        Debug.Log("Bloco destruído! Restam: " + blocosRestantes);

        // Se todos os blocos foram destruídos (fase completa)
        if (blocosRestantes <= 0)
        {
            CarregarFase2();
        }
    }

    void CarregarFase2()
    {
        Debug.Log("Fase completa! Carregando Fase 2...");
        SceneManager.LoadScene("fase2"); // Certifique-se que a cena se chama "Fase2" e está no Build Settings
    }

    // Opcional: manter a exibição da pontuação
    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + pontos);
    }
}