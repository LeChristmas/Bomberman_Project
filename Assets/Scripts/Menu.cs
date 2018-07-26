using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("- Array Of All The Menu Sections -")]
    public GameObject[] Menu_Sections;

    [Header("- String Of The Game Scene Name -")]
    public string levelname;

    // Called When The Start Button Is Pressed
    public void Start_Button ()
    {
        if (levelname != null)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().current_scene = Scene.Game;
            SceneManager.LoadScene(levelname);
        }
    }

    // Called When The Scores Button Is Pressed
    public void Scores ()
    {

    }

    // Called When The Return Button Is Pressed
    public void Return ()
    {

    }

    // Called When the Quit Button Is Pressed
    public void Quit()
    {
        // Quits The Application
        Application.Quit();
    }
}
