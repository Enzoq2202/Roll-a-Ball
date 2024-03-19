using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void ResetTheGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("Minigame");
        print("Game Restarted");
    }


    public void QuitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        print("Game Quit");
        Application.Quit();
    }

    
}
