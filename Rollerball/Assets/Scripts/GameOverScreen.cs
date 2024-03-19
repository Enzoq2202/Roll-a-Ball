using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText; 

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = score.ToString() + " Points";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void RestartButton()
    {
        SceneManager.LoadScene("MiniGame");
    }


    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
