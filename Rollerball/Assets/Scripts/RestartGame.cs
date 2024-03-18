using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void ResetTheGame()
    {
        // Carrega a cena atual
        SceneManager.LoadScene("Minigame");
        print("Game Restarted");
    }

    public void QuitGame()
    {
        // Sai do jogo
        print("Game Quit");
        Application.Quit();
    }
    
}
