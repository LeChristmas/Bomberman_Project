using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public string scene_name;

    public GameObject[] ui_objects;

    public int time;
    public Text timer_text;

    public GameObject[] controls_gameobject;

    public Text stage_text;

    private bool controls = true;

    private void Start()
    {
        StartCoroutine(Game_Timer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            controls = !controls;
        }

        if(controls == true)
        {
            foreach (GameObject control in controls_gameobject)
            {
                control.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject control in controls_gameobject)
            {
                control.SetActive(false);
            }
        }
    }

    public void Pause()
    {
        for (int i = 0; i < ui_objects.Length; i++)
        {
            if(i == 1)
            {
                ui_objects[i].SetActive(true);
            }
            else
            {
                ui_objects[i].SetActive(false);
            }
        }

        Time.timeScale = 0.0f;
    }

    public void UnPause()
    {
        for (int i = 0; i < ui_objects.Length; i++)
        {
            if (i == 0)
            {
                ui_objects[i].SetActive(true);
            }
            else
            {
                ui_objects[i].SetActive(false);
            }
        }

        Time.timeScale = 1.0f;
    }

    public void Death ()
    {
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().lives--;

        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().max_bombs = 1;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bomb_strength = 1;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().speed_increase = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_wallpass = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().detonator = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_bombpass = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_flamepass = false;

        if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().lives == 0)
        {
            for (int i = 0; i < ui_objects.Length; i++)
            {
                if (i == 2)
                {
                    ui_objects[i].SetActive(true);
                }
                else
                {
                    ui_objects[i].SetActive(false);
                }
            }

            Time.timeScale = 0.0f;
        }
        else
        {
            SceneManager.LoadScene(scene_name);
        }
    }

    public void Win ()
    {
        for (int i = 0; i < ui_objects.Length; i++)
        {
            if (i == 3)
            {
                ui_objects[i].SetActive(true);
            }
            else
            {
                ui_objects[i].SetActive(false);
            }
        }

        Time.timeScale = 0.0f;
    }

    public void Reset_Game ()
    {
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score = 0;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().lives = 2;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number = 0;

        SceneManager.LoadScene(scene_name);
    }

    public void Next_Level ()
    {
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number++;
        SceneManager.LoadScene(scene_name);
    }

    public void Quit ()
    {
        Application.Quit();
    }

    IEnumerator Game_Timer ()
    {
        yield return new WaitForSeconds(0.1f);

        stage_text.text = "Stage:" + GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number + 1;

        for (int i = 0; i < ui_objects.Length; i++)
        {
            if (i == 4)
            {
                ui_objects[i].SetActive(true);
            }
            else
            {
                ui_objects[i].SetActive(false);
            }
        }

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < ui_objects.Length; i++)
        {
            if (i == 0)
            {
                ui_objects[i].SetActive(true);
            }
            else
            {
                ui_objects[i].SetActive(false);
            }
        }

        Time.timeScale = 1.0f;

        for (int i = time; i > 0; i--)
        {
            timer_text.text = "Timer: " + i;
            yield return new WaitForSeconds(1.0f);
        }

        Death();
    }
}
