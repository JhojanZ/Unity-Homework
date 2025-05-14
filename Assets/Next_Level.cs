using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_Level : MonoBehaviour
{
    public string nombreEscenaDestino;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainPlayer")) 
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}
