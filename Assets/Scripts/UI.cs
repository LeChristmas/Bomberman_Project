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
        Data.game_data.lives--;

        // Resets All Player's Powerup Stats To Default
        Data.game_data.max_bombs = 1;
        Data.game_data.bomb_strength = 1;
        Data.game_data.speed_increase = false;
        Data.game_data.player_wallpass = false;
        Data.game_data.detonator = false;
        Data.game_data.player_bombpass = false;
        Data.game_data.player_flamepass = false;

        // If Lives Are Gone Display Game Over Screen
        if (Data.game_data.lives == 0)
        {
            Time.timeScale = 0.0f;

            for (int i = 0; i < ui_objects.Length; i++)
            {
                if (i == 2)
                {
                    ui_objects[i].SetActive(true);
                    if (Data.game_data.saving_active)
                    {
                        saving_ui[0].SetActive(true);
                        saving_ui[1].SetActive(false);

                        score_text.text = "Score: " + Data.game_data.score;
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
        Data.game_data.score = 0;
        Data.game_data.lives = 2;
        Data.game_data.level_number = 0;
        Data.game_data.max_bombs = 1;
        Data.game_data.bomb_strength = 1;
        Data.game_data.speed_increase = false;
        Data.game_data.player_wallpass = false;
        Data.game_data.detonator = false;
        Data.game_data.player_bombpass = false;
        Data.game_data.player_flamepass = false;

        SceneManager.LoadScene(main_scene_name);
    }

    // Called When The Players Moves On To The Next Level
    public void Next_Level ()
    {
        // Used To Determine If Next Level Is A Bonus Stage Or Not
        if (Data.game_data.bonus_stage == Bonus_Stage.Off)
        {
            if (Data.game_data.level_number == 4)
            {
                Data.game_data.bonus_stage = Bonus_Stage.A;
                Data.game_data.bonus_enemy_index = 0;
            }
            else if (Data.game_data.level_number == 9)
            {
                Data.game_data.bonus_stage = Bonus_Stage.B;
                Data.game_data.bonus_enemy_index = 1;
            }
            else if (Data.game_data.level_number == 14)
            {
                Data.game_data.bonus_stage = Bonus_Stage.C;
                Data.game_data.bonus_enemy_index = 2;
            }
            else if (Data.game_data.level_number == 19)
            {
                Data.game_data.bonus_stage = Bonus_Stage.D;
                Data.game_data.bonus_enemy_index = 3;
            }
            else if (Data.game_data.level_number == 24)
            {
                Data.game_data.bonus_stage = Bonus_Stage.E;
                Data.game_data.bonus_enemy_index = 4;
            }
            else if (Data.game_data.level_number == 29)
            {
                Data.game_data.bonus_stage = Bonus_Stage.F;
                Data.game_data.bonus_enemy_index = 5;
            }
            else if (Data.game_data.level_number == 34)
            {
                Data.game_data.bonus_stage = Bonus_Stage.G;
                Data.game_data.bonus_enemy_index = 6;
            }
            else if (Data.game_data.level_number == 38)
            {
                Data.game_data.bonus_stage = Bonus_Stage.H;
                Data.game_data.bonus_enemy_index = 7;
            }
            else if (Data.game_data.level_number == 43)
            {
                Data.game_data.bonus_stage = Bonus_Stage.I;
                Data.game_data.bonus_enemy_index = 7;
            }
            else if (Data.game_data.level_number == 48)
            {
                Data.game_data.bonus_stage = Bonus_Stage.J;
                Data.game_data.bonus_enemy_index = 7;
            }
        }
        else
        {
            Data.game_data.bonus_stage = Bonus_Stage.Off;
            Data.game_data.bonus_enemy_index = -1;
            Data.game_data.level_number++;
        }

        SceneManager.LoadScene(main_scene_name);
    }

    // Called Whe The Player Returns To The Application
    public void Quit ()
    {
        Data.game_data.current_scene = Scene.Menu;
        SceneManager.LoadScene(menu_scene_name);
    }

    // Used To Count Down The Timer
    IEnumerator Game_Timer ()
    {
        yield return new WaitForSeconds(0.1f);

        // Changes Beginning Screen Depending On If It Is A Bonus Stage Or Not
        if (Data.game_data.bonus_stage == Bonus_Stage.Off)
        {
            int lvl_number = Data.game_data.level_number + 1;
            stage_text.text = "Stage:" + lvl_number;
        }
        else
        {
            stage_text.text = "Bonus:" + Data.game_data.bonus_stage;
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
        if(Data.game_data.bonus_stage == Bonus_Stage.Off)
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
        Data.game_data.Save_Data(player_name);

        saving_ui[1].SetActive(true);
        saving_ui[0].SetActive(false);
    }
}
