using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public bool venceu = false;
    [Header("Pontuação")]
    public int pontos = 0;

    [Header("Progresso da Fase")]
    public int blocosRestantes;

    [Header("Vidas")]
    public int vidas = 15;

    void Awake()
    {
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        blocosRestantes = FindObjectsOfType<Brick>().Length;

        if (scene.name == "fase1")
        {
            vidas = 15;
            pontos = 0;
        }
    }

    public void BlocoDestruido()
    {
        pontos++;
        blocosRestantes--;

        Debug.Log("Pontos: " + pontos);

        if (blocosRestantes <= 0)
        {
            ProximaFase();
        }
    }

void ProximaFase()
{
    string cenaAtual = SceneManager.GetActiveScene().name;

    if (cenaAtual == "fase1")
    {
        SceneManager.LoadScene("fase2");
    }
    else if (cenaAtual == "fase2")
    {
        venceu = true;
        SceneManager.LoadScene("telaFinal");
    }
}

public void PerderVida()
{
    vidas--;

    if (vidas <= 0)
    {
        venceu = false; 
        SceneManager.LoadScene("telaFinal");
    }
}

    void OnGUI()
    {
        GUIStyle estilo = new GUIStyle(GUI.skin.label);
        estilo.fontSize = 30;
        estilo.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 20, 300, 40), "Vidas: " + vidas, estilo);
        GUI.Label(new Rect(Screen.width - 200, 20, 300, 40), "Pontos: " + pontos, estilo);
    }
}