using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //this will go from MainMenu to Game
    }

    public void QuitGame()
    {
        Application.Quit(); //quits game lol
    }
}
