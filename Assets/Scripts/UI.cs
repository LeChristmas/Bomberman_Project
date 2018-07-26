using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("- Names Of Scenes To Transistion To -")]
    public string main_scene_name;
    public string menu_scene_name;

    [Header("- Array Of UI Elements -")]
    public GameObject[] ui_objects;
    public GameObject[] saving_ui;

    [Header("- Controls UI -")]
    public GameObject[] controls_gameobject;
    private bool controls = true;

    [Header("- Timer Variables -")]
    public int time;
    public Text timer_text;

    [Header("- Stage Indicator Text variable -")]
    public Text stage_text;

    [Header("- Saving Variables -")]
    public Text score_text;
    private string player_name;

    // Called On Frist Frame
    private void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(Game_Timer());
    }

    // Called Every Frame
    private void Update()
    {
        // Turns Control UI On And Off
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

    // Called When Game Is Paused
    public void Pause()
    {
        // Switches UI To Pause
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

        // Freezes Game
        Time.timeScale = 0.0f;
    }

    // Called When Game Is Unpaused
    public void UnPause()
    {
        // Switches UI To Game
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

        // Sets game Time To Normal
        Time.timeScale = 1.0f;
    }

    // Called When The Player Dies
    public void Death (GameObject player)
    {
        Destroy(player);

        // Reducts Life
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().lives--;

        // Resets All Player's Powerup Stats To Default
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().max_bombs = 1;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bomb_strength = 1;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().speed_increase = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_wallpass = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().detonator = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_bombpass = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_flamepass = false;

        // If Lives Are Gone Display Game Over Screen
        if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().lives == 0)
        {
            Time.timeScale = 0.0f;

            for (int i = 0; i < ui_objects.Length; i++)
            {
                if (i == 2)
                {
                    ui_objects[i].SetActive(true);
                    if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().saving_active)
                    {
                        saving_ui[0].SetActive(true);
                        saving_ui[1].SetActive(false);

                        score_text.text = "Score: " + GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score;
                    }
                    else
                    {
                        saving_ui[1].SetActive(true);
                        saving_ui[0].SetActive(false);
                    }
                }
                else
                {
                    ui_objects[i].SetActive(false);
                }
            }
        }
        else
        {
            SceneManager.LoadScene(main_scene_name);
        }
    }

    // Called When The Player Complete The Stage
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

    // Called When The Player Resets The Game
    public void Reset_Game ()
    {
        // resets All Stats
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().score = 0;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().lives = 2;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number = 0;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().max_bombs = 1;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bomb_strength = 1;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().speed_increase = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_wallpass = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().detonator = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_bombpass = false;
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().player_flamepass = false;

        SceneManager.LoadScene(main_scene_name);
    }

    // Called When The Players Moves On To The Next Level
    public void Next_Level ()
    {
        // Used To Determine If Next Level Is A Bonus Stage Or Not
        if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 4 
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.A;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 0;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 9
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.B;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 1;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 14
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.C;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 2;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 19
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.D;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 3;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 24
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.E;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 4;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 29
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.F;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 5;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 34
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.G;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 6;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 38
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.H;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 7;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 43
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.I;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 7;
        }
        else if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number == 48
            && GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.J;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = 7;
        }
        else
        {
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage = Bonus_Stage.Off;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_enemy_index = -1;
            GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number++;
        }

        SceneManager.LoadScene(main_scene_name);
    }

    // Called Whe The Player Returns To The Application
    public void Quit ()
    {
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().current_scene = Scene.Menu;
        SceneManager.LoadScene(menu_scene_name);
    }

    // Used To Count Down The Timer
    IEnumerator Game_Timer ()
    {
        yield return new WaitForSeconds(0.1f);

        // Changes Beginning Screen Depending On If It Is A Bonus Stage Or Not
        if (GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            int lvl_number = GameObject.FindGameObjectWithTag("data").GetComponent<Data>().level_number + 1;
            stage_text.text = "Stage:" + lvl_number;
        }
        else
        {
            stage_text.text = "Bonus:" + GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage;
        }

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

        // Used To Decide What Happens When Timer Ends
        if(GameObject.FindGameObjectWithTag("data").GetComponent<Data>().bonus_stage == Bonus_Stage.Off)
        {
            GameObject.Find("Wall_Spawns").GetComponent<Wall_Spawner>().Time_Up();
        }
        else
        {
            Win();
        }
    }

    public void On_Value_Changed (InputField ipf)
    {
        player_name = ipf.text;
    }

    public void Continue_Button ()
    {
        GameObject.FindGameObjectWithTag("data").GetComponent<Data>().Save_Data(player_name);

        saving_ui[1].SetActive(true);
        saving_ui[0].SetActive(false);
    }
}
