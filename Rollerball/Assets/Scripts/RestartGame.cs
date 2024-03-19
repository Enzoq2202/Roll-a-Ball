using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void ResetTheGame()
    {
        // Prepara o cursor para a jogabilidade, se necessário
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("Minigame");
        print("Game Restarted");
    }


    public void QuitGame()
    {
        // Antes de sair, garante que o cursor seja visível
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        print("Game Quit");
        Application.Quit();
    }

    
}
