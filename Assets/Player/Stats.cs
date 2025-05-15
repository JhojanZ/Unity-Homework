using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
