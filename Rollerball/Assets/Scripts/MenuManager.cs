using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private string SceneName;
    
    public void StartGame()
    {
        Debug.Log("Start Game called");
        SceneManager.LoadScene(SceneName);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game called");
        Application.Quit();
    }

}
