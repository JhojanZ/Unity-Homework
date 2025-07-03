
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame(){
        Stats.ResetStats();
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }
}
