using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public static Stats Instance; // Singleton

    // Total life
    public static int health;
    // Total player ilumination
    public static int ligthning;
    // Cant die player
    public static int die;


    public Text healthText;
    public Text ligthningText;


    private void Awake()
    {
        // Singleton: evita duplicados
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre escenas
        ResetStats(); // Inicializa
    }

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Menu")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        Debug.Log("Vida: " +  health);
        if (healthText != null)
            healthText.text = "Vidas: " + health;
        if (ligthningText != null)
        {
            ligthningText.text = "Puntuacion: " + ligthning;
        }



        //Debug.Log("Vida: " + health);
    }

    private Coroutine ligthningCoroutine;
    public static bool end = false;

    private IEnumerator DecreaseLigthningOverTime()
    {
        while (true)
        {
            if (end)
            {
                yield break; 
            }

            yield return new WaitForSeconds(1f); 
            AddLigthning(-1);
        }
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void CreateUITexts()
    {
        // Busca o crea un Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            DontDestroyOnLoad(canvasObj);
        }
        // Crea el texto de vida
        if (healthText == null)
        {
            GameObject healthGO = new GameObject("HealthText");
            healthGO.transform.SetParent(canvas.transform);
            healthText = healthGO.AddComponent<Text>();
            healthText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            healthText.fontSize = 24;
            healthText.color = Color.red;
            healthText.rectTransform.anchoredPosition = new Vector2(100, -50);

            RectTransform rt = healthText.rectTransform;
            rt.sizeDelta = new Vector2(300, 40);
            rt.anchorMin = new Vector2(0, 1); // Esquina superior izquierda
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
            rt.anchoredPosition = new Vector2(10, -10); // 10px desde la esquina
        }

        // Crea el texto de luz
        if (ligthningText == null)
        {
            GameObject ligthningGO = new GameObject("LigthningText");
            ligthningGO.transform.SetParent(canvas.transform);
            ligthningText = ligthningGO.AddComponent<Text>();
            ligthningText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            ligthningText.fontSize = 24;
            ligthningText.color = Color.yellow;
            ligthningText.rectTransform.anchoredPosition = new Vector2(100, -100);

            RectTransform rt = ligthningText.rectTransform;
            rt.sizeDelta = new Vector2(300, 40);
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
            rt.anchoredPosition = new Vector2(10, -40); // Debajo del texto de vida
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Canvas canvas = FindObjectOfType<Canvas>();
        if (currentScene == "Menu")
        {
            gameObject.SetActive(false);
            if (canvas != null)
                canvas.gameObject.SetActive(false);
            if (ligthningCoroutine != null)
            {
                StopCoroutine(ligthningCoroutine);
                ligthningCoroutine = null;
            }
        }
        else
        {
            gameObject.SetActive(true);
            if (canvas != null)
                canvas.gameObject.SetActive(true);
            if (ligthningCoroutine == null)
                ligthningCoroutine = StartCoroutine(DecreaseLigthningOverTime());
        }
        CreateUITexts();
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("Menu");
        //gameObject.SetActive(false);
    }


    private static int maxLigthning = 0;
    private static int maxHealth = 7;

    public static void ResetStats()
    {
        ligthning = maxLigthning;
        health = maxHealth;
        die = 0;
        Debug.Log("Estadisticas reiniciadas.");
    }

    public void AddHealth(int amount)
    {
        health += amount;
    }

    public void SetHealth()
    {
        health = maxHealth;
    }

    public void AddLigthning(int amount)
    {
        ligthning += amount;
        if(ligthning >= 50)
        {
            ligthning -= 50;
            AddHealth(1);
        }
        ligthning = Math.Max(ligthning, 0);
    }

}
