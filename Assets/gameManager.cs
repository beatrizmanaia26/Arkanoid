using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GUISkin layout;

    [Header("Pontuação")]
    public int pontos = 0;
    public int recordePessoal = 0;

    [Header("Progresso da Fase")]
    public int blocosRestantes;

    void Awake()
    {
        // Mantém apenas uma instância e não destrói ao trocar de cena
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Chamado automaticamente toda vez que uma cena nova carrega
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    pontos = 0;
        blocosRestantes = FindObjectsOfType<Brick>().Length;
        recordePessoal = PlayerPrefs.GetInt("Recorde", 0);
        Debug.Log("Cena carregada: " + scene.name + " | Blocos: " + blocosRestantes);
    }

    public void GanharPonto()
    {
        pontos += 1;

        if (pontos > recordePessoal)
        {
            recordePessoal = pontos;
            PlayerPrefs.SetInt("Recorde", recordePessoal);
            PlayerPrefs.Save();
            Debug.Log("Novo recorde: " + recordePessoal);
        }

        Debug.Log("Pontos: " + pontos);
    }

    public void BlocoDestruido()
    {
        GanharPonto();
        blocosRestantes--;
        Debug.Log("Bloco destruído! Restam: " + blocosRestantes);

        if (blocosRestantes <= 0){
        Debug.Log("BLOCOS RESTANTESSSS: " + blocosRestantes);
            ProximaFase();
        }
    }
void ProximaFase()
    {
        int proximaFase = SceneManager.GetActiveScene().buildIndex + 1;

        if (proximaFase < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Fase completa! Carregando próxima fase...");
            SceneManager.LoadScene(proximaFase);
        }
        else
        {
            Debug.Log("Fim de jogo! Pontuação final: " + pontos);
            // Aqui você pode carregar uma tela de vitória ou menu
        }
    }

    void OnGUI()
    {
        if (layout != null)
            GUI.skin = layout;

        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + pontos);
    }
}