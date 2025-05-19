using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public static Stats Instance; // Singleton

    // Total life
    public int health;
    // Total player ilumination
    public int ligthning;
    // Cant die player
    public int die;


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
        if (healthText != null)
            healthText.text = "Vida: " + health;
        if (ligthningText != null)
            ligthningText.text = "Luz: " + ligthning;

        //Debug.Log("Vida: " + health);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Oculta el Canvas si la escena es "Menu"
        gameObject.SetActive(scene.name != "Menu");
    }


    private int maxLigthning = 10;
    private int maxHealth = 3;

    public void ResetStats()
    {
        ligthning = maxLigthning;
        health = maxHealth;
        die = 0;
        Debug.Log("Estadísticas reiniciadas.");
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
    }

}
