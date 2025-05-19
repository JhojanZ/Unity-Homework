using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame(){
        //Stats.Instance.ResetStats();
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
